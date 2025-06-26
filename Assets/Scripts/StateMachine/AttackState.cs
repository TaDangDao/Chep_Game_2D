using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{

    float randomTime;
    float timer;

    // Hàm được gọi khi vào trạng thái này
    void IState.OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            // Đổi hướng của enemy đến vị trí của player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();// Dừng lại
            Debug.Log("Attack");
            enemy.Attack();//Tấn công
        }
        timer = 0;
    }
    // Hàm xử lý logic chính mỗi frame
    void IState.OnExecute(Enemy enemy)
    {
        timer+= Time.deltaTime;// Cập nhật bộ đếm thời gian
        if (timer > 1.5f)//Nếu hết thời gian 
        {
            enemy.ChangeState(new PatrolState());// Di chuyển
        }
    }

    void IState.OnExit(Enemy enemy)
    {
    }
}
