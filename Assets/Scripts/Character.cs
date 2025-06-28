using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Khai báo thành phần Animator để điều khiển animation
    [SerializeField] private Animator animator;

    // Khai báo thanh máu (protected để lớp con có thể truy cập)
    [SerializeField] protected HealthBar healthBar;

    // Prefab hiển thị sát thương khi bị đánh trúng
    [SerializeField] protected CombatText combatTextPrefab;

    // Tên animation hiện tại, mặc định là "idle" (đứng yên)
    private string currentAnim = "idle";

    // Máu hiện tại và máu tối đa (protected để lớp con truy cập)
    protected float hp;
    protected float maxHp;

    // Sát thương của nhân vật
    protected float damage;

    // Property chỉ đọc để lấy giá trị sát thương
    public float Damage => damage;

    // Property kiểm tra nhân vật đã chết chưa (máu <= 0)
    public bool IsDead => hp <= 0;

    // Hàm khởi tạo khi đối tượng được tạo
    private void Start()
    {
        OnInit(); // Gọi hàm khởi tạo
        currentAnim = "idle"; // Đặt animation mặc định
    }

    // Hàm khởi tạo nhân vật (virtual để lớp con có thể override)
    public virtual void OnInit()
    {
        hp = 100; // Máu mặc định 100
        damage = 10; // Sát thương mặc định 10
        healthBar.OnInit(hp, transform); // Khởi tạo thanh máu
    }

    // Hàm hủy đối tượng (virtual để lớp con có thể override)
    public virtual void OnDespawn()
    {
        // Có thể thêm logic hủy đối tượng ở lớp con
    }

    // Hàm thay đổi animation
    protected void ChangeAnimation(string animationName)
    {
        // Chỉ đổi animation nếu khác animation hiện tại
        if (currentAnim != animationName)
        {
            animator.ResetTrigger(currentAnim); // Tắt animation cũ
            currentAnim = animationName; // Cập nhật animation hiện tại
            animator.SetTrigger(currentAnim); // Kích hoạt animation mới
        }
    }

    // Hàm xử lý khi nhận sát thương
    public virtual void OnHit(float damage)
    {
        // Chỉ xử lý nếu nhân vật còn sống
        if (!IsDead)
        {
            hp -= damage; // Trừ máu
            healthBar.SetHp(hp); // Cập nhật thanh máu

            // Tạo text hiển thị sát thương tại vị trí nhân vật + Vector3.up
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);

            // Kiểm tra nếu máu <= 0 thì chết
            if (IsDead)
            {
                OnDeath();
            }
        }
    }

    // Hàm xử lý khi chết (virtual để lớp con có thể override)
    protected virtual void OnDeath()
    {
        ChangeAnimation("die"); // Chuyển sang animation chết
        Debug.Log(this + "dead"); // Log thông báo (debug)
        Invoke(nameof(OnDespawn), 2f); // Gọi hủy đối tượng sau 2 giây
    }
}
