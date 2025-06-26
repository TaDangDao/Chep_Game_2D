using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Khai báo hai điểm di chuyển (A và B) với SerializeField để có thể chỉnh sửa trong Inspector
    [SerializeField] private Transform aPoint;
    [SerializeField] private Transform bPoint;

    // Biến lưu vị trí mục tiêu hiện tại
    Vector3 target;

    // Tốc độ di chuyển của platform
    [SerializeField] float speed;

    void Start()
    {
        // Khởi tạo vị trí ban đầu tại điểm A
        transform.position = aPoint.position;

        // Đặt mục tiêu ban đầu là điểm B
        target = bPoint.position;
    }

    void Update()
    {
        // Di chuyển platform từ vị trí hiện tại đến vị trí mục tiêu với tốc độ đã định
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Kiểm tra nếu platform gần đến điểm A (khoảng cách < 0.1f)
        if (Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            // Đổi mục tiêu sang điểm B
            target = bPoint.position;
        }
        // Ngược lại, nếu gần đến điểm B
        else if (Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            // Đổi mục tiêu sang điểm A
            target = aPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi có va chạm, kiểm tra nếu đó là Player
        if (collision.gameObject.tag == "Player")
        {
            // Gán Player làm con của platform để Player di chuyển cùng platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Khi Player rời khỏi platform
        if (collision.gameObject.tag == "Player")
        {
            // Bỏ parent của Player để nó không di chuyển cùng platform nữa
            collision.transform.SetParent(null);
        }
    }
}
