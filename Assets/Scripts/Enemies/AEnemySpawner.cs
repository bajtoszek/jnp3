using UnityEngine;

public abstract class AEnemySpawner<T> : MonoBehaviour where T : APooledObject
{
    [SerializeField]
    private ASpawnerConfig<T> m_Config = null;

    private float m_Timer = 0f;
    
    private int m_BossTimer = 0;

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer < m_Config.SpawnIntervalSeconds)
        {
            return;
        }

        m_Timer = 0f;
        m_BossTimer++;
        SpawnEnemy();

        if (m_BossTimer >= m_Config.BossSpawnRatio)
        {
            m_BossTimer = 0;
            SpawnBoss();
        }
    }

    private void SpawnEnemy()
    {
        T randomEnemyPrefab = m_Config.GetRandomEnemy();

        if (randomEnemyPrefab == null)
        {
            return;
        }

        PrefabPool<T> pool = PoolManager.Instance.GetPool(randomEnemyPrefab);
        T enemy = pool.Get();

        SetupEnemy(enemy);
    }

    private void SpawnBoss()
    {
        T randomBossPrefab = m_Config.GetBoss();
        if (randomBossPrefab == null)
        {
            return;
        }

        PrefabPool<T> pool = PoolManager.Instance.GetPool(randomBossPrefab);
        T enemy = pool.Get();

        SetupEnemy(enemy);
    }

    protected abstract void SetupEnemy(T enemy);
}
