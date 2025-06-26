using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombatText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    //Hàm khởi tạo CombatText
    public void OnInit(float damage)
    {
         text.text=damage.ToString();
        Invoke(nameof(Ondespawn), 1f);
    }
    // Hàm hủy CombatText
    public void Ondespawn()
    {
        Destroy(gameObject);
    }
}
