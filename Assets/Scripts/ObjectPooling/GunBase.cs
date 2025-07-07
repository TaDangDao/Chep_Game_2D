using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField] private float frameRate = 1f;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= frameRate)
        {
            time -= frameRate;
            Shoot();
        }
    }
    public virtual void Shoot()
    {

    }
}
