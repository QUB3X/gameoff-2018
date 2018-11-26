using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveA : MonoBehaviour {

    private PlayerMovement pm;
    public float speed = 1.4f;
    public float lerpTime = 0.3f;

	// Use this for initialization
	void Start () {
        pm = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        // Don't go faster diagonally
        if (pm.v.magnitude > 1)
            pm.v.Normalize();
        pm.rb.velocity = Vector2.Lerp(pm.rb.velocity, pm.v * speed, lerpTime);
    }
}
