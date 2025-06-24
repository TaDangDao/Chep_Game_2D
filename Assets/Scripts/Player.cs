using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    private bool isGrounded=true;
    private bool isJumping=false;
    private bool isIdle = true;
    private bool isAttack=false;
    private bool isDead=false;
    [SerializeField] private LayerMask ground;
    private float movement;
    [SerializeField]private float speed=500f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    public int coinAmount=0;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Vector3 savePoint;
    private void Awake()
    {
        ChangeAnimation("idle");
        SavePoint();
    }
    public override void OnInit()
    {
        ChangeAnimation("idle");
        base.OnInit();
        coinAmount = PlayerPrefs.GetInt("Coin",0);
        UIManager.Instance.SetCoin(coinAmount);
        isGrounded = true;
        isJumping = false;
        isIdle = true;
        isAttack = false;
        isDead = false;
        transform.position = savePoint;
        DeActiveAttack();
        Debug.Log("respawn");
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        isDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded= CheckGrounded();
       // movement = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        if (isDead)
        {
            ChangeAnimation("die");
            return;
        }
        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
            else if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }
            else if (Mathf.Abs(movement) > 0.1f)
            {
                ChangeAnimation("run");
            }
        }
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnimation("fall");
            isJumping = false;
            isIdle = true;
        }
        if (Mathf.Abs(movement)>0.1f&&!isAttack)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, movement > 0?0: 180, 0));
            rb.velocity = new Vector2(movement*speed, rb.velocity.y);
        }
        else if(isGrounded&&isIdle)
        {
            ChangeAnimation("idle");
            rb.velocity = Vector2.zero;
        }
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down*1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, ground);
        return hit.collider!= null;
    }
    public void ResetAttack()
    {
        isAttack = false;
        isIdle = true;
        DeActiveAttack();
        ChangeAnimation("idle");
    }
    public void Attack()
    {
        isIdle = false;
        isAttack = true;
        ChangeAnimation("attack");
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
    }
    public void Throw()
    {
        isIdle = false;
        isAttack = true;
        ChangeAnimation("throw");
        Invoke(nameof(ResetAttack), 0.5f);
        Instantiate(kunaiPrefab, 
            throwPoint.transform.position, 
            transform.rotation.y == 0?Quaternion.identity: Quaternion.Euler(new Vector3(0, -180, 0)))
            .GetComponent<Kunai>().OnInit(1);
    }
    private void Jump()
    {
        isJumping = true;
        isIdle = false;
        ChangeAnimation("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    private void Dead()
    {

    }
    public void SavePoint()
    {
        savePoint=transform.position;
    }
    private void ActiveAttack()
    {
        attackArea.gameObject.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.gameObject.SetActive(false);
    }
    public void SetMove(float horizontal)
    {
        this.movement=horizontal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Coin")
        {
            coinAmount++;
            PlayerPrefs.SetInt("Coin", coinAmount);
            UIManager.Instance.SetCoin(coinAmount);
            Destroy(collision.gameObject);
        }
        if(collision.tag=="DeathZone")
        {
            isDead = true;
            ChangeAnimation("die");
            Invoke(nameof(OnInit), 1f);
        }
    }
}
