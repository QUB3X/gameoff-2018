using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    
    public Animator anim;
    public Transform firePoint;
    public GameObject bullet;
    
    [HideInInspector]
    public bool isShooting;

    private float angle = 0;

    // Quick test, bad idea
    public float newAttackTime = 1.0f;
    private float elapsedTime = 0;

	// Update is called once per frame
	void Update () {
        
        if(elapsedTime > newAttackTime) {
            Transform weapon = transform.GetChild(0);
            angle = + Random.Range(0, 360);
            weapon.transform.rotation = Quaternion.Euler(0, 0, angle);

            isShooting = Random.Range(0.0f, 1.0f) > 0.5f ? true : false;

            elapsedTime = 0;
        }

        elapsedTime += Time.deltaTime;
	}

    public void AddShooting(char scriptId) {
        var script = System.Type.GetType("EnemyAttack" + scriptId);
        gameObject.AddComponent(script);
    }

    // Debug
    void Start() {
        AddShooting('A');
    }
}
