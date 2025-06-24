using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; }  private set { } }
    [SerializeField] private TextMeshProUGUI coinText;
    private void Awake()
    {
        instance = this;
    }
    public void SetCoin(int coin)
    {
        coinText.SetText(coin.ToString());

    }
}
