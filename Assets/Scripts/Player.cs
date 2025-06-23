using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    [SerializeField] private Animator animator;
    private string currentAnim;
    public int coinAmount=0;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 savePoint;
    private void Awake()
    {
        ChangeAnimation("idle");
    }
    void Start()
    {
        SavePoint();
        OnInit();
    }
    public void OnInit()
    {
        isGrounded = true;
        isJumping = false;
        isIdle = true;
        isAttack = false;
        isDead = false;
        transform.position = savePoint;
        ChangeAnimation("idle");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded= CheckGrounded();
        movement = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        if (isDead)
        {
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
            rb.velocity = new Vector2(movement*Time.deltaTime*speed, rb.velocity.y);
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
        ChangeAnimation("idle");
    }
    private void Attack()
    {
        isIdle = false;
        isAttack = true;
        ChangeAnimation("attack");
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void Throw()
    {
        isIdle = false;
        isAttack = true;
        ChangeAnimation("throw");
        Invoke(nameof(ResetAttack), 0.5f);
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
    private void ChangeAnimation(string animationName)
    {
        if (currentAnim != animationName)
        {
            animator.ResetTrigger(animationName);
            currentAnim = animationName;
            animator.SetTrigger(currentAnim);
        }
    }
    public void SavePoint()
    {
        savePoint=transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Coin")
        {
            coinAmount++;
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
