using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spawner;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneAIDirector : MonoBehaviour
{
    public static SceneAIDirector Instance;
    
    private List<SpawnPoint> _spawnPoints;
    public int MonstersAlive;
    public int MinimumMonstersAlive;
    public int MaximumMonstersAlive;
    public int MonstersWaitingToSpawn;
    public List<GameObject> EnemyPrefabs;
    public float MinimumCashierTargetInterval;
    public float MaximumCashierTargetInterval;
    public float TimeLastSpawnedWithCashierTarget;
    public float ChanceToFollowCashier;
    public float SpawnInterval;
    public float MaxRandomSpawnDelay;

    private float _timeSinceLastSpawn;
    private float _nextSpawnDelay;

    private SpawnWindowManager _spawnWindowManager;

    public SceneAIDirector()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnPoints = FindObjectsOfType<SpawnPoint>().ToList();
        
        MovementController.Instance.SetTarget(Target.Player, GameObject.FindGameObjectWithTag("Player").transform);
        MovementController.Instance.SetTarget(Target.Cashier, GameObject.FindGameObjectWithTag("Cashier").transform);

        var spawnWindowStats = new SpawnWindowStats()
        {
            RandomDelay = MaxRandomSpawnDelay,
            ChanceToSpawnDouble = 0.1f,
            MinimumTimeBetweenSpawns = SpawnInterval,
        };
        _spawnWindowManager = new SpawnWindowManager(new List<(float time, SpawnWindowStats stats)>()
        {
            (0f, spawnWindowStats),
        });
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;
        if (MonstersAlive + MonstersWaitingToSpawn >= MaximumMonstersAlive)
        {
            return;
        }

        var stats = _spawnWindowManager.GetCurrentSpawnWindowStats();
        bool spawnWindowIsOpen = _timeSinceLastSpawn > stats.MinimumTimeBetweenSpawns + _nextSpawnDelay;
        
        if (spawnWindowIsOpen)
        {
            int enemyTypeIndex = Random.Range(0, EnemyPrefabs.Count);
            var followBehaviour = DecideFollowBehaviour();
            if (MonstersAlive + MonstersWaitingToSpawn < MaximumMonstersAlive)
            {
                SpawnSingleEnemy(EnemyPrefabs[enemyTypeIndex].gameObject, followBehaviour, _nextSpawnDelay);
            }
            else if (MonstersAlive + MonstersWaitingToSpawn < MinimumMonstersAlive)
            {
                SpawnEnemies(EnemyPrefabs[enemyTypeIndex], followBehaviour, 0f, 2);
            }
            else
            {
                bool shouldSpawnTwo = Random.Range(0, 1f) >= stats.ChanceToSpawnDouble;
                if (shouldSpawnTwo)
                {
                    SpawnEnemies(EnemyPrefabs[enemyTypeIndex].gameObject, followBehaviour, _nextSpawnDelay, 2);
                }
                else
                {
                    SpawnSingleEnemy(EnemyPrefabs[enemyTypeIndex], followBehaviour, _nextSpawnDelay);
                }
            }

            _timeSinceLastSpawn = 0f;
            _nextSpawnDelay = _nextSpawnDelay = MonstersAlive + MonstersWaitingToSpawn <= MinimumMonstersAlive
                ? 0f
                : Random.Range(0f, stats.RandomDelay);
        }
    }

    private EnemyMover.FollowBehaviour DecideFollowBehaviour()
    {
        if (TimeLastSpawnedWithCashierTarget - MaximumCashierTargetInterval > Time.realtimeSinceStartup)
        {
            TimeLastSpawnedWithCashierTarget = Time.realtimeSinceStartup;
            return EnemyMover.FollowBehaviour.FollowCashier;
        }
        if (TimeLastSpawnedWithCashierTarget + MinimumCashierTargetInterval > Time.realtimeSinceStartup)
        {
            var random = Random.Range(0f, 1f);
            
            if (random < ChanceToFollowCashier)
            {
                TimeLastSpawnedWithCashierTarget = Time.realtimeSinceStartup;
                return EnemyMover.FollowBehaviour.FollowCashier;
            }
        }
        
        return EnemyMover.FollowBehaviour.FollowPlayer;
    }

    public void SpawnEnemies(GameObject prefab, EnemyMover.FollowBehaviour followBehaviour, float spawnAfter, int count)
    {
        if (count == 1)
        {
            SpawnSingleEnemy(prefab, followBehaviour, spawnAfter);
            return;
        }

        var leastMonstersInQueue = _spawnPoints
            .Where(sp => sp.TakesNewSpawnsOrders)
            .OrderBy(sp => sp.SpawnQueue.Count)
            .ToList();

        if (count > leastMonstersInQueue.Count())
        {
            for (int i = 0; i < count; i++)
            {
                SpawnSingleEnemy(prefab, followBehaviour, spawnAfter);
            }
        }
        else
        {
            var points = leastMonstersInQueue.Take(count);
            foreach (var point in points)
            {
                point.QueueEnemySpawn(prefab, followBehaviour, spawnAfter);
            }
        }
    }

    public void SpawnSingleEnemy(GameObject enemyPrefab, EnemyMover.FollowBehaviour followBehaviour, float spawnAfter)
    {
        var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        
        spawnPoint.QueueEnemySpawn(enemyPrefab, followBehaviour, spawnAfter);
    }
}
