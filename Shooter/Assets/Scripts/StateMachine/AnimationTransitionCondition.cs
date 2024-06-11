using UnityEngine;

public class AnimationTransitionCondition : ACondition
{
    private readonly Animator _characterAnimationController;
    private readonly string _transitionName;
    private readonly float _exitTime;

    public AnimationTransitionCondition(Animator characterAnimationController,
        string transitionName, float exitTime = 0.9f)
    {
        _characterAnimationController = characterAnimationController;
        _transitionName = transitionName;
        _exitTime = exitTime;
    }

    public override bool IsConditionSuccess()
    {
        return _characterAnimationController.GetCurrentAnimatorStateInfo(0).IsName(_transitionName.ToString()) &&
               _characterAnimationController.GetCurrentAnimatorStateInfo(0).normalizedTime > _exitTime;
    }
}