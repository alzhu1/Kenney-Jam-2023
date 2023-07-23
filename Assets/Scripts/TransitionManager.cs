using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour {
    public static TransitionManager instance = null;

    private Image transitionImage;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        transitionImage = GetComponentInChildren<Image>();
    }

    void Start() {
        // Done once only
        transitionImage.color = Color.clear;
    }

    public IEnumerator Fade(bool fadeIn) {
        Color start = fadeIn ? Color.black : Color.clear;
        Color end = fadeIn ? Color.clear : Color.black;

        float time = 0f;
        while (time <= 1f) {
            transitionImage.color = Color.Lerp(start, end, time);
            yield return null;
            time += Time.deltaTime;
        }
        transitionImage.color = end;
    }
}
