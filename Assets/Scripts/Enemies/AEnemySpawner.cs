using UnityEngine;

public abstract class AEnemySpawner<T> : MonoBehaviour where T : APooledObject
{
    [SerializeField]
    private float m_SpawnIntervalSeconds = 3f;

    [SerializeField]
    private T m_EnemyPrefab = null;

    private PrefabPool<T> m_PrefabPool = null;

    private float m_Timer = 0f;

    private void Awake()
    {
        m_PrefabPool = PoolManager.Instance.GetPool(m_EnemyPrefab);
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_SpawnIntervalSeconds)
        {
            m_Timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        T enemy = m_PrefabPool.Get();
        SetupEnemy(enemy);
    }

    protected abstract void SetupEnemy(T enemy);
}
