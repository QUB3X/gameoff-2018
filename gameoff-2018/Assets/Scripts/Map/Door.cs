using UnityEngine;

public class Door : MonoBehaviour {

    public World world;

    private void Start() {
        if (world == null) {
            world = GameObject.Find("/GameController").GetComponent<World>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            world.Freeze();
            world.EnteredDoor();
        }
    }
}
