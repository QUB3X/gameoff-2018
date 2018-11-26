using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackA : MonoBehaviour {

    private EnemyShoot es;

    private void Start() {
        es = GetComponent<EnemyShoot>();
    }

    // Update is called once per frame
    void Update () {
        if (es.isShooting) {
            es.anim.SetTrigger("shoot");
            // Spawn bullet
            Instantiate(es.bullet, es.firePoint.position, es.firePoint.rotation);
        }
    }
}
