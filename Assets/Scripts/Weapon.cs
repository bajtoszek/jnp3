using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ProjectilePrefab = null;

    [SerializeField]
    private Transform m_Emitter = null;

    [SerializeField]
    private float m_RateOfFire = 10f;

    private PrefabPool m_Pool = null;

    private Coroutine m_LimiterCoroutine = null;

    private void Awake()
    {
        m_Pool = new PrefabPool(m_ProjectilePrefab);
    }

    public bool Fire()
    {
        if (m_LimiterCoroutine != null)
        {
            return false;
        }

        m_LimiterCoroutine = StartCoroutine(RateOfFireLimiterCoroutine());
        return true;       
    }

    private IEnumerator RateOfFireLimiterCoroutine()
    {
        GameObject projectileInstance = m_Pool.Get();//Instantiate(m_ProjectilePrefab, m_Emitter.position, m_Emitter.rotation);

        projectileInstance.transform.SetPositionAndRotation(m_Emitter.position, m_Emitter.rotation);
        projectileInstance.SetActive(true);

        yield return new WaitForSeconds(1f / m_RateOfFire);

        m_LimiterCoroutine = null;
    }
}
