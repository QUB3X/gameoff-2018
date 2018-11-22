using UnityEngine;

public class Door : MonoBehaviour {
    
    public World world;

    private void Start() {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        world.Freeze();
    }
}
