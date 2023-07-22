using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    [SerializeField] private int nextLevelIndex;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == 6) { // Player layer
            AudioManager.instance.Play("LevelWin");
            // TODO: Probably add some transition here
            LevelManager.instance.LoadLevel(nextLevelIndex);
        }
    }
}
