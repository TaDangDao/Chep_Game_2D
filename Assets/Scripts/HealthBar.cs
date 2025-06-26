using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Vector3 offset;
    private Transform target;
    private float hp;
    private float maxHp;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _image.fillAmount = Mathf.Lerp(_image.fillAmount, hp / maxHp, Time.deltaTime * 5f);
        transform.position = target.position + offset;
    }
    public void OnInit(float maxHp,Transform target)
    {
        this.target= target;
        this.maxHp = maxHp;
        hp = maxHp;
        _image.fillAmount = 1;
    }
    public void SetHp(float hp)
    {
        this.hp = hp;
        //_image.fillAmount = hp / maxHp;
    }
    public void SetFilled()
    {
        _image.type = Image.Type.Filled;
    }
}
