using UnityEngine;

public class DeathEffectSpawner : MonoBehaviour
{
    [Header("Asset Connection")]
    [Tooltip("Particle System prefab instantiated at this object's position when its health component reports death.")]
    public ParticleSystem deathParticlesPrefab;

    private bool spawned;

    public void SpawnDeathEffectOnce()
    {
        if (spawned)
        {
            return;
        }

        spawned = true;

        if (deathParticlesPrefab == null)
        {
            return;
        }

        ParticleSystem particles = Instantiate(
            deathParticlesPrefab,
            transform.position,
            Quaternion.identity
        );

        ParticleSystem.MainModule main = particles.main;
        float lifetime = main.duration + main.startLifetime.constantMax;
        Destroy(particles.gameObject, lifetime + 0.1f);
    }
}
