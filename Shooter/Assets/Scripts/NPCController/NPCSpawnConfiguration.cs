using System;
using UnityEngine;

[Serializable]
public class NPCSpawnConfiguration
{
    [field: SerializeField] public Transform SpawnCenter { get; private set; }
    [field: SerializeField] public Vector2 SpawnScale { get; private set; }
}