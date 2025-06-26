using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // Mục tiêu camera theo dõi
    public Vector3 offset;                     // Khoảng cách giữa camera và player
    [SerializeField] private float speed;      // Tốc độ di chuyển camera

    void Start()
    {
        // Tự động tìm và gán player làm mục tiêu khi bắt đầu
        target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        // Di chuyển camera mượt mà đến vị trí mục tiêu
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
}
