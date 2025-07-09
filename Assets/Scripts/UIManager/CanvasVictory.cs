using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasVictory : UICanvas
{
    [SerializeField] private TextMeshProUGUI bestScoreTMP;
    public override void SetUp()
    {
        base.SetUp();
        SetBestScore(100);
    }
    public void MainMenuButton()
    {
        UIManager_.Instance.CloseAll<UICanvas>();
        UIManager_.Instance.Open<CanvasMenu>();
    }
    public void SetBestScore(int bestScore)
    {
        bestScoreTMP.SetText(bestScore.ToString());
    }
}
