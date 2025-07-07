using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrow : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;
    private List<GameObject> gameObjects = new List<GameObject>();
    public List<GameObject> pooling = new List<GameObject>();
    private Vector2 pos;
    public float time;
    public float timer;
    private int direction;
    private bool isFlaming;
    private void Awake()
    {
        pos= transform.position;
        OnInit();
        time = 1000;
        timer = 0;
        direction = 1;
        isFlaming=false;
    }
    private void Update()
    {
        if (timer < time)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (!isFlaming)
            {
                ClearPoolInRange();
                timer = 0;
            }
            else
            {
                timer -= Time.deltaTime * 10f;
            }
           
        }
    }
    public void OnInit()
    {
        for(int i = 0; i < 11; i++)
        {
            GameObject x = Instantiate(firePrefab);
            x.SetActive(false); 
            pooling.Add(x);
        }
    }
    public GameObject GetPoolingObject()
    {
        for(int i=0;i < pooling.Count; i++) {
            if (!pooling[i].gameObject.activeInHierarchy)
            {
                return pooling[i];
            }
        }
        GameObject x = Instantiate(firePrefab);
        x.SetActive(false);
        pooling.Add(x);
        return x;
    }
    public void Attack(int x)
    {
        pos = transform.position;
        direction = x;
        StartCoroutine(ThrowFlame());
    }
    public IEnumerator ThrowFlame()
    {
        isFlaming = true;
        for(int i=0;i<11;i++)
        {
            GameObject x = GetPoolingObject();
            x.SetActive(true);
            x.transform.position= pos;
            gameObjects.Add(x);
            pos += new Vector2(0.5f*direction, 0);
            yield return new WaitForSeconds(0.05f);
        }
        yield return OnDespawn();

    }
    public IEnumerator OnDespawn()
    {
        pos=transform.position;
        isFlaming = false;
        yield return true;
    }
    public void ClearPoolInRange()
    {
        Debug.LogWarning("xoa");
        for(int i = 11; i < pooling.Count; i++)
        {
            Destroy(pooling[i].gameObject);
        }
        pooling.RemoveRange(11, pooling.Count-11);
        Debug.Log(pooling.Count);
    }
}
