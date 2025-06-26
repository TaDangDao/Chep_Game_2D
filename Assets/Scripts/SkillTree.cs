using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    // Danh sách các UI hiển thị nâng cấp
    public List<UpgradeUI> upgradeUIList = new List<UpgradeUI>();

    // Danh sách các tùy chọn nâng cấp có thể
    public List<UpgradeOption> upgradeOptions = new List<UpgradeOption>();

    // Tham chiếu đến player
    public Player player;

    private void Awake()
    {
        // Lấy component Player
        player = GetComponent<Player>();
        // Áp dụng các tùy chọn nâng cấp khi khởi tạo
        ApplyUpgradeOption();
    }

    // Phương thức áp dụng các tùy chọn nâng cấp lên UI
    public void ApplyUpgradeOption()
    {
        // Tạo danh sách các tùy chọn có sẵn (copy từ upgradeOptions)
        List<UpgradeOption> availableOptions = new List<UpgradeOption>(upgradeOptions);

        // Duyệt qua từng UI nâng cấp
        foreach (var upgrade in upgradeUIList)
        {
            // Chọn ngẫu nhiên 1 tùy chọn từ danh sách có sẵn
            UpgradeOption option = availableOptions[Random.Range(0, availableOptions.Count)];

            // Gán tùy chọn cho UI
            upgrade.upgradeOption = option;

            // Loại bỏ tùy chọn đã chọn để không bị trùng
            availableOptions.Remove(option);

            // Cập nhật thông tin hiển thị trên UI
            upgrade.txt.SetText("Gain " + upgrade.upgradeOption.info.statsUpgrade + "% for " +
                             upgrade.upgradeOption.info.name + "'s stats");
            upgrade.img.sprite = option.info.Icon;
            upgrade.img.SetNativeSize();
            upgrade.gold.SetText(option.info.goldConsume.ToString());

            // Xóa các listener cũ và thêm listener mới cho button
            upgrade.button.onClick.RemoveAllListeners();
            upgrade.button.onClick.AddListener(() =>
            {
                Upgrade(option);
                Debug.Log(upgradeUIList.IndexOf(upgrade));
            });
        }
    }

    // Phương thức nâng cấp
    public void Upgrade(UpgradeOption upgradeOption)
    {
        // Gọi phương thức nâng cấp của player
        player.Upgrade(upgradeOption);

        // Tăng giá trị nâng cấp thêm 5% cho lần sau
        upgradeOption.info.statsUpgrade += 5;

        // Đóng panel nâng cấp
        UIManager.Instance.CloseUpgradeUIPanel();
    }
}
