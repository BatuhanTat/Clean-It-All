using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject ingameUI;
    [SerializeField] ButtonStateHandler buttonStateHandler;
    [Space]
    [SerializeField] ParticleSystem[] particleSystems;
    [Space]
    [SerializeField] TextMeshProUGUI topBarLevelText;
    [SerializeField] TextMeshProUGUI completeLevelText;

    private GameObject UICamera;

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

    public void LevelPanel()
    {
        levelPanel.SetActive(!levelPanel.activeSelf);
        buttonStateHandler.SetLevelButtons(PlayerPrefs.GetInt("UnlockedLevels"));
        GameManager.instance.inMenu = levelPanel.activeSelf;
    }

    public void SettingsPanel()
    {
        Debug.Log("Settings worked");
        PlayLevelCompleteParticles();
    }

    public void SelectLevel(Button button)
    {
        GameManager.instance.LoadLevel(button.name);
        // Do something with the buttonName, such as printing it
        Debug.Log("Clicked button name: " + button.name);     
        //LevelPanel();
        //ToggleIngameUI(false);
        StartCoroutine(ToggleUIDelay(false));
    }

    public void PlayLevelCompleteParticles()
    {
        foreach (var particle in particleSystems)
        {
            particle.Play();
        }
    }

    public void UpdateLevelTexts(int levelNumber)
    {
        // Level 999
        topBarLevelText.SetText("Level " + levelNumber);
        // Level 999
        // Completed
        completeLevelText.SetText(topBarLevelText.text + "\nCompleted");
    }

    public void UpdateRenderCamera()
    {
        UICamera = GameObject.Find("UI Camera");
        Debug.Log("UICamera " + UICamera);
        if (UICamera != null)
        {
            Debug.Log("Canvas worldcamera/ renderCamera: " + gameObject.GetComponent<Canvas>().worldCamera);
            gameObject.GetComponent<Canvas>().worldCamera = UICamera.GetComponent<Camera>();
            Debug.Log("Canvas worldcamera/ renderCamera: " + gameObject.GetComponent<Canvas>().worldCamera);
        }
    }

    public void ToggleIngameUI(bool state)
    {
        ingameUI.SetActive(state);            
    }

    public void ToggleLevelCompleteText(bool state)
    {
        completeLevelText.gameObject.SetActive(state);
    }

    private IEnumerator ToggleUIDelay(bool state)
    {
        yield return new WaitForSeconds(0.2f);
        ToggleIngameUI(state);
        LevelPanel();
    }
}
