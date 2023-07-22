using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelName : MonoBehaviour {
    [SerializeField] private float showTime;

    private Text levelNameText;

    void Awake() {
        levelNameText = GetComponent<Text>();
    }

    void Start() {
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText() {
        Color currTextColor = levelNameText.color;
        Color target = currTextColor;
        target.a = 0;

        // Wait a few seconds before fading
        yield return new WaitForSeconds(showTime);

        float time = 0;
        while (time < 1f) {
            levelNameText.color = Color.Lerp(currTextColor, target, time);
            yield return null;
            time += Time.deltaTime;
        }
        levelNameText.enabled = false;
    }
}
