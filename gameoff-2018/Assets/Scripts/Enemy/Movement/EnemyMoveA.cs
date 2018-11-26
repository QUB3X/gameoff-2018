using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveA : MonoBehaviour {

    private EnemyMovement em;
    public float speed = 1f;
    public float lerpTime = 0.3f;

    // Use this for initialization
    void Start () {
        em = GetComponent<EnemyMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        // Don't go faster diagonally
        if (em.v.magnitude > 1)
            em.v.Normalize();
        em.rb.velocity = Vector2.Lerp(em.rb.velocity, em.v * speed, lerpTime);
    }
}
