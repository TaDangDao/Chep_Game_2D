using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public List<UpgradeUI> upgradeUIList= new List<UpgradeUI>();
    public List<UpgradeOption> upgradeOptions = new List<UpgradeOption>();
    public Player player;
    private void Awake()
    {
        player = GetComponent<Player>();
        ApplyUpgradeOption();
    }
    public void ApplyUpgradeOption()
    {
        List<UpgradeOption> availableOptions = new List<UpgradeOption>(upgradeOptions);
        foreach(var upgrade in upgradeUIList)
        {
            UpgradeOption option= availableOptions[Random.Range(0, availableOptions.Count)];
            upgrade.upgradeOption = option;
            availableOptions.Remove(option);
            upgrade.txt.SetText("Gain " + upgrade.upgradeOption.info.statsUpgrade+ "% for " + upgrade.upgradeOption.info.name + "'s stats");
            upgrade.img.sprite = option.info.Icon;
            upgrade.img.SetNativeSize();
            upgrade.gold.SetText(option.info.goldConsume.ToString());
            upgrade.button.onClick.AddListener(() =>
            {
                Upgrade(upgrade.upgradeOption);
            });
        }

    }
    public void Upgrade(UpgradeOption upgradeOption)
    {
        player.Upgrade(upgradeOption);
        upgradeOption.info.statsUpgrade += 5;
        UIManager.Instance.CloseUpgradeUIPanel();
    }

}
