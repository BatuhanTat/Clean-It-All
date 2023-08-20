using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject levelPanel;
    [SerializeField] ButtonStateHandler buttonStateHandler;


    //float levelLoadDelay = 1f;
    //string sceneName;

    public void LevelPanel()
    {
        levelPanel.SetActive(!levelPanel.activeSelf);
        buttonStateHandler.SetLevelButtons(PlayerPrefs.GetInt("UnlockedLevels"));
        GameManager.instance.inMenu = levelPanel.activeSelf;
    }

    public void SettingsPanel()
    {
        Debug.Log("Settings worked");
    }

    public void SelectLevel(Button button)
    {      
        GameManager.instance.LoadLevel(button.name);
        // Do something with the buttonName, such as printing it
        Debug.Log("Clicked button name: " + button.name);     
        LevelPanel();

        // Get the name of the clicked button's GameObject
        //sceneName = button.name;
        //Invoke(nameof(LoadDelayedScene), levelLoadDelay);
    }

    //private void LoadDelayedScene()
    //{
    //    SceneManager.LoadScene(sceneName);
    //}
}
