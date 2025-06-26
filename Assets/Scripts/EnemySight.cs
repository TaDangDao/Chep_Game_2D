using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    // Khai báo tham chiếu đến enemy sẽ được điều khiển
    [SerializeField] Enemy enemy;

    // Hàm kích hoạt khi có vật thể khác đi vào vùng trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu vật thể va chạm có tag "Player"
        if (collision.tag == "Player")
        {
            // Gán player làm mục tiêu cho enemy bằng cách lấy component Character của player
            enemy.SetTarget(collision.GetComponent<Character>());
        }
    }

    // Hàm kích hoạt khi vật thể rời khỏi vùng trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Kiểm tra nếu vật thể rời đi có tag "Player"
        if (collision.tag == "Player")
        {
            // Xóa mục tiêu hiện tại của enemy (set null)
            enemy.SetTarget(null);
        }
    }
}
