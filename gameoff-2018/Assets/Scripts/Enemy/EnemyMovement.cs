using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    // Player Movement Variables
    public Rigidbody2D rb;
    public Animator anim;
    public World world;

    // Quick test, bad idea
    public float newMoveTime = 1.2f;
    private float elapsedTime = 0;

    [HideInInspector]
    public float moveX = 0;
    [HideInInspector]
    public float moveY = 0;
    [HideInInspector]
    public Vector2 v;

    void Start() {
        if(world == null) {
            world = GameObject.Find("/GameController").GetComponent<World>();
        }
        AddMovement('A');
    }

    void FixedUpdate() {
        if(elapsedTime > newMoveTime) {
            moveX = Mathf.Sign(Random.Range(-1,1));
            moveY = Mathf.Sign(Random.Range(-1,1));
            v = new Vector2(moveX, moveY);
            elapsedTime = 0;
        }

        // Animate enemy
        if (!world.IsFrozen) {
            anim.SetBool("enemyIsMoving", v.magnitude > 0);
        }

        elapsedTime += Time.fixedDeltaTime;
    }

    public void AddMovement(char scriptId) {
        var script = System.Type.GetType("EnemyMove" + scriptId);
        gameObject.AddComponent(script);
    }
}
