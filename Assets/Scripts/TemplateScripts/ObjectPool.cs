using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools = null;

    private void Awake()
    {
        for (int i1 = 0; i1 < pools.Length; i1++)
        {
            pools[i1].pooledObjects = new Queue<GameObject>();

            for (int i2 = 0; i2 < pools[i1].poolSize; i2++)
            {
                GameObject obj = Instantiate(pools[i1].objectPrefab);
                obj.SetActive(false);
                pools[i1].pooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledObjectAdd(int objectType)
    {
        GameObject obj = pools[objectType].pooledObjects.Dequeue();
        obj.SetActive(true);
        pools[objectType].pooledObjects.Enqueue(obj);
        return obj;
    }
    public GameObject GetPooledObject(int objectType)
    {
        GameObject obj = pools[objectType].pooledObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void AddObject(int objectType, GameObject Obj)
    {
        Obj.SetActive(false);
        pools[objectType].pooledObjects.Enqueue(Obj);
    }
}
