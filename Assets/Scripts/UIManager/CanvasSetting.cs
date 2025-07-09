using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    [SerializeField] private GameObject[] buttons;
    public void SetState(UICanvas canvas)
    {
        for(int i=0; i<buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        switch(canvas)
        {
            case CanvasMenu:
                buttons[2].gameObject.SetActive(true);
                break;
            case CanvasGamePlay:
                buttons[0].gameObject.SetActive(true); 
                buttons[1].gameObject.SetActive(true);
                break;
             default:
                break;

        }
    }
    public void MainMenuButton()
    {
        UIManager_.Instance.CloseAll<UICanvas>();
        UIManager_.Instance.Open<CanvasMenu>();
    }
}
