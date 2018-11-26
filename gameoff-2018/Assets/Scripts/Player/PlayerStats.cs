using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int hitpoints {
		get {
			return hitpoints;
		}
		set {
			if(value < 0) hitpoints = 0;
			if(value > maxHitpoints) hitpoints = maxHitpoints;
		}
	}

	public int damage;

	public int maxHitpoints = 10;

	// Use this for initialization
	void Start () {
		hitpoints = maxHitpoints;
	}
	
	// Update is called once per frame
	void Update () {
		if(hitpoints == 0) {
			Die();
		}
	}

	public void TakeDamage(int damage) {
		hitpoints -= damage;
		// Play sound/animation...
	}

	public void Die() {
		// Play animation,
		// show gameover screen... 
	}
}
