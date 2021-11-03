using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APooledObject : MonoBehaviour
{
    public PrefabPool Pool { get; set; }

    protected void ReturnToPool()
    {
        Pool.Return(gameObject);
    }
}
