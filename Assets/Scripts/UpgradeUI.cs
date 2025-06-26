using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class UpgradeUI 
{
    public Button button;
    public Image img;
    public TextMeshProUGUI txt;
    public UpgradeOption upgradeOption;
    public TextMeshProUGUI gold;
}
[System.Serializable]
public class UpgradeOption
{
   public  UpgradeOptionScriptableObject info;
}
