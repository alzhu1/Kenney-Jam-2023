using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance = null;

    private bool loading;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            instance.loading = false;
            Destroy(gameObject);
            return;
        }
    }

    void Update() {
        if (loading) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            RestartLevel();
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            LoadTitle();
        }
    }

    public void LoadLevel(int index) {
        if (loading) {
            return;
        }

        if (index >= SceneManager.sceneCountInBuildSettings) {
            LoadTitle();
        } else {
            SceneManager.LoadScene(index);
        }
        loading = true;
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        loading = true;
    }

    public void LoadTitle() {
        SceneManager.LoadScene(0);
        loading = true;
    }
}
