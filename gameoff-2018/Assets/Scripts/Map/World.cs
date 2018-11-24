using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public float timeScaleDelta = 0.05f;
    public bool IsFrozen { get; private set; }
    public char CurrentRoom { get; private set; }

    public Camera cam;
    public Animator sceneAnimator;
    public Animator playerAnimator;
    public Music musicManager;

    private Dictionary<char, GameObject> maps = new Dictionary<char, GameObject>();
    private GameObject doorPrefab;
    private char roomToLoad;
    private bool spawnDoors;

    private void Start() {
        foreach(GameObject o in Resources.LoadAll<GameObject>("Rooms")) {
            maps.Add(o.name[0], o);
        }
        doorPrefab = Resources.Load<GameObject>("Door");
        LoadRoom('A');
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

        if (spawnDoors) {
            // Generate 2 doors
            int pos = Random.Range(0, 3);
            Texture2D bg = room.transform.Find("Background").GetComponent<SpriteRenderer>().sprite.texture;

            for (int i = 0; i < 2; i++) {
                GameObject door = Instantiate(doorPrefab);
                SpriteRenderer doorSprite = door.GetComponent<SpriteRenderer>();
                door.transform.parent = room.transform;

                switch (pos) {
                    case 0: // Top
                        door.transform.localPosition = new Vector3(0, 2.36f, -2);
                        door.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 1: // Right
                        door.transform.localPosition = new Vector3(3.36f, 0, -2);
                        door.transform.localRotation = Quaternion.Euler(0, 0, 270);
                        break;
                    case 2: // Bottom
                        door.transform.localPosition = new Vector3(0, -2.36f, -2);
                        door.transform.localRotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case 3: // Left
                        door.transform.localPosition = new Vector3(-3.36f, 0, -2);
                        door.transform.localRotation = Quaternion.Euler(0, 0, 90);
                        break;
                }

                // Set color to background
                Vector2 normalDirection = new Vector2(door.transform.position.x == 0 ? 0 : Mathf.Sign(door.transform.position.x),
                                                      door.transform.position.y == 0 ? 0 : Mathf.Sign(door.transform.position.y));
                Vector2 pixel = new Vector2(normalDirection.x * bg.width / 2 * 0.8f, normalDirection.y * bg.height / 2 * 0.8f);
                pixel += new Vector2(bg.width / 2, bg.height / 2);
                doorSprite.color = bg.GetPixel((int)pixel.x, (int)pixel.y);

                // If we can enter the door, attach script
                if (i != 0) {
                    Door doorScript = door.AddComponent<Door>();
                    doorScript.world = this;
                } else {
                    MovePlayerToSpawn(door);
                }

                // Pick a new different location
                int newPos = Random.Range(0, 3);
                while (newPos == pos)
                    newPos = Random.Range(0, 3);
                pos = newPos;
            }
        } else {
            // We already have a door to spawn off from
            GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");
            MovePlayerToSpawn(spawn);
        }

        this.CurrentRoom = id;
        musicManager.LoadMusic(room.GetComponentInChildren<AudioSource>());
    }

    public void FadeToRoom(char id, bool spawnDoors = false) {
        this.roomToLoad = id;
        this.spawnDoors = spawnDoors;
        sceneAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeOutComplete() {
        LoadRoom(roomToLoad, spawnDoors);
        sceneAnimator.SetTrigger("FadeIn");
        Unfreeze();
    }

    public void MovePlayerToSpawn(GameObject spawn) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(spawn.transform.position.x * 0.8f, spawn.transform.position.y * 0.7f, -1);
    }
}
