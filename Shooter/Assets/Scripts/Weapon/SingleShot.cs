using UnityEngine;

public class SingleShot : BulletMain
{
    private EffectSpawner effectSpawner;

    public override void Shot(Transform startPosition)
    {
        if (Physics.Raycast(startPosition.position, startPosition.forward, out var hit))
        {
            effectSpawner ??= ServiceLocator.GetService<EffectSpawner>();
            effectSpawner.SpawnEffect(hit.point, EffectType.DecalEffect);
        }
    }
}