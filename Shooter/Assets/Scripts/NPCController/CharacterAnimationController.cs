using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController
{
    private readonly Animator _animator;

    private Dictionary<CharacterAnimationType, int> characterAnimationTypeHash =
        new Dictionary<CharacterAnimationType, int>();

    public Animator Animator => _animator;

    public CharacterAnimationController(Animator animator)
    {
        _animator = animator;
        foreach (CharacterAnimationType characterAnimationType in Enum.GetValues(typeof(CharacterAnimationType)))
        {
            characterAnimationTypeHash.Add(characterAnimationType,
                Animator.StringToHash(characterAnimationType.ToString()));
        }
    }

    public void SetBool(CharacterAnimationType characterAnimationType, bool value)
    {
        _animator.SetBool(characterAnimationTypeHash[characterAnimationType], value);
    }

    public void SetTrigger(CharacterAnimationType characterAnimationType)
    {
        _animator.SetTrigger(characterAnimationTypeHash[characterAnimationType]);
    }
}