using UnityEngine;

public class SingleShot : BulletMain
{
    private EffectSpawner effectSpawner;

    public override void Shot(Transform startPosition, int damage)
    {
        if (Physics.Raycast(startPosition.position, startPosition.forward, out var hit))
        {
            effectSpawner ??= ServiceLocator.GetService<EffectSpawner>();
            var decalListener = hit.transform.GetComponent<DecalListener>();
            effectSpawner.SpawnEffect(hit.point, decalListener ? decalListener.DecalType : EffectType.DecalEffect);

            var damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}