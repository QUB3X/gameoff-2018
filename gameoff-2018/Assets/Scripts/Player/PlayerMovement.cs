using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Player Movement Variables
    public Rigidbody2D rb;
    public Animator anim;
    public World world;
    public float speed;
    public float lerpTime;

    [HideInInspector]
    public float moveX = 0;
    [HideInInspector]
    public float moveY = 0;

    private void Update() {
        // moveX and moveY are a value between -1 and 1.
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // Animate player
        if (!world.IsFrozen) {
            anim.SetBool("playerIsMoving", rb.velocity.magnitude > 0);
            anim.SetFloat("mousePosition", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - rb.transform.position.y);
        }
    }
}
