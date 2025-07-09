using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        UIManager_.Instance.Open<CanvasGamePlay>();
    }
    public void SettingButton()
    {
        UIManager_.Instance.Open<CanvasSetting>().SetState(this);
    }
}
