using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour {
    private Button[] buttons;

    void Awake() {
        // Should be ordered in scene index order
        buttons = GetComponentsInChildren<Button>();
    }

    void Start() {
        for (int i = 0; i < buttons.Length; i++) {
            Debug.Log(buttons[i]);
            Debug.Log($"{i}, {buttons.Length}");
            buttons[i].interactable = LevelManager.instance.IsLevelIndexAccessible(i + 1);
        }
    }

    public void LoadLevel(int index) {
        LevelManager.instance.LoadLevel(index);
    }
}
