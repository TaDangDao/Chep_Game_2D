using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Character owner;// Lấy tham chiếu đến đối tượng chứa lớp này
    private void Awake()
    {
        owner = GetComponentInParent<Character>();
    }

    // Kiểm tra va chạm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")// Nếu là Player hoặc Enemy thì gây damage;
        {
            collision.GetComponent<Character>().OnHit(owner.Damage);// Gọi hàm gây damage của lớp cha Character
        }
    }
}
