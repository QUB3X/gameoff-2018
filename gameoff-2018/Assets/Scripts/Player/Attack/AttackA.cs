using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackA : MonoBehaviour {

    public int Cooldown = 1;

    private PlayerShoot ps;
    private float cooldown = 0;

    private void Start() {
        ps = GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update () {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0) {
            if (ps.isShooting) {
                cooldown = Cooldown;
                ps.anim.SetTrigger("shoot");
                // Spawn bullet
                GameObject bullet = Instantiate(ps.bullet, ps.firePoint.position, ps.firePoint.rotation);
                // What if bullet collides before its damage is set? Unlikely but could happen i guess?
                bullet.GetComponent<Bullet>().damage = ps.stats.damage;
            }
        }
    }
}
