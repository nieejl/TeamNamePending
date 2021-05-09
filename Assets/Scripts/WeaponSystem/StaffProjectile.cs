using UnityEngine;
public class StaffProjectile : BaseProjectile
{
    [SerializeField]
    private float ExplosionRange = 5f;
    private ParticleSystem projectileParticles;
    private ParticleSystem impactParticles;

    private void Awake()
    {
        projectileParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
        impactParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision from base projectile with " + collision.transform.tag);

        foreach (var nearbyObjects in Physics.OverlapSphere(collision.GetContact(0).point, ExplosionRange)) 
            if (nearbyObjects.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                Debug.Log("staff projectile dealing " + projectileDamage);
                damageable.TakeDamage(projectileDamage);
                FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Staff/staffHitEvent");
            }

        Stop();
    }

    protected override void Stop()
    {
        base.Stop();
        var mainModule = projectileParticles.main;
        mainModule.loop = false;
        projectileParticles.Stop();
        impactParticles.Play();
        Invoke(nameof(StopAllParticles), impactParticles.main.duration);
    }

    private void StopAllParticles()
    {
        projectileParticles.Stop();
        impactParticles.Stop();
    }
}
