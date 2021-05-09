using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    public float MaxHealth;

    public float CurrentHealth;

    private bool _isDying;

    private bool _componentsDisabled;

    [SerializeField] public float DeathFallThroughFloorSpeed;

    [SerializeField]
    private Vector3 _spawnOffset;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0f && !_isDying)
        {
            DropSystem.Instance.CheckIfShouldSpawnDropItemAtPosition(transform.position + _spawnOffset);
            SceneAIDirector.Instance.MonstersAlive -= 1;
            _isDying = true;
        }
    }

    public void Update()
    {
        // if (CurrentHealth <= 0f && !_isDying)
        // {
        //     SceneAIDirector.Instance.MonstersAlive -= 1;
        //     _isDying = true;
        // }
        if (_isDying)
        {
            Die();
        }
    }

    private void Die()
    {
        var position = transform.position;
        if (position.y > -2f)
        {
            var distance = Time.deltaTime * DeathFallThroughFloorSpeed;
            transform.Translate(Vector3.down * distance, Space.World);
            if (!_componentsDisabled)
            {
                var mover = GetComponent<EnemyMover>();
                var agent = GetComponent<NavMeshAgent>();
                agent.isStopped = true;
                agent.enabled = false;
                mover.enabled = false;
            }
            _componentsDisabled = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
