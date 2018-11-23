using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Player Movement Variables
    public Rigidbody2D rb;
    public Animator anim;
    public float speed;
    public float lerpTime;

    float moveX = 0;
    float moveY = 0;

    // Use this for initialization
    void Start () {
	}

    private void Update() {
        // moveX and moveY are a value between -1 and 1.
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // Animate player
        int dir = 0;
        if (moveY > 0) dir = 1;
        if (moveY < 0) dir = -1;
        // Handle side movement
        int oldDir = anim.GetInteger("dirY");
        if (moveX != 0 && dir == 0) {
            dir = oldDir;
            var currentState = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("IdleBack")) dir = 1;
            else if (currentState.IsName("IdleFront")) dir = -1;
        }
        
        anim.SetInteger("dirY", dir);
    }
    
    void FixedUpdate() {
        // Move player
        Vector2 v = new Vector2(moveX, moveY);
        // Don't go faster diagonally
        if (v.magnitude > 1)
            v.Normalize();
        rb.velocity = Vector2.Lerp(rb.velocity, v * speed, lerpTime);
    }
}
