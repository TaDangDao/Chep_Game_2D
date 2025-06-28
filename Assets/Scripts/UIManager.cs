using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    //Singleton
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; }  private set { } }
    [SerializeField] private TextMeshProUGUI coinText;// hiển thị số coin
    [SerializeField] private GameObject UpgradeUIPanel;// hiển thi nâng cấp
    [SerializeField] private SkillTree skillTree;// tham chiếu đến lớp SkillTree
    [SerializeField] private GameObject NoMoneyLeftUI;
    [SerializeField] private GameObject EndGameUI;
    [SerializeField] private TextMeshProUGUI endGameText;
    private void Awake()
    {
        instance = this;// khởi tạo instance;
    }
    // Thay đổi sự hiển thị số lượng coin
    public void SetCoin(int coin)
    {
        coinText.SetText(coin.ToString());

    }
    // Hiển thị thông báo không đủ tiền mua nâng cấp
    public void ShowNoMoneyLeftUI()
    {
        NoMoneyLeftUI.SetActive(true);
        Invoke(nameof(CloseNoNoMoneyLeftUI), 1f);
    }
    // Đóng thông báo
    public void CloseNoNoMoneyLeftUI()
    {
        NoMoneyLeftUI.SetActive(false);
    }
    // Hiển thị UI nâng cấp 
    public void ShowUpgradeUIPanel()
    {
        UpgradeUIPanel.SetActive(true);
        Time.timeScale = 0f;
        skillTree.ApplyUpgradeOption();
    }
    // Đóng UI nâng cấp
    public void CloseUpgradeUIPanel()
    {
        UpgradeUIPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    // Hiển thị UI End Game
    public void ShowEndGameCanvas(int x)
    {
        if (x == 1)// Nếu bằng 1 là thắng
        {
            endGameText.SetText("You Win");
        }
        else// Ngược lại thì thua
        {
            endGameText.SetText("You Lose");
        }
            EndGameUI.SetActive(true);
        Invoke(nameof(ReloadCurrentScene), 1f);
    }
  
        public void ReloadCurrentScene()
    {
        // Lấy tên scene hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Tải lại scene
        SceneManager.LoadScene(currentSceneName);

      
    }
}
