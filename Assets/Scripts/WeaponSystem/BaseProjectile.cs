using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private Collider projectileCollider;
    private Rigidbody rigidBody;
    protected float projectileDamage;

    public void Launch(Vector3 force, float damage)
    {
        projectileCollider = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(force);
        projectileDamage = damage;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        transform.SetParent(collision.transform);
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(projectileDamage);
            FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Bow/bowHitEvent");
        }
        Stop();
    }

    private void LateUpdate()
    {
        RotateTowardsVelocity();
    }

    private void RotateTowardsVelocity()
    {
        if (rigidBody.velocity.sqrMagnitude > 0f)
            transform.forward = Vector3.Slerp(transform.forward, rigidBody.velocity.normalized, Time.deltaTime);
    }

    protected virtual void Stop()
    {
        projectileCollider.enabled = false;
        Destroy(rigidBody);
        this.enabled = false;
    }
}
