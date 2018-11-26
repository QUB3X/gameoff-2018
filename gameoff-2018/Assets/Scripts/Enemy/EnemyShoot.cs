using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    
    public Animator anim;
    public Transform firePoint;
    public GameObject bullet;
    
    [HideInInspector]
    public bool isShooting;
    [HideInInspector]
    public float targetAngle = 0;
    private GameObject player;

    // Quick test, bad idea
    public float newAttackTime = 1.0f;
    private float elapsedTime = 0;

	// Update is called once per frame
    void FixedUpdate () {
        if (elapsedTime > newAttackTime) {
            targetAngle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
            isShooting = Random.Range(0.0f, 1.0f) > 0.5f ? true : false;
            elapsedTime = 0;
        }

        elapsedTime += Time.fixedDeltaTime;
    }

    public void AddShooting(char scriptId) {
        var script = System.Type.GetType("EnemyAttack" + scriptId);
        gameObject.AddComponent(script);
    }

    // Debug
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        AddShooting('A');
    }
}
