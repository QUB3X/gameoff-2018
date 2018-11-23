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
    }
}
