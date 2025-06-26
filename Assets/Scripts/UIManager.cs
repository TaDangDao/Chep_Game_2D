using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; }  private set { } }
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject UpgradeUIPanel;
    [SerializeField] private SkillTree skillTree;
    private void Awake()
    {
        instance = this;
    }
    public void SetCoin(int coin)
    {
        coinText.SetText(coin.ToString());

    }
    public void ShowUpgradeUIPanel()
    {
        UpgradeUIPanel.SetActive(true);
        skillTree.ApplyUpgradeOption();
    }
    public void CloseUpgradeUIPanel()
    {
        UpgradeUIPanel.SetActive(false);
    }
}
