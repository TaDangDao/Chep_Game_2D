using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombatText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public void OnInit(float damage)
    {
         text.text=damage.ToString();
        Invoke(nameof(Ondespawn), 1f);
    }
    public void Ondespawn()
    {
        Destroy(gameObject);
    }
}
