using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] private CombatText combatTextPrefab;
    private string currentAnim="idle";
    private float hp;
    public bool IsDead => hp <= 0;

    private void Start()
    {
        OnInit();
        currentAnim = "idle";
    }
    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100,transform);
    }
    public virtual void OnDespawn()
    {

    }
    protected void ChangeAnimation(string animationName)
    {
        if (currentAnim != animationName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animationName;
            animator.SetTrigger(currentAnim);
        }
    }
    public void OnHit(float damage)
    {
        Debug.Log(this + "dmg");
        if (!IsDead)
        {
            hp -= damage;
            healthBar.SetHp(hp);
            Instantiate(combatTextPrefab, transform.position+Vector3.up, Quaternion.identity).OnInit(damage);
            if(IsDead)
            {
                OnDeath();
            }
        }
        
    }
    protected virtual void OnDeath()
    {
        ChangeAnimation("die");
        Debug.Log(this + "dead");
        Invoke(nameof(OnDespawn), 2f);
    }
}
