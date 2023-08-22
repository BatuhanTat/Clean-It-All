using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public bool canClean { get; set; } = false;
    public bool inMenu { get; set; } = false;

    private int unlockedLevels; // Initially, only the first level is unlocked

    float levelLoadDelay = 1f;
    int levelCount = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        levelCount = SceneManager.sceneCountInBuildSettings;
        Debug.Log("Total number of scenes: " + levelCount);

        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0); // Load the unlockedLevels value from PlayerPrefs
    }


    public void CompleteLevel()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Level Index " + levelIndex);
        if (levelIndex == unlockedLevels)
        {
            Debug.Log("Level Index " + levelIndex + "  unlockedLevels: " + unlockedLevels);
            if (levelIndex == 0 || IsPreviousLevelCompleted(levelIndex))
            {
                Debug.Log("unlockedLevels " + unlockedLevels);
                unlockedLevels++;
                Debug.Log("unlockedLevels " + unlockedLevels);
                PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);

                PlayerPrefs.SetInt("Level_" + levelIndex, 1);
                PlayerPrefs.Save(); // Optional: Manually save PlayerPrefs
                
            }
        }
        LoadNextlevel(levelIndex);
        UIManager.instance.PlayLevelCompleteParticles();
        UIManager.instance.ToggleIngameUI(false);
        UIManager.instance.ToggleLevelCompleteText(true);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UIManager.instance.UpdateRenderCamera();
        UIManager.instance.UpdateLevelTexts(SceneManager.GetActiveScene().buildIndex + 1);
        UIManager.instance.ToggleIngameUI(true);
        UIManager.instance.ToggleLevelCompleteText(false);
    }
    public void LoadLevel(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            //Invoke(nameof(LoadDelayedScene), levelLoadDelay);
            StartCoroutine(LoadingDelay(sceneName));
        }
    }

    private IEnumerator LoadingDelay(object arg)
    {
        yield return new WaitForSeconds(levelLoadDelay);

        if(arg is string)
        {
            SceneManager.LoadScene((string)arg);
        }
        else if(arg is int)
        {
            SceneManager.LoadScene((int)arg);
        }
    }

    private void LoadNextlevel(int levelIndex)
    {
        if (levelIndex + 1 <= PlayerPrefs.GetInt("UnlockedLevels") && levelIndex + 1 < levelCount)
        {
            StartCoroutine(LoadingDelay(levelIndex + 1));
        }
        if(levelIndex == levelCount - 1)
        {
            // On completion of final level load level 1, buildindex 0.
            StartCoroutine(LoadingDelay(0));
        }
    }

    private bool IsPreviousLevelCompleted(int levelIndex)
    {
        if (levelIndex == 0) // First level has no previous level
        {
            return true;
        }

        return PlayerPrefs.GetInt("Level_" + (levelIndex - 1), 0) == 1;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
