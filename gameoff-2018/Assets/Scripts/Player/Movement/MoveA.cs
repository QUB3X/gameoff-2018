using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveA : MonoBehaviour {

    private PlayerMovement pm;

	// Use this for initialization
	void Start () {
        pm = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 v = new Vector2(pm.moveX, pm.moveY);
        // Don't go faster diagonally
        if (v.magnitude > 1)
            v.Normalize();
        pm.rb.velocity = Vector2.Lerp(pm.rb.velocity, v * pm.speed, pm.lerpTime);
    }
}
