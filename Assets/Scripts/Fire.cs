using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float dmg = 10;
    private void OnEnable()
    {
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy") {
            collision.GetComponent<Character>().OnHit(dmg);
        }
    }
}
