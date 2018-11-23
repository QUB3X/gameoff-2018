using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public float timeScaleDelta = 0.05f;
    public bool IsFrozen { get; private set; }

    void FixedUpdate() {
        if (IsFrozen) {
            Time.timeScale = Mathf.Max(0, Time.timeScale - timeScaleDelta);
        } else {
            Time.timeScale = 1;
        }
    }

    public void Freeze() {
        this.IsFrozen = true;
    }
}
