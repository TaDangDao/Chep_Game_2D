using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : HealthBar
{
    [SerializeField] private Image healthFill;// Tham chiếu tới hình ảnh thanh máu
    [SerializeField] private TextMeshProUGUI bossNameText;// Thanm chiếu tới tên

    private float maxHp;

    // Khởi tạo thanh máu
    public void Init(float maxHealth)
    {
        Debug.Log("Init");
        maxHp = maxHealth;
        healthFill.fillAmount = 1f;
    }
    // Set giá trị thanh máu
    public override void SetHp(float currentHp)
    {
        healthFill.fillAmount = currentHp / maxHp;
    }
    private void Update()
    {
        
    }

}
