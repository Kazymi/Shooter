using System;
using UnityEngine;

[Serializable]
public class EffectConfiguration
{
    [field: SerializeField] public EffectType EffectType;
    [field: SerializeField] public TemporaryMonoObject Prefab;
}