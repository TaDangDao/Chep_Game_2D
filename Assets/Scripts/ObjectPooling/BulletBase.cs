using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] private float speed=10f;
    float dmg;
    private void Update()
    {
        transform.Translate(transform.up*speed*Time.deltaTime);
    }
    public void OnInit(float dmg)
    {
        this.dmg = dmg;
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            OnDespawn();
        }
    }
}
