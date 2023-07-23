using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    [SerializeField] private int nextLevelIndex;

    private bool triggered;

    void OnTriggerEnter2D(Collider2D collider) {
        if (!triggered && collider.gameObject.layer == 6) { // Player layer
            AudioManager.instance.Play("LevelWin");
            LevelManager.instance.LoadLevel(nextLevelIndex);
            triggered = true;
        }
    }
}
