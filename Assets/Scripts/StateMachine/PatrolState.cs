using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    // Biến thời gian ngẫu nhiên và bộ đếm thời gian
    float randomTime;
    float timer;

    // Hàm được gọi khi vào trạng thái này
    void IState.OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(3f, 6f); // Chọn thời gian tuần tra ngẫu nhiên từ 3-6 giây
    }

    // Hàm xử lý logic chính mỗi frame
    void IState.OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime; // Cập nhật bộ đếm thời gian

        if (enemy.Target != null) // Nếu có mục tiêu (player)
        {
            // Đổi hướng enemy dựa trên vị trí player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            if (enemy.isTargetInRange()) // Nếu player trong tầm tấn công
            {
                enemy.ChangeState(new AttackState()); // Chuyển sang trạng thái tấn công
            }
            else // Nếu player ngoài tầm tấn công
            {
                enemy.Moving(); // Tiếp tục di chuyển về phía player
            }
        }
        else // Nếu không có mục tiêu
        {
            if (timer < randomTime) // Nếu chưa hết thời gian tuần tra
            {
                enemy.Moving(); // Tiếp tục di chuyển
            }
            else // Nếu đã hết thời gian tuần tra
            {
                enemy.ChangeState(new IdleState()); // Chuyển sang trạng thái đứng yên
            }
        }
    }

    // Hàm được gọi khi thoát khỏi trạng thái này
    void IState.OnExit(Enemy enemy)
    {
        // Hiện không có xử lý gì khi thoát trạng thái
    }
}
