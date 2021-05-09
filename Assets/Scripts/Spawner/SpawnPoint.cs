using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Queue<(GameObject prefab, EnemyMover.FollowBehaviour followBehaviour)> SpawnQueue;
    public bool IsActive;
    public bool TakesNewSpawnsOrders = true;
    private float _spawnNextAfter;

    private Transform _spawnPointTransform { get; set; }

    private void Awake()
    {
        _spawnPointTransform = transform;
        SpawnQueue = new Queue<(GameObject, EnemyMover.FollowBehaviour)>();
    }

    public void Reset()
    {
        IsActive = false;
        TakesNewSpawnsOrders = true;
        _spawnNextAfter = 0f;
        SpawnQueue.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i), 2f);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, EnemyMover.FollowBehaviour followBehaviour)
    {
        var enemy = Instantiate(enemyPrefab, _spawnPointTransform);
        var enemyMover = enemy.GetComponent<EnemyMover>();
        enemyMover.SetFollowBehaviour(followBehaviour);
        
        SceneAIDirector.Instance.MonstersAlive += 1;
        SceneAIDirector.Instance.MonstersWaitingToSpawn -= 1;
    }

    public void QueueEnemySpawn(GameObject enemyPrefab, EnemyMover.FollowBehaviour followBehaviour, float spawnAfter)
    {
        if (!IsActive)
        {
            return;
        }
        SpawnQueue.Enqueue((enemyPrefab, followBehaviour));
        SceneAIDirector.Instance.MonstersWaitingToSpawn += 1;
        _spawnNextAfter = spawnAfter;
        TakesNewSpawnsOrders = false;
    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        _spawnNextAfter -= Time.deltaTime;
        if (SpawnQueue.Count > 0 && _spawnNextAfter < 0f)
        {
            var spawnData = SpawnQueue.Dequeue();
            SpawnEnemy(spawnData.prefab, spawnData.followBehaviour);
            TakesNewSpawnsOrders = true;
        }
    }
}
