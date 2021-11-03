using System.Collections.Generic;
using UnityEngine;

public class PrefabPool 
{
    private Queue<GameObject> m_Pool = new Queue<GameObject>();
    
    private GameObject m_Prefab;

    public PrefabPool(GameObject prefab)
    {
        m_Prefab = prefab;
    }

    public GameObject Get()
    {
        GameObject instance;

        if (m_Pool.Count > 0)
        {
            instance = m_Pool.Dequeue();
            return instance;
        }

        instance = GameObject.Instantiate(m_Prefab);

        APooledObject pooledObject = instance.GetComponent<APooledObject>();
        if (pooledObject != null)
        {
            pooledObject.Pool = this;
        }

        return instance;
    }

    public void Return(GameObject instance)
    {
        if (instance == null)
        {
            return;
        }

        instance.SetActive(false);
        m_Pool.Enqueue(instance);
    }
}
