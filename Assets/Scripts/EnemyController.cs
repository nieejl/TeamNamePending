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

    private Collider _collider;

    private Rigidbody _rigidbody;

    [SerializeField]private PlayerData PlayerHealth;

    [SerializeField] public float DeathFallThroughFloorSpeed;

    [SerializeField]
    private Vector3 _spawnOffset;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        Debug.Log("Enemy taking damage. hp now " + CurrentHealth);
        if (CurrentHealth <= 0f && !_isDying)
        {
            DropSystem.Instance.CheckIfShouldSpawnDropItemAtPosition(transform.position + _spawnOffset);
            Die();
        }
    }

    public void Update()
    {
        // if (CurrentHealth <= 0f && !_isDying)
        // {
        //     SceneAIDirector.Instance.MonstersAlive -= 1;
        //     _isDying = true;
        // }
        // if (_isDying)
        // {
        //     Die();
        // }
    }

    private void Die()
    {
        SceneAIDirector.Instance.MonstersAlive -= 1;
        var position = transform.position;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.detectCollisions = false;
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
                FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Player/Dialogue/Enemies/enemyDeadEvent");
            }
            _componentsDisabled = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision with player");
            if (!_isDying)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Player/Dialogue/Enemies/enemyAttackEvent");
                _isDying = true;
                Die();
                PlayerHealth.ChangeValue(-1);
            }
        }
        else
        {
            Debug.Log("collision with " + other.gameObject.tag);
        }
    }
}
