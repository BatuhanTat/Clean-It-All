using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonStateHandler : MonoBehaviour
{
    [SerializeField] List<Button> buttonList;

    [SerializeField] float alphaReductionPercentage = 0.5f;

    private void Start()
    {
        GameManager.instance.levelCount = buttonList.Count;
    }

    public void SetLevelButtons(int levelProgress)
    {
        CheckScore(levelProgress);
        SetObjectsAlpha();
    }

    private void CheckScore(int levelProgress)
    {
        // levelProgress variable starts from 0.
        for (int i = 0; i < buttonList.Count; i++)
        {
            if(i <= levelProgress)
            {
                buttonList[i].enabled = true;
            }
            else
            {
                buttonList[i].enabled = false;
            }
        }
    }

    private void SetObjectsAlpha()
    {
        foreach (Button button in buttonList)
        {
            if (!button.interactable)
            {
                foreach (Graphic childObject in button.GetComponentsInChildren<Graphic>())
                {
                    Color childColor = childObject.color;
                    childColor.a *= (1 - alphaReductionPercentage);
                    childObject.color = childColor;
                }
            }
            else
            {
                foreach (Graphic childObject in button.GetComponentsInChildren<Graphic>())
                {
                    Color childColor = childObject.color;
                    childColor.a = 1;
                    childObject.color = childColor;
                }
            }
        }
    }

}
