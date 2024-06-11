using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController
{
    private readonly Animator _animator;
    private Dictionary<WeaponAnimationType, int> weaponAnimationHash = new Dictionary<WeaponAnimationType, int>();

    public Animator Animator => _animator;

    public WeaponAnimationController(Animator animator)
    {
        _animator = animator;
        foreach (WeaponAnimationType weaponAnimationType in Enum.GetValues(typeof(WeaponAnimationType)))
        {
            weaponAnimationHash.Add(weaponAnimationType, Animator.StringToHash(weaponAnimationType.ToString()));
        }
    }

    public void SetBool(WeaponAnimationType weaponAnimationType, bool value)
    {
        _animator.SetBool(weaponAnimationHash[weaponAnimationType], value);
    }

    public void SetTrigger(WeaponAnimationType weaponAnimationType)
    {
        _animator.SetTrigger(weaponAnimationHash[weaponAnimationType]);
    }
}