using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour {

    public float timeScaleDelta = 0.05f;
    public bool IsFrozen { get; private set; }
    public char CurrentRoom { get; private set; }
    
    public Animator sceneAnimator;
    public GameObject player;
    public Dialog dialogView;
    public GameObject enemyPrefab;

    private Dictionary<char, GameObject> rooms = new Dictionary<char, GameObject>();
    private GameObject doorPrefab;
    private char roomToLoad;
    private bool willSpawnDoors;
    private bool willSpawnEnemies;
    private DialogModel dialogs;
    private List<char> roomsLeftToVisit = new List<char>();
    private TextAsset dialogsJson;

    // Used to remove player movement
    private System.Type currentPlayerMovement;
    private System.Type currentPlayerAttack;

    private void Start() {
        // Use this to init all the dialog lines contained in Resources/dialogs.txt
        dialogsJson = Resources.Load("dialogs") as TextAsset;
        dialogs = JsonUtility.FromJson<DialogModel>(dialogsJson.text);

        foreach(GameObject o in Resources.LoadAll<GameObject>("Rooms")) {
            char name = o.name[0];
            rooms.Add(name, o);
            if (name != 'A' && name != 'F')
                roomsLeftToVisit.Add(name);
        }
        doorPrefab = Resources.Load<GameObject>("Door");
        LoadRoom('A',spawnEnemies: false);
        ChangePlayerMovement('A');
        ChangePlayerAttack('A');
    }

    public void Restart() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        roomsLeftToVisit = rooms.Where(kvp => kvp.Key != 'A' && kvp.Key != 'F').ToDictionary(kvp => kvp.Key, kvp => kvp.Value).Keys.ToList();
        dialogs = JsonUtility.FromJson<DialogModel>(dialogsJson.text);
        Freeze();
        ChangeRoom('A', false, false);
    }

    void FixedUpdate() {
        if (IsFrozen) {
            Time.timeScale = Mathf.Max(0, Time.timeScale - timeScaleDelta);
        } else {
            Time.timeScale = 1;
        }
    }

    public void Freeze() {
        this.IsFrozen = true;
        foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet")) {
            Destroy(bullet);
        }
    }

    public void Unfreeze() {
        this.IsFrozen = false;
        Time.timeScale = 1;
    }

    public void LoadRoom(char id, bool spawnDoors = false, bool spawnEnemies = true) {
        // Delete any room object instantiated.
        Destroy(GameObject.FindGameObjectWithTag("Room"));

        // Spawn new room
        GameObject room = Instantiate(rooms[id]);
        room.name = id.ToString();

        if (spawnDoors)
            SpawnDoors(room);

        if (spawnEnemies)
            for (int i = 0; i < Random.Range(1, 3); i++) {
                GameObject enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), -1), Quaternion.identity);
                enemy.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }

        MovePlayerToSpawn();
        this.CurrentRoom = id;
    }

    public void SpawnDoors(GameObject room) {
        // Generate 2 doors
        int pos = Random.Range(0, 3);
        Texture2D bg = room.transform.Find("Background").GetComponent<SpriteRenderer>().sprite.texture;

        for (int i = 0; i < 2; i++) {
            GameObject door = Instantiate(doorPrefab);
            SpriteRenderer doorSprite = door.GetComponentInChildren<SpriteRenderer>();
            door.transform.parent = room.transform;

            switch (pos) {
                case 0: // Top
                    door.transform.localPosition = new Vector3(0, 1.80f, -2);
                    door.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 1: // Right
                    door.transform.localPosition = new Vector3(2.80f, 0, -2);
                    door.transform.localRotation = Quaternion.Euler(0, 0, 270);
                    break;
                case 2: // Bottom
                    door.transform.localPosition = new Vector3(0, -1.80f, -2);
                    door.transform.localRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case 3: // Left
                    door.transform.localPosition = new Vector3(-2.80f, 0, -2);
                    door.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;
            }

            // Set color to background
            Vector2 normalDirection = new Vector2(door.transform.position.x == 0 ? 0 : Mathf.Sign(door.transform.position.x),
                                                    door.transform.position.y == 0 ? 0 : Mathf.Sign(door.transform.position.y));
            Vector2 pixel = new Vector2(normalDirection.x * bg.width / 2 * 0.8f, normalDirection.y * bg.height / 2 * 0.8f);
            pixel += new Vector2(bg.width / 2, bg.height / 2);
            doorSprite.color = bg.GetPixel((int)pixel.x, (int)pixel.y);

            // If the door is an entrance
            if (i != 0) {
                door.tag = "Goal";
                Door doorScript = door.AddComponent<Door>();
                doorScript.world = this;
            }
            // Else if it's a spawn point
            else {
                door.tag = "Spawn";
                door.GetComponent<Animator>().SetTrigger("Shrink");
            }

            // Pick a new different location
            int newPos = Random.Range(0, 3);
            while (newPos == pos)
                newPos = Random.Range(0, 3);
            pos = newPos;
        }
    }

    public void ChangeRoom(char id, bool spawnDoors = false, bool spawnEnemies = true) {
        this.roomToLoad = id;
        this.willSpawnDoors = spawnDoors;
        this.willSpawnEnemies = spawnEnemies;

        roomsLeftToVisit.Remove(id);

        Destroy(GameObject.FindGameObjectWithTag("Spawn"));
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);
        sceneAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeOutComplete() {
        LoadRoom(roomToLoad, willSpawnDoors, willSpawnEnemies);
        sceneAnimator.SetTrigger("FadeIn");
        Unfreeze();
    }

    public void MovePlayerToSpawn() {
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, -1);
    }

    public void ChangePlayerMovement(char scriptId) {
        if(currentPlayerMovement != null) {
            Destroy(player.GetComponent(currentPlayerMovement));
        }
        var script = System.Type.GetType("Move" + scriptId);
        currentPlayerMovement = script;
        player.AddComponent(script);
    }

    public void ChangePlayerAttack(char scriptId) {
        if (currentPlayerAttack != null) {
            Destroy(player.GetComponent(currentPlayerAttack));
        }
        var script = System.Type.GetType("Attack" + scriptId);
        currentPlayerAttack = script;
        player.AddComponent(script);
    }

    public void OnEnemyDied(GameObject enemy) {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length - 1 == 0) //-1 cause the enemy that just died isn't destroyed yet
            GameObject.FindGameObjectWithTag("Goal").GetComponent<Animator>().SetTrigger("Expand");
    }

    public void OnEnteredDoor() {
        if (CheckWin()) {
            ChangeRoom('F', false, false);
        } else {
            DimAndAskQuestion();
        }
    }

    public void DimAndAskQuestion() {
        sceneAnimator.SetTrigger("Dim");
    }

    public void OnDimComplete() {
        PromptQuestion(CurrentRoom == 'A' ? 0 : -1);
    }

    public void PromptQuestion(int idx = -1){
        Question q;
        if (idx != -1) {
            q = dialogs.PickAt(idx);
        } else {
            // Get a random question
            q = dialogs.PickRandom();
        }
        dialogView.ShowQuestion(q, skippable: false);
    }

    public bool CheckWin() {
        return (roomsLeftToVisit.Count == 0 || dialogs.questions.Count == 0);
    }
}
