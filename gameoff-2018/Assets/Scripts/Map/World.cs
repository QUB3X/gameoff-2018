using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public float timeScaleDelta = 0.05f;
    public bool IsFrozen { get; private set; }
    public char CurrentRoom { get; private set; }
    
    public Animator sceneAnimator;

    public GameObject player;

    private Dictionary<char, GameObject> maps = new Dictionary<char, GameObject>();
    private GameObject doorPrefab;
    private char roomToLoad;
    private bool spawnDoors;

    // Used to remove player movement
    private System.Type currentPlayerMovement;
    private System.Type currentPlayerAttack;

    // Used to show UI
    private Dialog dialog;

    // Dictionary of all dialog lines:
    private Dictionary<string, string> dialogs;

    private void Start() {
        // Use this to init all the dialog lines contained in Resources/dialogs.txt
        dialogs = DialogParser.Parse();

        dialog = FindObjectOfType<Dialog>();

        foreach(GameObject o in Resources.LoadAll<GameObject>("Rooms")) {
            maps.Add(o.name[0], o);
        }
        doorPrefab = Resources.Load<GameObject>("Door");
        LoadRoom('A');
        ChangePlayerMovement('A');
        ChangePlayerAttack('A');

        dialog.Show(dialogs["start_game"], 0.3f);
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
    }

    public void Unfreeze() {
        this.IsFrozen = false;
    }

    public void LoadRoom(char id, bool spawnDoors = false) {
        // Delete any room object instantiated.
        Destroy(GameObject.FindGameObjectWithTag("Room"));

        // Spawn new room
        GameObject room = Instantiate(maps[id]);
        room.name = id.ToString();

        if (spawnDoors)
            SpawnDoors(room);

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
                Door doorScript = door.AddComponent<Door>();
                doorScript.world = this;
                door.GetComponent<Animator>().SetTrigger("Expand");
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

    public void ChangeRoom(char id, bool spawnDoors = false) {
        this.roomToLoad = id;
        this.spawnDoors = spawnDoors;
        Destroy(GameObject.FindGameObjectWithTag("Spawn"));
        sceneAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeOutComplete() {
        LoadRoom(roomToLoad, spawnDoors);
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
}
