using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI coin_TextMeshProUGUI;
    public override void SetUp()
    {
        base.SetUp();
        UpdateCoin(0);
    }
    public void UpdateCoin(int coin)
    {
        coin_TextMeshProUGUI.SetText(coin.ToString());
    }
    public void SettingButton()
    {
        UIManager_.Instance.Open<CanvasSetting>().SetState(this);
    }
}
