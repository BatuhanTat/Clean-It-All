using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject levelPanel;



    public void LevelPanel()
    {
        levelPanel.SetActive(!levelPanel.activeSelf);
        GameManager.instance.inMenu = levelPanel.activeSelf;
    }

    public void SettingsPanel()
    {
        Debug.Log("Setttings worked");
    }
}
