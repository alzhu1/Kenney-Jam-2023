using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadLevel(int index) {
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevel() {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((index + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadTitle() {
        SceneManager.LoadScene(0);
    }
}
