using UnityEngine;

public class Door : MonoBehaviour {
    
    public World world;

    private void Start() {
        if(world == null) {
            world = GameObject.Find("/GameController").GetComponent<World>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.tag == "Player") {
            Debug.Log("Door hit by: " + collision.name);
            
            world.Freeze();
            // Temporarely just load the next room.
            world.PromptQuestion();
            /*char nextRoom = (char)(world.CurrentRoom + 1);
            bool shouldSpawnDoors = (nextRoom == 'F' ? false : true);
            world.ChangeRoom(nextRoom, shouldSpawnDoors);*/
        }
    }
}
