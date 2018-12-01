﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public World world;

    private int _hitpoints;
	public int Hitpoints {
        get { return _hitpoints; }
        private set {
            if (value < 0) _hitpoints = 0;
            else if (value > maxHitpoints) _hitpoints = maxHitpoints;
            else _hitpoints = value;
        }
    }

	public int damage = 1;

	public int maxHitpoints = 10;

	// Use this for initialization
	void Start () {
		Hitpoints = maxHitpoints;
	}
	
	// Update is called once per frame
	void Update () {
		if(Hitpoints == 0) {
			Die();
		}
	}

	public void TakeDamage(int damage) {
        Hitpoints -= damage;
        // Play sound/animation...
    }

	public void Die() {
        // Play animation,
        // show gameover screen... 
        world.Restart();
	}
}
