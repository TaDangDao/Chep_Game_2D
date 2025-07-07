using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb; // Thành phần vật lý 2D
    private bool isGrounded = true; // Kiểm tra có đang chạm đất không
    private bool isJumping = false; // Trạng thái nhảy
    private bool isIdle = true; // Trạng thái đứng yên
    private bool isAttack = false; // Trạng thái tấn công
    private bool isDead = false; // Trạng thái chết
    [SerializeField] private LayerMask ground; // Layer mặt đất
    private float movement; // Giá trị di chuyển (-1 đến 1)
    [SerializeField] private float speed = 500f; // Tốc độ di chuyển
    [SerializeField] private float jumpForce; // Lực nhảy
    [SerializeField] private Kunai kunaiPrefab; // Prefab kunai để ném
    [SerializeField] private float kunaiDamage; // Sát thương kunai
    [SerializeField] private Transform throwPoint; // Vị trí ném kunai
    [SerializeField] private GameObject attackArea; // Vùng tấn công cận chiến
    [SerializeField] private float mana; // Mana tối đa
    [SerializeField] private float currentMana; // Mana hiện tại
    [SerializeField] private HealthBar manaBar; // Thanh hiển thị mana
    public int coinAmount = 0; // Số lượng coin
    [SerializeField] private SpriteRenderer spriteRenderer; // Renderer hiển thị hình ảnh
    public Vector3 savePoint; // Vị trí hồi sinh
    private float manaRecoveryTime ; // Thời gian hồi phục mana
    private float manaTimer; // Đếm thời gian hồi phục mana
    private bool canControl = true;
    private FlameThrow flameSkill;
    private void Awake()
    {
        ChangeAnimation("idle");//Bắt đầu với animation đứng yên
        manaRecoveryTime = 1f;
        manaTimer = 0f;
        flameSkill=GetComponentInChildren<FlameThrow>();
        SavePoint();// Lưu vị trí ban đầu
    }
    public override void OnInit()
    {
        ChangeAnimation("idle");

        // Lấy giá trị từ PlayerPrefs hoặc dùng giá trị mặc định
        maxHp = PlayerPrefs.GetFloat("PlayerHealth",100);
        hp = maxHp;
        damage = PlayerPrefs.GetFloat("PlayerDamage", 5);
        mana = PlayerPrefs.GetFloat("PlayerMana",100);
        kunaiDamage = PlayerPrefs.GetFloat("Kunai", 10);
        coinAmount = PlayerPrefs.GetInt("Coin", 0);
        currentMana = mana;

        // Khởi tạo thanh máu và mana và hiển thị coin
        healthBar.OnInit(maxHp, transform);
        manaBar.OnInit(mana, transform);
        UIManager.Instance.SetCoin(coinAmount);

        // Reset các trạng thái
        isGrounded = true;
        isJumping = false;
        isIdle = true;
        isAttack = false;
        isDead = false;

        // Đặt lại vị trí
        transform.position = savePoint;
        DeActiveAttack();// Tắt tấn công
    }

    // Hàm hủy đối tượng
    public override void OnDespawn()
    {
        base.OnDespawn();
       // OnInit();// Khởi tạo lại khi hủy
    }
    protected override void OnDeath()
    {
        base.OnDeath();// Gọi hàm OnDeath của cha;
        UIManager.Instance.ShowEndGameCanvas(0);
        isDead = true;// Đặt trạng thái chết
    }
    public void SetControl(bool enable)
    {
        canControl = enable;
        rb.velocity = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded= CheckGrounded();// Kiểm tra chạm đất
        movement = Input.GetAxisRaw("Horizontal"); // Lấy input di chuyển theo chiều ngang( các nút A, D ,->,<-)
        //vertical = Input.GetAxisRaw("Vertical");

        //Tính thời gian để hồi lại mana
        if (manaTimer < manaRecoveryTime)
        {
            manaTimer += Time.deltaTime;
        }
        else
        {
            if (currentMana < mana)
            {
                currentMana += 1;
                manaBar.SetHp(currentMana);
            }
            manaTimer = 0f;
        }

        // Kiểm tra trạng thái chết
        if (isDead)
        {
            ChangeAnimation("die");// chuyển animation chết
            return;
        }
        // Kiểm tra có đang tấn công hay không, nếu có thì dừng di chuyển
        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        // Kiểm tra điều kiện chạm đất
        if (isGrounded)
        {
            if (isJumping)// Nếu đang nhảy thì return
            {
                return;
            }
            // Nếu nhấn nút space và đang ở dưới đất thì gọi hàm nhảy
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
            else if (Mathf.Abs(movement) > 0.1f)// Nếu ngược lại, đang di chuyển thì chuyển sang animation chạy
            {
                ChangeAnimation("run");
            }
        }
        if (Input.GetKeyDown(KeyCode.V) && !isAttack)// Kiểm tra nếu nhấn nút V và đang không trong trạng thái tấn công
        {
            Throw();// Gọi hàm throw để ném kunai
        }
        else if (Input.GetKeyDown(KeyCode.C) && !isAttack)// Kiểm tra nếu nhấn nút C và đang không tấn công 
        {
            Attack();// Gọi hàm tấn công
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            Dash();
        }else if (Input.GetKeyDown(KeyCode.G) && isGrounded)
        {
            flameSkill.Attack(transform.rotation.y==0 ? 1 : -1);
        }
        if (!isGrounded && rb.velocity.y < 0)// Nếu không ở trên mặt đất và đang rơi 
        {
            ChangeAnimation("fall");//Chuyển sang animation rơi xuống
            isJumping = false;
            isIdle = true;
        }
        if (Mathf.Abs(movement)>0.1f&&!isAttack)// Nếu input nhân vào khác 0 và đang không trong trạng thái tấn công
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, movement > 0?0: 180, 0));// Xoay nhân vật theo nút bấm
            rb.velocity = new Vector2(movement*speed, rb.velocity.y);// Di chuyển 
        }
        else if(isGrounded&&isIdle)// Nếu đang ở trên mặt đất và trạng thái là đứng yên
        {
            ChangeAnimation("idle");// Chuyển animation sang idle
            rb.velocity = Vector2.zero;// Đặt velocity bằng 0
        }

    }

    // Hàm để thực thi các Upgrade mà người chơi mua bằng vàng
    public void Upgrade(UpgradeOption upgradeOption)
    {
        if (coinAmount >= upgradeOption.info.goldConsume)// Nếu đủ tiền mua thì mới có thể có nâng cấp
        {
            switch (upgradeOption.info.name)
            {
                case "Health":// Nâng cấp máu của player
                    maxHp += maxHp * upgradeOption.info.statsUpgrade / 100f;
                    hp = maxHp;
                    SetHealth(maxHp);
                    break;
                case "Damage":// Nâng cấp damage của player
                    damage += damage * upgradeOption.info.statsUpgrade / 100f;
                    SetDamage(damage);
                    break;
                case "Mana":// Nâng cấp mana của player
                    mana += mana * upgradeOption.info.statsUpgrade / 100f;
                    currentMana = mana;
                    SetMana(mana);
                    break;
                case "Kunai":// // Nâng cấp damage của kunai
                    kunaiDamage += kunaiDamage * upgradeOption.info.statsUpgrade / 100f;
                    SetKunaiDamage(kunaiDamage);
                    break;
                default:
                    break;
            }
            coinAmount -= upgradeOption.info.goldConsume;// Trừ coin
            PlayerPrefs.SetInt("Coin", coinAmount);// Lưu lại số coin
            UIManager.Instance.SetCoin(coinAmount);// Cập nhật lại số coin trên UI
        }
        else// Nếu không đủ tiền thì hiển thị thông báo
        {
            UIManager.Instance.ShowNoMoneyLeftUI();
        }
    }
    // Thiết lập máu
    public void SetHealth(float hp)
    {
        PlayerPrefs.SetFloat("PlayerHealth", hp);
        healthBar.OnInit(hp, transform);
    }
    // Thiết lập damage
    public void SetDamage(float damage)
    {
        PlayerPrefs.SetFloat("PlayerDamage",damage);
    }
    // Thiết lập mana
    public void SetMana(float mana)
    {
        PlayerPrefs.SetFloat("PlayerMana", mana);
        manaBar.OnInit(mana, transform);
    }
    // Thiết lập damage của kunai
    public void SetKunaiDamage(float damage)
    {
        PlayerPrefs.SetFloat("Kunai", damage);
    }
    // Kiểm tra có chạm đất không
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down*1.1f, Color.red);// Hiển thị Ray trong scene để debug
        // Chiếu raycast từ vị trí của nhân vật xuống phía dưới 1.1 đơn vị, và chọn lọc chỉ phát các thành phần có layermask là "Ground"
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, ground);
        return hit.collider!= null;// Trả về kết quả
    }

    // Đưa nhân vật từ trạng thái tấn công về idle và tắt vùng tấn công 
    public void ResetAttack()
    {
        isAttack = false;
        isIdle = true;
        DeActiveAttack();
        ChangeAnimation("idle");
    }
    // Tấn công
    public void Attack()
    {
        isIdle = false;
        isAttack = true;
        string attackAnim = isGrounded ? "attack" : "jump_attack";// Nếu đang trên không thì sử dụng anim nhảy tấn công, còn không thì sử dụng anim tấn công bình thường
        ChangeAnimation(attackAnim);// Chuyển đổi anim
        Invoke(nameof(ResetAttack), 0.5f);// Gọi hàm RessetAttack sau 0.5 giây
        ActiveAttack();// Kích hoạt vùng tấn công để gây damage;
    }
    // Ném kunai
    public void Throw()
    {
        if (currentMana > 20)// Kiểm tra có đủ mana không
        {
            // Đặt các trạng thái
            isIdle = false;
            isAttack = true;
            // trừ mana và cập nhật thanh mana
            currentMana -= 20;
            manaBar.SetHp(currentMana);
            manaBar.SetFilled();
            //Quyết định anim nhảy ném hay ném bình thường
            string throwAnim = isGrounded ? "throw" : "jump_throw";
            ChangeAnimation(throwAnim);// Chuyển anim ném
            Invoke(nameof(ResetAttack), 0.5f);// // Gọi hàm RessetAttack sau 0.5 giây

            // Khởi tạo kunai
            var kunai = Instantiate(
                kunaiPrefab,
                throwPoint.position,
                transform.rotation.y == 0 ? Quaternion.identity : Quaternion.Euler(0, -180, 0)
            ).GetComponent<Kunai>();

            kunai.OnInit(1, kunaiDamage); // Thiết lập Kunai
        }
    }
    // Nhảy
    private void Jump()
    {
        isJumping = true;
        isIdle = false;
        ChangeAnimation("jump");
        rb.AddForce(jumpForce * Vector2.up);//Thêm lực đẩy lên
    }
    private void Dash()
    {
        rb.AddForce(transform.right * 10000f);
        Debug.Log("Dash");
    }
    // Hàm lưu vị trí quay trở về khi chết
    public void SavePoint()
    {
        savePoint=transform.position;
    }
    // Kích hoạt vùng tấn công
    private void ActiveAttack()
    {
        attackArea.gameObject.SetActive(true);
    }
    // Tắt vùng tấn công
    private void DeActiveAttack()
    {
        attackArea.gameObject.SetActive(false);
    }
    // Hàm thiết laoaj giá trí movement dùng khi sử dụng nút trên màn hình
    public void SetMove(float horizontal)
    {
        this.movement=horizontal;
    }
    // Kiếm tra va chạm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Coin")// Nếu là coin thì cập nhật coin, xoá đi vật thể coin vừa va chạm;
        {
            coinAmount++;
            PlayerPrefs.SetInt("Coin", coinAmount);
            UIManager.Instance.SetCoin(coinAmount);
            Destroy(collision.gameObject);
        }
        if(collision.tag=="DeathZone")// Nếu là deathzone thì chuyển anim sang chết và chuyển trạng thái isDead bằng true
        {
            isDead = true;
            ChangeAnimation("die");
            Invoke(nameof(OnInit), 1f);// Gọi hàm khởi tạo lại sau 1 giây
        }
    }
}
