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
    
    // used to keep track of how many monsters to spawn. Edited from outside     
    public int MonstersAlive;
    public int MonstersWaitingToSpawn;

    [SerializeField] private EventTriggerData SpawnerState;
    [SerializeField] private int MinimumMonstersAlive;
    [SerializeField] private int MaximumMonstersAlive;
    [SerializeField] private List<GameObject> EnemyPrefabs;
    [SerializeField] private float MinimumCashierTargetInterval;
    [SerializeField] private float MaximumCashierTargetInterval;
    [SerializeField] private float ChanceToTargetCashier;
    [SerializeField] private float SpawnInterval;
    [SerializeField] private float MaxRandomSpawnDelay;

    // high value to spawn enemy with cashier target at start.
    private float _timeLastSpawnedWithCashierTarget = 200f;
    private float _timeSinceLastSpawn;
    private float _nextSpawnDelay;

    private List<SpawnPoint> _spawnPoints;
    private SpawnWindowManager _spawnWindowManager;

    public SceneAIDirector()
    {
        Instance = this;
    }
    
    void Start()
    {
        _spawnPoints = FindObjectsOfType<SpawnPoint>().ToList();
        Reset();
    }

    public void Reset()
    {
        MovementController.Instance.SetTarget(Target.Player, GameObject.FindGameObjectWithTag("Player").transform);
        MovementController.Instance.SetTarget(Target.Cashier, GameObject.FindGameObjectWithTag("Cashier").transform);

        MonstersAlive = 0;
        MonstersWaitingToSpawn = 0;
        var spawnWindowStats = new SpawnWindowStats()
        {
            RandomDelay = MaxRandomSpawnDelay,
            ChanceToSpawnDouble = 0.1f,
            MinimumTimeBetweenSpawns = SpawnInterval,
        };
        _spawnWindowManager = new SpawnWindowManager(new List<(float time, SpawnWindowStats stats)>()
        {
            (0f, spawnWindowStats),
            // so to add new spawn window stats, just add as below.
            // (60f, spawnWindowStats),
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!SpawnerState.IsActive)
        {
            if (SpawnerState.ShouldReset)
            {
                Reset();
                SpawnerState.ShouldReset = false;
            }
            return;
        }
        _timeSinceLastSpawn += Time.deltaTime;
        _timeLastSpawnedWithCashierTarget += Time.deltaTime;
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
        if (_timeLastSpawnedWithCashierTarget > MaximumCashierTargetInterval)
        {
            _timeLastSpawnedWithCashierTarget = 0f;
            return EnemyMover.FollowBehaviour.FollowCashier;
        }
        if (_timeLastSpawnedWithCashierTarget > MinimumCashierTargetInterval
            && _timeLastSpawnedWithCashierTarget < MaximumCashierTargetInterval)
        {
            var random = Random.Range(0f, 1f);
            
            if (random < ChanceToTargetCashier)
            {
                _timeLastSpawnedWithCashierTarget = 0f;
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
        var activeSpawnPoints = _spawnPoints.Where((sp => sp.IsActive)).ToList();
        var spawnPoint = activeSpawnPoints[Random.Range(0, activeSpawnPoints.Count)];
        
        spawnPoint.QueueEnemySpawn(enemyPrefab, followBehaviour, spawnAfter);
    }
}
