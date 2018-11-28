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
            world.Freeze();
            if (world.CheckWin()) {
                world.ChangeRoom('F', false);
            } else {
                world.PromptQuestion(world.CurrentRoom == 'A' ? 0 : -1);
            }
        }
    }
}
