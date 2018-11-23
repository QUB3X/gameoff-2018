using UnityEngine;

public class Door : MonoBehaviour {
    
    public World world;

    private void Start() {
        if(world == null) {
            world = GameObject.Find("/MapController").GetComponent<World>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        world.Freeze();
        // Temporarely just load the next room.
        char nextRoom = (char)(world.CurrentRoom + 1);
        bool shouldSpawnDoors = nextRoom == 'F' ? false : true;
        world.FadeToRoom(nextRoom, shouldSpawnDoors);
    }
}
