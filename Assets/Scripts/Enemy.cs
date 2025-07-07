using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    // Các thông số cấu hình enemy
    [SerializeField] protected float attackRange;  // Khoảng cách tấn công
    [SerializeField] protected float moveSpeed;    // Tốc độ di chuyển
    [SerializeField] private Rigidbody2D rb;    // Thành phần vật lý
    [SerializeField] private GameObject attackArea; // Vùng tấn công

    private IState currentState;  // State hiện tại (State Pattern)
    private bool isRight = true;  // Hướng quay mặt
    private Character target;     // Mục tiêu tấn công
    public Character Target => target; // Property truy cập target

    void Update()
    {
        // Xử lý state hiện tại nếu enemy còn sống
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }

    // Hàm khởi tạo enemy
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState()); // Bắt đầu bằng trạng thái idle
        DeActiveAttack(); // Tắt vùng tấn công
    }

    // Hàm hủy enemy
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject); // Hủy thanh máu
        Destroy(gameObject); // Hủy bản thân enemy
    }

    // Xử lý khi chết
    protected override void OnDeath()
    {
        ChangeState(null); // Dừng mọi state
        base.OnDeath();
    }

    // Thay đổi state (State Pattern)
    public void ChangeState(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this); // Thoát state cũ
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this); // Vào state mới
        }
    }

    // Di chuyển enemy
    public void Moving()
    {
        ChangeAnimation("run"); // Chuyển animation chạy
        rb.velocity = transform.right * moveSpeed; // Di chuyển theo hướng
    }

    // Dừng di chuyển
    public void StopMoving()
    {
        if(IsDead) return;
        ChangeAnimation("idle"); // Chuyển animation đứng yên
        rb.velocity = Vector2.zero; // Vận tốc = 0
    }

    // Thực hiện tấn công
    public virtual void Attack()
    {
        ChangeAnimation("attack"); // Animation tấn công
        ActiveAttack(); // Kích hoạt vùng tấn công
        Invoke(nameof(DeActiveAttack), 0.5f); // Tự tắt sau 0.5s
    }

    // Kiểm tra mục tiêu trong tầm
    public bool isTargetInRange()
    {
        return target != null &&
               Vector2.Distance(target.transform.position, transform.position) < attackRange;
    }

    // Xử lý va chạm với tường
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight); // Đổi hướng khi chạm tường
        }
    }

    // Thay đổi hướng mặt
    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        // Quay mặt sang trái/phải bằng cách xoay transform
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    // Bật/tắt vùng tấn công
    private void ActiveAttack() => attackArea.SetActive(true);
    private void DeActiveAttack() => attackArea.SetActive(false);

    // Thiết lập mục tiêu
    public void SetTarget(Character character)
    {
        this.target = character;

        // Tự động chuyển state dựa trên điều kiện
        if (isTargetInRange())
        {
            ChangeState(new AttackState()); // Tấn công nếu đủ gần
        }
        else if (Target != null)
        {
            ChangeState(new PatrolState()); // Đuổi theo nếu có mục tiêu
        }
        else
        {
            ChangeState(new IdleState()); // Idle nếu không có mục tiêu
        }
    }
}
