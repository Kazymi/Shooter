using UnityEngine;

[RequireComponent(typeof(ShotSystem))]
public class ShotEffectSpawner : MonoBehaviour
{
    [SerializeField] private EffectType effectType;
    [SerializeField] private Transform effectSpawnPosition;


    [SerializeField] private EffectType bulletEffectType;
    [SerializeField] private Transform bulletSpawnPosition;

    private EffectSpawner effectSpawner;

    private void Awake()
    {
        GetComponent<ShotSystem>().Shooted += SpawnEffect;
    }

    private void SpawnEffect()
    {
        effectSpawner ??= ServiceLocator.GetService<EffectSpawner>();
        effectSpawner.SpawnEffect(effectSpawnPosition, effectType);
        effectSpawner.SpawnEffect(bulletSpawnPosition, bulletEffectType);
    }
}