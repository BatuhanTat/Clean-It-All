using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject levelPanel;
    [SerializeField] ButtonStateHandler buttonStateHandler;


    float levelLoadDelay = 1f;
    string sceneName;

    public void LevelPanel()
    {
        levelPanel.SetActive(!levelPanel.activeSelf);
        buttonStateHandler.SetLevelButtons(PlayerPrefs.GetInt("Level"));
        GameManager.instance.inMenu = levelPanel.activeSelf;
    }

    public void SettingsPanel()
    {
        Debug.Log("Settings worked");
    }

    public void SelectLevel(Button button)
    {
        // Get the name of the clicked button's GameObject
        sceneName = button.name;

        // Do something with the buttonName, such as printing it
        Debug.Log("Clicked button name: " + sceneName);
        Invoke(nameof(LoadDelayedScene), levelLoadDelay);
        LevelPanel();
    }

    private void LoadDelayedScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
