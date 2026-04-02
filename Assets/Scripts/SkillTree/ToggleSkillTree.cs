using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{
    public Canvas uiCanva;
    public CanvasGroup uiCanvasGroup;
    private bool skillTreeOpen = false;


    void Update()
    {
        if(Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                uiCanvasGroup.alpha = 0;
                uiCanvasGroup.blocksRaycasts = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                uiCanvasGroup.alpha = 1;
                uiCanvasGroup.blocksRaycasts = true;
                skillTreeOpen = true;
            }

        }
        
    }
}
