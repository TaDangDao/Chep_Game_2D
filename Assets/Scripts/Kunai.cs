using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;  // Thành phần vật lý
    [SerializeField] private GameObject hitVFX;  // Hiệu ứng khi trúng đích
    private float damage;  // Lượng sát thương

    // Hàm khởi tạo kunai
    public void OnInit(int direction, float dmg)
    {
        this.damage = dmg;  // Gán sát thương
        rb.velocity = transform.right * direction * 5f;  // Bay theo hướng chỉ định với tốc độ 5
        Invoke(nameof(OnDespawn), 4f);  // Tự hủy sau 4 giây
    }

    // Hàm hủy kunai
    public void OnDespawn()
    {
        Destroy(gameObject);  // Xóa game object
    }

    // Xử lý va chạm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")  // Nếu chạm enemy
        {
            // Gây sát thương cho enemy
            collision.GetComponent<Character>().OnHit(this.damage);

            // Tạo hiệu ứng trúng đích
            Instantiate(hitVFX, collision.transform.position, Quaternion.identity);

            // Hủy kunai sau khi trúng
            OnDespawn();
        }
    }
}