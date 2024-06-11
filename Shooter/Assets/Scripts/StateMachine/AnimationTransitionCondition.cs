using System;
using UnityEngine;

public class AnimationTransitionCondition : ACondition
{
    private readonly Animator _characterAnimationController;
    private readonly string _transitionName;
    private readonly float _exitTime;
    private readonly int layer;
    
    public AnimationTransitionCondition(Animator characterAnimationController,
        string transitionName, float exitTime = 0.9f, int layer = 0)
    {
        _characterAnimationController = characterAnimationController;
        _transitionName = transitionName;
        _exitTime = exitTime;
        this.layer = layer;
    }

    public override bool IsConditionSuccess()
    {
        return _characterAnimationController.GetCurrentAnimatorStateInfo(layer).IsName(_transitionName.ToString()) &&
               _characterAnimationController.GetCurrentAnimatorStateInfo(layer).normalizedTime > _exitTime;
    }
}

public class AnimationTransitionConditionWithFunk : ACondition
{
    private readonly Func<bool> _returnValue;
    private readonly Animator _characterAnimationController;
    private readonly string _transitionName;
    private readonly float _exitTime;

    public AnimationTransitionConditionWithFunk(Func<bool> returnValue, Animator characterAnimationController,
        string transitionName, float exitTime = 0.9f)
    {
        _returnValue = returnValue;
        _characterAnimationController = characterAnimationController;
        _transitionName = transitionName;
        _exitTime = exitTime;
    }

    public override bool IsConditionSuccess()
    {
        return _returnValue.Invoke() && _characterAnimationController.GetCurrentAnimatorStateInfo(0)
                   .IsName(_transitionName.ToString()) &&
               _characterAnimationController.GetCurrentAnimatorStateInfo(0).normalizedTime > _exitTime;
    }
}