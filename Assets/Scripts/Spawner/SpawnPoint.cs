using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform SpawnPointTransform { get; private set; }
    public Queue<(GameObject prefab, EnemyMover.FollowBehaviour followBehaviour)> SpawnQueue;
    public bool IsActive;
    public float MinTimeBetweenSpawns;
    public float LastTimeSpawned { get; set; }

    private void Awake()
    {
        SpawnPointTransform = transform;
        SpawnQueue = new Queue<(GameObject, EnemyMover.FollowBehaviour)>();
    }

    public void SpawnInstant(GameObject enemyPrefab, bool updateLastTimeSpawned = true)
    {
        Instantiate(enemyPrefab);
        if (updateLastTimeSpawned)
        {
            LastTimeSpawned = Time.realtimeSinceStartup;
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab, EnemyMover.FollowBehaviour followBehaviour)
    {
        Debug.Log("Spawned enemy");
        LastTimeSpawned = Time.realtimeSinceStartup;
        var enemy = Instantiate(enemyPrefab, SpawnPointTransform);
        var enemyMover = enemy.GetComponent<EnemyMover>();
        enemyMover.SetFollowBehaviour(followBehaviour);
        SceneAIDirector.Instance.MonstersAlive += 1;
        SceneAIDirector.Instance.MonstersWaitingToSpawn -= 1;
    }

    public void QueueEnemySpawn(GameObject enemyPrefab, EnemyMover.FollowBehaviour followBehaviour)
    {
        SpawnQueue.Enqueue((enemyPrefab, followBehaviour));
        SceneAIDirector.Instance.MonstersWaitingToSpawn += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnQueue.Count > 0 && LastTimeSpawned + MinTimeBetweenSpawns <= Time.realtimeSinceStartup)
        {
            var spawnData = SpawnQueue.Dequeue();
            SpawnEnemy(spawnData.prefab, spawnData.followBehaviour);
        }
    }
}
