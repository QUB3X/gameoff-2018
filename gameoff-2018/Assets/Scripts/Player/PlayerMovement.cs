﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Player Movement Variables
    public Rigidbody2D rb;
    public Animator anim;
    public World world;

    [HideInInspector]
    public float moveX = 0;
    [HideInInspector]
    public float moveY = 0;
    [HideInInspector]
    public Vector2 v;

    private void Update() {
        // moveX and moveY are a value between -1 and 1.
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        v = new Vector2(moveX, moveY);

        // Animate player
        if (!world.IsFrozen) {
            anim.SetBool("playerIsMoving", v.magnitude > 0);
        }
    }
}
