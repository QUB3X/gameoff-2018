using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Player Movement Variables
    public Rigidbody2D rb;
    public float speed;
    public float lerpTime;

	// Use this for initialization
	void Start () {
		if(rb == null) {
            rb = GameObject.Find("/Player").GetComponent<Rigidbody2D>();
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // moveX and moveY are a value between -1 and 1.
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Move player
        Vector2 v = new Vector2(moveX, moveY);
        
        // Don't go faster diagonally
        if (v.magnitude > 1)
            v.Normalize();

        rb.velocity = Vector2.Lerp(rb.velocity, v*speed, lerpTime);
    }
}
