using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Player Movement Variables
    public Rigidbody2D rb;
    public float speed = 1;
    public float lerpTime = 0.5f;
	// Use this for initialization
	void Start () {
		if(rb == null) {
            rb = GameObject.Find("/Player").GetComponent<Rigidbody2D>();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // moveX and moveY are a value between -1 and 1.
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Move player
        rb.velocity = new Vector2(
            Mathf.Lerp(0, moveX * speed, lerpTime),
            Mathf.Lerp(0, moveY * speed, lerpTime));

        // Make player face mouse cursor
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        float angle = Mathf.Atan2(mousePos.y - playerPos.y, mousePos.x - playerPos.x) * Mathf.Rad2Deg - 90;
        // Lerp it maybe?
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
