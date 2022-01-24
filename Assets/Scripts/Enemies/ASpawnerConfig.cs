using System.Collections.Generic;
using UnityEngine;

public abstract class ASpawnerConfig<T> : ScriptableObject where T : APooledObject
{
    public List<T> Enemies = new List<T>();
    public List<T> Bosses = new List<T>();

    public float SpawnIntervalSeconds = 1f;

    public int BossSpawnRatio = 50;

    public T GetRandomEnemy()
    {
        if (Enemies == null || Enemies.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, Enemies.Count);
        return Enemies[randomIndex];
    }

    public T GetBoss()
    {
        if (Bosses == null || Bosses.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, Bosses.Count);
        return Bosses[randomIndex];
    }
}

