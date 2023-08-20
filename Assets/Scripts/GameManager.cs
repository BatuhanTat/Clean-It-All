using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public bool canClean { get; set; } = false;
    public bool inMenu { get; set; } = false;

    [HideInInspector] public int levelCount = 0;
    private int levelProgress = 0;

    // Method to increment levelProgress by 1
    public void IncrementLevelProgress()
    {
        int currentSceneNumber = 0;
        int.TryParse(SceneManager.GetActiveScene().name, out currentSceneNumber);

        Debug.Log("Level count: " + levelCount);
        Debug.Log("Level progression before: " + levelProgress);
        //levelProgress = (levelProgress < levelCount) ? levelProgress + 1 : levelProgress;
       
        if(levelProgress < levelCount && PlayerPrefs.GetInt("Level") < currentSceneNumber )
       
        SetLevelProgression();
        Debug.Log("Level progression after: " + levelProgress);
        Debug.Log("Level progression playerPref after: " + PlayerPrefs.GetInt("Level"));
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        SetLevelProgression();
        //levelProgress = PlayerPrefs.GetInt("Level");
        Debug.Log("levelProgress prefs " + PlayerPrefs.GetInt("Level"));
    }


    private void SetLevelProgression()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            int oldLevel = PlayerPrefs.GetInt("Level");
            if (levelProgress > oldLevel)
            {
                PlayerPrefs.SetInt("Level", levelProgress);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("Level", levelProgress);
            PlayerPrefs.Save();
        }
    }

}
