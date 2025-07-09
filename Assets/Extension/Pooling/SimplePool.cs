using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public static class SimplePool 
{
    private static Dictionary<PoolType,Pool> poolInstance = new Dictionary<PoolType,Pool>();
    // khoi tao pool moi
    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if(prefab == null)
        {
            return;
        }
        if (!poolInstance.ContainsKey(prefab.poolType) || poolInstance[prefab.poolType]==null){
            Pool p = new Pool();
            p.PreLoad(prefab, amount, parent);
            poolInstance[prefab.poolType] = p;
        }
    }
    // lay phan tu ra
    public static T Spawn<T>(PoolType poolType,Vector3 pos, Quaternion rot) where T: GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            return null;
        }
        return poolInstance[poolType].Spawn(pos, rot) as T;
    }
    // tra phan tu ve pool
    public static void Despawn(GameUnit gameUnit)
    {
        if (!poolInstance.ContainsKey(gameUnit.poolType))
        {
            Debug.Log("!");
        }
        else
        {
            poolInstance[gameUnit.poolType].Despawn(gameUnit);
        }
    }
    //  thu thap phan tu
    public static void Collect(PoolType type)
    {
        if (!poolInstance.ContainsKey(type))
        {
            Debug.Log("!");
        }
        else
        {
            poolInstance[type].Collect();
        }
    }
    // thu thap tat ca
    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }
    }
    // xoa 1 pool
    public  static void Release(PoolType type)
    {
        if (!poolInstance.ContainsKey(type))
        {
            Debug.Log("!");
        }
        else
        {
            poolInstance[type].Release();
        }
    }
    // xoa tat ca pool
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}
public class Pool
{
    Transform parent;
    GameUnit prefab;
    //list chua cac unit dang o trong pool
    Queue<GameUnit> inactives= new Queue<GameUnit>();
    // list chua cac unit dang dc su dung
    List<GameUnit> actives= new List<GameUnit>();
    // khoi tao pool
    public void PreLoad(GameUnit prefab,int amount, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
        Debug.Log(amount);
        for(int i=0;i<amount;i++) {
            Despawn(GameObject.Instantiate(prefab, parent));
        }
    }
    // lay phan tu tu ppol
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;
        if (inactives.Count <= 0)
        {
            unit= GameObject.Instantiate(prefab, pos,rot,parent);
            return unit;
        }
        else
        {
            unit = inactives.Dequeue();
            unit.gameObject.SetActive(true);
        }
        unit.TF.SetPositionAndRotation(pos, rot);
        actives.Add(unit);
        return unit;
    }
    // tra phan tu ve pool
    public void Despawn(GameUnit gameUnit)
    {
        if (gameUnit!=null&&gameUnit.gameObject.activeInHierarchy)
        {
            actives.Remove(gameUnit);
            inactives.Enqueue(gameUnit);
            gameUnit.gameObject.SetActive(false);
        }
    }
    // thu thap tat ca pha tu dang dung ve pool
    public void Collect()
    {
        while(inactives.Count>0) {
            Despawn(actives[0]);
        }
    }
    // destroy tat ca phan tu
    public void Release()
    {
        Collect();
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }
        inactives.Clear();
    }
}