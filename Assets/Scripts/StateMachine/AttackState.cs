using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{

    float randomTime;
    float timer;
    void IState.OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            // doi huong enemy toi huong cua player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();
            Debug.Log("Attack");
            enemy.Attack();
        }
        timer = 0;
    }

    void IState.OnExecute(Enemy enemy)
    {
        timer+= Time.deltaTime;
        if (timer > 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    void IState.OnExit(Enemy enemy)
    {
    }
}
