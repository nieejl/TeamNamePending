using UnityEngine;
public class StaffProjectile : BaseProjectile
{
    private ParticleSystem projectileParticles;
    private ParticleSystem impactParticles;

    private void Awake()
    {
        projectileParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
        impactParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    protected override void Stop()
    {
        base.Stop();
        var mainModule = projectileParticles.main;
        mainModule.loop = false;
        impactParticles.Play();
        Invoke(nameof(StopAllParticles), impactParticles.main.duration);
    }

    private void StopAllParticles()
    {
        projectileParticles.Stop();
        impactParticles.Stop();
    }
}
