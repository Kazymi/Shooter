using System;
using UnityEngine;

[Serializable]
public class WeaponConfiguration
{
    [field: SerializeField] public GameObject HandWeapon { get; private set; }
    [field: SerializeField] public GameObject BodyWeapon { get; private set; }
}