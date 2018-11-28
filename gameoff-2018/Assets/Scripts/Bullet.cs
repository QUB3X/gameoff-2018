using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public Rigidbody2D rb;
    public int damage = 1;

	// Use this for initialization
	void Start () {
        // Go forward
        rb.velocity = transform.up * speed;
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
        }
        else if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
