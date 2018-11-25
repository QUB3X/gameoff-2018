using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackA : MonoBehaviour {

    private PlayerShoot ps;

    private void Start() {
        ps = GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update () {
        if (ps.isShooting) {
            ps.anim.SetTrigger("shoot");
            // Spawn bullet
            Instantiate(ps.bullet, ps.firePoint.position, ps.firePoint.rotation);
        }
    }
}
