using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    void Start() {
        
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == 6) { // Player layer
            Debug.Log("Should win!");
            LevelManager.instance.LoadNextLevel();
        }
    }
}
