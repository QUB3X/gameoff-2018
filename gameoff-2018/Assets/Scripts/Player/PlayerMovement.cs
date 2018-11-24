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

    float moveX = 0;
    float moveY = 0;

    // Use this for initialization
    void Start () {
	}

    private void FixedUpdate() {
        // moveX and moveY are a value between -1 and 1.
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // Move and animate player
        if (!world.IsFrozen) {
            Vector2 v = new Vector2(moveX, moveY);
            // Don't go faster diagonally
            if (v.magnitude > 1)
                v.Normalize();
            rb.velocity = Vector2.Lerp(rb.velocity, v * speed, lerpTime);

            anim.SetBool("playerIsMoving", v.magnitude > 0);
            anim.SetFloat("mousePosition", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - rb.transform.position.y);
        }
    }
}
