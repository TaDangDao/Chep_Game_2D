using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // Mục tiêu camera theo dõi
    public Vector3 offset;                     // Khoảng cách giữa camera và player
    [SerializeField] private float speed;      // Tốc độ di chuyển camera
    [SerializeField] private Transform minBound;
    [SerializeField] private Transform maxBound;
    private Camera cam;// Lấy tham chiếu đến camera
    // kích thước vùng camera
    float camHeight ;
    float camWidth;
    void Start()
    {
        // Tự động tìm và gán player làm mục tiêu khi bắt đầu
        target = FindObjectOfType<Player>().transform;
        cam = Camera.main;// Gán tham chiếu với main camera
        // Tính kích thước vùng
        camHeight = cam.orthographicSize;
        camWidth = cam.aspect * camHeight;
    }
    public void SetMaxBound(Transform maxBound)
    {
        this.maxBound = maxBound;

    }

    void Update()
    {
      // Giới hạn di chuyển của camera sao cho không vượt quá maxbound và minbound -> không lộ ngoài map
        Vector3 pos = new Vector3(Mathf.Clamp(target.position.x + offset.x, minBound.position.x+camWidth, maxBound.position.x-camWidth), 
            Mathf.Clamp(target.position.y + offset.y, minBound.position.y+camHeight, maxBound.position.y-camHeight),
            transform.position.z);
        // Di chuyển camera mượt mà đến vị trí mục tiêu
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
    }
}
