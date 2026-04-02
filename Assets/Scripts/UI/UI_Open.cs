using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Open : MonoBehaviour
{
    public Canvas uiCanva;
    public CanvasGroup uiCanvasGroup;
    private bool isOpened;
    public string buttonName;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(buttonName)&& isOpened)
        {
            uiCanva.sortingOrder = 0;
            uiCanvasGroup.alpha = 0;
            isOpened = false;
            uiCanvasGroup.blocksRaycasts = false;
            Time.timeScale = 1;
        }
        else if(Input.GetButtonDown(buttonName) && isOpened == false)
        {
            uiCanva.sortingOrder = 1;
            uiCanvasGroup.alpha = 1;
            isOpened = true;
            uiCanvasGroup.blocksRaycasts = true;
            Time.timeScale = 0;
        }
    }
}
