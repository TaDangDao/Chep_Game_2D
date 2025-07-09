using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager_.Instance.Open<CanvasMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && UIManager_.Instance.IsOpened<CanvasGamePlay>())
        {
            UIManager_.Instance.GetUI<CanvasGamePlay>().UpdateCoin(100);
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            UIManager_.Instance.CloseAll<UICanvas>();
            UIManager_.Instance.Open<CanvasVictory>().SetBestScore(100);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager_.Instance.CloseAll<UICanvas>();
            UIManager_.Instance.Open<Canvas_Fail>().SetBestScore(100);
        }
    }
}
