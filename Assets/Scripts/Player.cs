using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    private bool isGrounded=true;
    private bool isJumping=false;
    private bool isAttack;
    [SerializeField] private LayerMask ground;
    private float movement;
    [SerializeField]private float speed=500f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;
    private string currentAnim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded= CheckGrounded();
        movement = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isJumping = true;
                ChangeAnimation("jump");
                //isGrounded = false;
                rb.AddForce(jumpForce * Vector2.up);
            }
        
            if (Mathf.Abs(movement) > 0.1f)
            {
                ChangeAnimation("run");
            }
        }
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnimation("fall");
            isJumping = false;
        }
        if (Mathf.Abs(movement)>0.1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, movement > 0?0: 180, 0));
            rb.velocity = new Vector2(movement*Time.deltaTime*speed, rb.velocity.y);
        }
        else if(isGrounded)
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
    private void Attack()
    {

    }
    private void Throw()
    {

    }
    private void Jump()
    {
        ChangeAnimation("jump");
        rb.AddForce(jumpForce * Vector2.up);

    }
    private void Dead()
    {

    }
    private void ChangeAnimation(string animationName)
    {
            Debug.Log(animationName); 
        if (currentAnim != animationName)
        {
            animator.ResetTrigger(animationName);
            currentAnim = animationName;
            animator.SetTrigger(currentAnim);
        }
    }
}
