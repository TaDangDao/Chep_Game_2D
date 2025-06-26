using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject hitVFX;
    public void OnInit(int direction)
    {
        rb.velocity = transform.right*direction * 5f;
        Invoke(nameof(OnDespawn), 4f);
    }
    public void UpgradeBehavior(int level)
    {

    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVFX,collision.transform.position, Quaternion.identity);
            OnDespawn();
        }
    }
}
