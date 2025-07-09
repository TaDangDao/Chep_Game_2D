using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] PoolAmount[] poolAmounts;
    private void Awake()
    {
        //Load tu folder Resources 
        GameUnit[] units = Resources.LoadAll<GameUnit>("Pool/");
        for(int i = 0; i < poolAmounts.Length; i++)
        {
            SimplePool.Preload(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }
        for(int i=0;i< units.Length; i++)
        {
            SimplePool.Preload(units[i], 0, new GameObject(units[i].name).transform);
        }
    
    }
}
public enum PoolType
{
    Bullet_1 = 0,
    Bullet_2 = 1,
    Bullet_3 = 2
}
[System.Serializable]
public class PoolAmount
{
    public GameUnit prefab;
    public int amount;
    public Transform parent;
}
