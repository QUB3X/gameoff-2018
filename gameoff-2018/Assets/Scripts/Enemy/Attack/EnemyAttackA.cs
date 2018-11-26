using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackA : MonoBehaviour {

    private EnemyShoot es;
    private float angle = 0;
    private float lerpSpeed = 0.05f;

    private void Start() {
        es = GetComponent<EnemyShoot>();
    }

    // Update is called once per frame
    void Update () {
        Transform weapon = transform.GetChild(0);
        angle = Mathf.Lerp(angle, es.targetAngle, lerpSpeed);
        weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        //anim.SetFloat("lookDirection", angle > 180 ? 1.0f : -1.0f);

        if (es.isShooting) {
            es.anim.SetTrigger("shoot");
            // Spawn bullet
            Instantiate(es.bullet, es.firePoint.position, es.firePoint.rotation);
            es.isShooting = false;
        }
    }
}
