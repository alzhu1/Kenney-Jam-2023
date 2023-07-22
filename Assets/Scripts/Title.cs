using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour {
    [SerializeField] private GameObject connectionParent;

    private Button[] buttons;

    private static Dictionary<string, int> levelNameToIndex = new Dictionary<string, int>() {
        { "1", 1 },
        { "2", 2 },
        { "3", 3 },
        { "4a", 4 },
        { "4b", 5 },
        { "5a", 6 },
        { "5b", 7 },
        { "6a", 8 },
        { "6b", 9 },
        { "6c", 10 },
        { "7a", 11 },
        { "7b", 12 },
        { "7c", 13 },
        { "8a", 14 },
        { "8b", 15 },
        { "9a", 16 },
        { "9b", 17 },
        { "9c", 18 },
        { "9d", 19 },
        { "10", 20 },
    };

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

        // Check connections
        Image[] connections = connectionParent.GetComponentsInChildren<Image>();
        foreach (Image connection in connections) {
            // Probably a nicer way to do this, but whatever
            string[] path = connection.name.Split(" -> ");
            int fromIndex = levelNameToIndex[path[0]];
            int toIndex = levelNameToIndex[path[1]];

            Color connectionColor = connection.color;
            if (!LevelManager.instance.IsLevelIndexAccessible(fromIndex)) {
                connectionColor.a = 0;
            } else if (LevelManager.instance.IsConnectionVisible(fromIndex, toIndex)) {
                connectionColor.a = 1;
            }
            connection.color = connectionColor;
        }
    }

    public void LoadLevel(int index) {
        // Button will trigger this
        AudioManager.instance.Play("ButtonClick");
        LevelManager.instance.LoadLevel(index);
    }
}
