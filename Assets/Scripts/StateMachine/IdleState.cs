using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    // Biến thời gian ngẫu nhiên và bộ đếm thời gian
    float randomTime;
    float timer;
    // Hàm được gọi khi vào trạng thái này
    void IState.OnEnter(Enemy enemy)
    {
        enemy.StopMoving();// Dừng di chuyển
        timer = 0;// Đặt lại bộ đếm thời gian
        randomTime = Random.Range(2.5f, 4f);// Thiết lập lại biến thời gian ngẫu nhiên
    }

    // Hàm xử lý logic chính mỗi frame
    void IState.OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;// Cập nhật bộ đếm thời gian
        if (timer > randomTime)// Nếu hết thời gian đừng im
        {
            enemy.ChangeState(new PatrolState());// Chuyển trạng thái di chuyển
        }
    }

    void IState.OnExit(Enemy enemy)
    {
        
    }
}
