using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance = null;

    private bool loading;
    private bool[] accessibleLevels;

    void Awake() {
        if (instance == null) {
            instance = this;
            instance.Init();
            DontDestroyOnLoad(gameObject);
        } else {
            instance.Init();
            Destroy(gameObject);
            return;
        }
    }

    void Init() {
        loading = false;

        if (accessibleLevels == null) {
            accessibleLevels = new bool[SceneManager.sceneCountInBuildSettings];
            accessibleLevels[0] = true;
            accessibleLevels[1] = true;
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

        accessibleLevels[index] = true;

        // TODO: Also track connections
        SceneManager.LoadScene(index);

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

    public bool IsLevelIndexAccessible(int index) {
        return accessibleLevels[index];
    }
}
