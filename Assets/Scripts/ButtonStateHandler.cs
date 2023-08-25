using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonStateHandler : MonoBehaviour
{
    [SerializeField] List<Button> buttonList;
    
    public void SetLevelButtons(int levelProgress)
    {
        CheckProgress(levelProgress);
        SetObjectsAlpha();
    }

    private void CheckProgress(int levelProgress)
    {
        // levelProgress variable starts from 0.
        for (int i = 0; i < buttonList.Count; i++)
        {
            if(i < levelProgress)
            {
                buttonList[i].interactable = true;
            }
            else
            {
                buttonList[i].interactable = false;
            }
        }
    }

    private void SetObjectsAlpha()
    {
        foreach (Button button in buttonList)
        {
            Transform parent = button.gameObject.transform;
            Image mostInnerImage = parent.GetChild(0).GetChild(0).GetComponent<Image>();
            if (!button.interactable)
            {               
                Debug.Log("Button child count: " + parent.childCount);
               
                if (mostInnerImage != null)
                {
                    Debug.Log("mostInnerImage " + mostInnerImage);                
                    mostInnerImage.enabled = false;
                }

            }
            else
            {                             
                if (mostInnerImage != null)
                {                    
                    mostInnerImage.enabled = true;
                }
            }
        }
    }

}
