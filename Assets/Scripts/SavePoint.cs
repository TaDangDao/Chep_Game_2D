using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    // goi khi collider trigger va cham vao 1 collider khac
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Khi có va chạm, kiểm tra nếu đó là Player, thì lưu vị trí savePoint
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().SavePoint();
        }
    }
}
