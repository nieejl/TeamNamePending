using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneAIDirector : MonoBehaviour
{
    public static SceneAIDirector Instance;
    
    private List<SpawnPoint> _spawnPoints;
    public int MonstersAlive;
    public int MinimumMonstersAlive;
    public int MaximumMonstersAlive;
    public float SpawningIntensity;
    public List<GameObject> EnemyPrefabs;
    public float MinimumCashierTargetInterval;
    public float MaximumCashierTargetInterval;
    public float TimeLastSpawnedWithCashierTarget;
    public float ChanceToFollowCashier;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (MonstersAlive >= MaximumMonstersAlive)
        {
            Debug.Log("Max amount of monsters alive.");
            return;
        }
        if (MonstersAlive <= MinimumMonstersAlive)
        {
            int enemyIndex = Random.Range(0, EnemyPrefabs.Count);
            var followBehaviour = DecideFollowBehaviour();
            SpawnAtEmptiestPoint(EnemyPrefabs[enemyIndex].gameObject, followBehaviour);
        }
    }

    EnemyMover.FollowBehaviour DecideFollowBehaviour()
    {
        if (TimeLastSpawnedWithCashierTarget + MinimumCashierTargetInterval > Time.realtimeSinceStartup)
        {
            return EnemyMover.FollowBehaviour.FollowCashier;
        }
        if (TimeLastSpawnedWithCashierTarget + MaximumCashierTargetInterval > Time.realtimeSinceStartup)
        {
            return EnemyMover.FollowBehaviour.FollowPlayer;
        }

        var random = Random.Range(0f, 1f);

        if (random < ChanceToFollowCashier)
        {
            TimeLastSpawnedWithCashierTarget = Time.realtimeSinceStartup;
            return EnemyMover.FollowBehaviour.FollowCashier;
        }

        return EnemyMover.FollowBehaviour.FollowPlayer;
    }

    public void SpawnAtEmptiestPoint(GameObject enemyPrefab, EnemyMover.FollowBehaviour followBehaviour)
    {
        var leastMonstersInQueue = _spawnPoints.OrderBy(sp => sp.SpawnQueue.Count);
        var spawnPoint = leastMonstersInQueue.FirstOrDefault(sp => sp.IsActive);
        if (spawnPoint == null)
        {
            Debug.LogWarning("SceneAIDirector - No active spawn points. Cant spawn enemy");
            return;
        }
        
        spawnPoint.QueueEnemySpawn(enemyPrefab, followBehaviour);
    }

    private void SpawnAtPoint(GameObject prefab, SpawnPoint spawnPoint)
    {
        Instantiate(prefab, spawnPoint.SpawnPointTransform);
    }

    private void SetEnemyTarget(GameObject enemy, EnemyMover.FollowBehaviour followBehaviour)
    {
        enemy.GetComponent<EnemyMover>().SetFollowBehaviour(followBehaviour);
    }
}
