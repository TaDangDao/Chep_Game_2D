using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController :Enemy
{
    public string bossName = "Dragon Lord";
    [SerializeField] private BossHealthBar bossHealthBar;// Tham chiếu thanh máu boss
    [SerializeField] private ParticleSystem ps;// Thâm chiếu particle system
    private bool isBattleActive = false;// Trạng thái có thể tấn công
    private int currentPhase = 1;// Phase hiện tại của boss
    private Player player;
    private Vector3 movement;
    private float attack_Time;// Thời gian tấn công
    private float attack_Timer;// Bộ đếm thời gian
    public void Start()
    {
        OnInit();
    }
    // Khởi tao thông số cơ bản cho bos
    public override void OnInit()
    {
        damage = 10f;
        maxHp = 500f;
        hp = maxHp;
        player = FindObjectOfType<Player>();
        attack_Time = 2f;
        attack_Timer = 2f;
        healthBar = bossHealthBar;
    }
    // Khởi động boss
    public void InitiateBattle()
    {
        isBattleActive = true;
        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.Init(maxHp);
    }
    // Check xem player có trong tầm
    public bool InRange()
    {
        return Vector2.Distance(transform.position, player.transform.position)<attackRange;
    }
    private void Update()
    {
        if (!IsDead)// Nếu chưa chết
        {
            movement = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * moveSpeed * 0.1f);// Di chuyển
            ChangeDirection(player.transform.position.x > transform.position.x);// Quay mặt về hướng di chuyển
            if (!InRange())// Nếu không trong tầm đánh thì di chuyển tới Player
            {
                ChangeAnimation("run");
                transform.position = new Vector2(movement.x, transform.position.y);
            }
            else
            {
                // Ngược lại thì check thời gian tấn công
                if (attack_Timer <= attack_Time)// Nếu chưa đạt thời gian thì cộng bộ đếm
                {
                    attack_Timer += Time.deltaTime;
                }
                else// Ngược lại thì tấn công
                {
                    Attack();
                    attack_Timer = 0f;
                    Invoke(nameof(ResetAttack), 1f);// Reset đòn đánh sau 1 khoảng thời gian
                }

            }
        }
    }
    // Tấn công
    public override void Attack()
    {
        base.Attack();
        Debug.Log("Attack");
    }
    // Reset đòn đánh
    public void ResetAttack()
    {
        ChangeAnimation("idle");
    }
    // Check xem có bị gây damage không
    public override void OnHit(float damage)
    {
        // Chỉ xử lý nếu nhân vật còn sống
        if (!IsDead)
        {
            hp -= damage; // Trừ máu
            bossHealthBar.SetHp(hp); // Cập nhật thanh máu
            ChangeAnimation("getHit" + Random.Range(1, 3));
            // Tạo text hiển thị sát thương tại vị trí nhân vật + Vector3.up
            Instantiate(base.combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);

            // Kiểm tra nếu máu <= 0 thì chết
            if (IsDead)
            {
                OnDeath();
            }
        }
        Debug.Log("Damage"+ damage);
        Debug.Log(hp);
        // Chuyển phase khi máu giảm
        if (hp < maxHp * 0.5f && currentPhase == 1)
        {
            EnterPhase2();
        }
    }

    // Chuyển phase 2
    private void EnterPhase2()
    {
        currentPhase = 2;// Chuyển phase hiện tại thành 2
        damage *= 1.5f;// Tăng damage;
        moveSpeed *= 1.2f;// Tăng tốc độ di chuyển
        // Kích hoạt kỹ năng đặc biệt
        StartCoroutine(Phase2SpecialAttack());
    }
    // Thực thi chiêu thức ở Phase 2
    private IEnumerator Phase2SpecialAttack()
    {
        while (isBattleActive)// Nếu đang trong trạng thái chiến đấu
        {
            yield return new WaitForSeconds(5f);
            if (!isBattleActive) yield break;
            CastAoeAttack();// Chiếu thưc ở phase 2
        }
    }
    // Chiêu thức phase 2
    public void CastAoeAttack()
    {
        ps.Play();// Cho chạy particle system để phóng ra các thanh kunai
    }
    // Hàm gọi khi chết
    protected override void OnDeath()
    {
        isBattleActive = false;// Chuyển trạng thái về false;
        base.OnDeath();
        UIManager.Instance.ShowEndGameCanvas(1);// Hiển thị UI EndGame;
        // Ẩn health bar boss
        bossHealthBar.gameObject.SetActive(false);
    }
}
