using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance = null;

    private bool loading;
    private bool[] accessibleLevels;

    private Dictionary<int, HashSet<int>> completedConnections;

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

        if (completedConnections == null) {
            completedConnections = new Dictionary<int, HashSet<int>>();
        }

        if (TransitionManager.instance != null) {
            StartCoroutine(TransitionManager.instance.Fade(true));
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

        int completedIndex = SceneManager.GetActiveScene().buildIndex;
        if (!completedConnections.ContainsKey(completedIndex)) {
            completedConnections.Add(completedIndex, new HashSet<int>());
        }
        completedConnections[completedIndex].Add(index);

        StartCoroutine(Load(index));
    }

    public void RestartLevel() {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadTitle() {
        StartCoroutine(Load(0));
    }

    IEnumerator Load(int index) {
        loading = true;
        yield return StartCoroutine(TransitionManager.instance.Fade(false));
        SceneManager.LoadScene(index);
    }

    public bool IsLevelIndexAccessible(int index) {
        return accessibleLevels[index];
    }

    public bool IsConnectionVisible(int fromIndex, int toIndex) {
        return completedConnections.ContainsKey(fromIndex) && completedConnections[fromIndex].Contains(toIndex);
    }
}
