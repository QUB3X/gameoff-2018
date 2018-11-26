using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    // Player Movement Variables
    public Rigidbody2D rb;
    public Animator anim;
    public World world;
    public float speed;
    public float lerpTime;

    // Quick test, bad idea
    public float newMoveTime = 1.2f;
    private float elapsedTime = 0;

    [HideInInspector]
    public float moveX = 0;
    [HideInInspector]
    public float moveY = 0;
    [HideInInspector]
    public Vector2 v;

    private void Update() {
        if(elapsedTime > newMoveTime) {
            moveX = Mathf.Sign(Random.Range(-1,1)) * Time.deltaTime * speed;
            moveY = Mathf.Sign(Random.Range(-1,1)) * Time.deltaTime * speed;
            v = new Vector2(moveX, moveY);
            elapsedTime = 0;
        }

        // Animate enemy
        if (!world.IsFrozen) {
            anim.SetBool("enemyIsMoving", v.magnitude > 0);
        }

        elapsedTime += Time.deltaTime;
    }

    public void AddMovement(char scriptId) {
        var script = System.Type.GetType("EnemyMove" + scriptId);
        gameObject.AddComponent(script);
    }

    // Debug
    private void Start() {
     AddMovement('A');
    }
}
