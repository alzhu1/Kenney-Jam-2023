using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour {
    [SerializeField] private GameObject connectionParent;
    [SerializeField] private Text congratsText;

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
        bool beatAllLevels = true;
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = LevelManager.instance.IsLevelIndexAccessible(i + 1);
            if (!buttons[i].interactable) {
                beatAllLevels = false;
            }
        }

        // Check connections
        bool foundAllConnections = true;
        Image[] connections = connectionParent.GetComponentsInChildren<Image>();
        foreach (Image connection in connections) {
            // Probably a nicer way to do this, but whatever
            string[] path = connection.name.Split(" -> ");
            int fromIndex = levelNameToIndex[path[0]];
            int toIndex = levelNameToIndex[path[1]];

            Color connectionColor = connection.color;
            if (!LevelManager.instance.IsLevelIndexAccessible(fromIndex)) {
                connectionColor.a = 0;
                foundAllConnections = false;
            } else if (LevelManager.instance.IsConnectionVisible(fromIndex, toIndex)) {
                connectionColor.a = 1;
            } else {
                foundAllConnections = false;
            }
            connection.color = connectionColor;
        }

        if (foundAllConnections) {
            congratsText.text = "You beat all levels and found all connections! Awesome!!";
        } else if (beatAllLevels) {
            congratsText.text = "You beat all levels! Nice!";
        } else {
            congratsText.enabled = false;
        }
    }

    public void LoadLevel(int index) {
        // Button will trigger this
        AudioManager.instance.Play("ButtonClick");
        LevelManager.instance.LoadLevel(index);
    }
}
