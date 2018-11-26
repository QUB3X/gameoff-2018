using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        // Go forward
        rb.velocity = transform.up * speed;
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        // TODO: Apply damage
        Destroy(gameObject);
    }
}
