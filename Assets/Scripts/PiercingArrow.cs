using UnityEngine;

public class PiercingArrow : BaseProjectile
{
    private int piercedTargetsCounter = 3;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            if (piercedTargetsCounter == 0)
            {
                transform.SetParent(collision.transform);
                damageable.TakeDamage(projectileDamage);
                Stop();
            }
            else
            {
                damageable.TakeDamage(projectileDamage);
                piercedTargetsCounter--;
            }
        }
    }

    protected override void Stop()
    {
        base.Stop();
        var childrenParticles = GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in childrenParticles)
            particle.Stop();
    }
}
