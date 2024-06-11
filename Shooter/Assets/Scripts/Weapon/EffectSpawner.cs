using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] private EffectConfiguration[] effectConfigurations;

    private Dictionary<EffectType, IPool<TemporaryMonoObject>> _pools;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        ServiceLocator.Subscribe<EffectSpawner>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.UnSubscribe<EffectSpawner>();
    }

    private void Initialize()
    {
        _pools = new Dictionary<EffectType, IPool<TemporaryMonoObject>>();
        foreach (var effectConfiguration in effectConfigurations)
        {
            var factory = new FactoryMonoObject<TemporaryMonoObject>(effectConfiguration.Prefab.gameObject, transform);
            var pool = new Pool<TemporaryMonoObject>(factory, 2);
            _pools.Add(effectConfiguration.EffectType, pool);
        }
    }

    public void SpawnEffect(Transform spawnPosition, EffectType effectType,bool isSetParent = false)
    {
        var newEffect = _pools[effectType].Pull();
        newEffect.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);
        if (isSetParent)
        {
            newEffect.transform.parent = spawnPosition;
        }
    }
    
    public void SpawnEffect(Vector3 spawnPosition, EffectType effectType)
    {
        var newEffect = _pools[effectType].Pull();
        newEffect.transform.position = spawnPosition;
    }
}