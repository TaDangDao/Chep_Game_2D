using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Reference đến Image component dùng để hiển thị thanh máu
    [SerializeField] private Image _image;

    // Offset để điều chỉnh vị trí thanh máu so với target
    [SerializeField] private Vector3 offset;

    // Transform của đối tượng mà thanh máu sẽ đi theo
    private Transform target;

    // Máu hiện tại và máu tối đa
    private float hp;
    private float maxHp;

    void Update()
    {
        // Cập nhật fillAmount một cách mượt mà sử dụng Lerp
        _image.fillAmount = Mathf.Lerp(_image.fillAmount, hp / maxHp, Time.deltaTime * 5f);

        // Cập nhật vị trí thanh máu theo vị trí target + offset
        transform.position = target.position + offset;
    }

    // Hàm khởi tạo thanh máu
    public void OnInit(float maxHp, Transform target)
    {
        this.target = target;           // Gán target để thanh máu đi theo
        this.maxHp = maxHp;             // Gán máu tối đa
        hp = maxHp;                     // Set máu hiện tại = maxHp
        _image.fillAmount = 1;          // Đặt thanh máu đầy (fillAmount = 1)
    }

    // Hàm cập nhật máu hiện tại
    public virtual void SetHp(float hp)
    {
        this.hp = hp;                   // Cập nhật giá trị máu hiện tại
        // _image.fillAmount = hp / maxHp; // Có thể dùng cách này để cập nhật ngay lập tức
    }

    // Hàm đặt chế độ filled cho Image
    public void SetFilled()
    {
        _image.type = Image.Type.Filled; // Chuyển sang chế độ filled (thường dùng cho thanh máu)
    }
}