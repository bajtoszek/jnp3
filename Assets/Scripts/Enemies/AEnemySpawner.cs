using UnityEngine;

public abstract class AEnemySpawner<T> : MonoBehaviour where T : APooledObject
{
    [SerializeField]
    private ASpawnerConfig<T> m_Config = null;

    [SerializeField]
    private float m_Timer = 0f;
    
    [SerializeField]
    private int m_BossTimer = 0;

    [SerializeField]
    private int m_SpawnedEnemies = 0;

    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= m_Config.SpawnIntervalSeconds) {
            m_Timer = 0f;
            m_BossTimer += 1;

            if (m_BossTimer >= m_Config.BossSpawnRatio) {
                m_BossTimer = 0;
                SpawnBoss();
            } else {
                SpawnEnemy();
            }

            m_SpawnedEnemies += 1;
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

        SetupEnemy(enemy, m_SpawnedEnemies * 10);
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

        SetupEnemy(enemy, m_SpawnedEnemies * 10);
    }

    protected abstract void SetupEnemy(T enemy, int deltaTotalHealthPoints=0);
}
