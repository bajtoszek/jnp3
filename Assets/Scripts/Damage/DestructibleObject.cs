using UnityEngine;
using UnityEngine.Events;

public class DestructibleObject : MonoBehaviour
{
    public UnityEvent OnDestroyed = new UnityEvent();

    [SerializeField]
    private int m_TotalHealthpoints = 100;

    private int m_CurrentHealthpoints = 0;

    private bool m_IsDestroyed = false;

    private void Awake()
    {
        Reset();
    }

    public void ApplyDamage(DamageInfo damageInfo)
    {
        if (m_IsDestroyed)
        {
            return;
        }

        m_CurrentHealthpoints -= damageInfo.DamageAmount;

        if (m_CurrentHealthpoints <= 0)
        {
            m_IsDestroyed = true;
            OnDestroyed.Invoke();
        }
    }

    private void Reset()
    {
        m_CurrentHealthpoints = m_TotalHealthpoints;
        m_IsDestroyed = false;
    }
}
