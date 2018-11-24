using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public Music music;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var shoot = Input.GetMouseButtonDown(0) || Input.GetKeyDown("z");
        if (shoot) {
            if (music.IsOnTime() == 0) {
                Debug.Log("On time!");
            } else {
                //Debug.Log("GIT GUD LUL");
            }
        }
	}
}
