using UnityEngine;

public class AttackNPCState : State
{
    private readonly CharacterAnimationController _characterAnimationController;
    private readonly Transform _npc;
    private readonly Transform _target;

    public AttackNPCState(CharacterAnimationController characterAnimationController, Transform npc)
    {
        _characterAnimationController = characterAnimationController;
        _npc = npc;
        _target = GameObject.FindObjectOfType<FirstPersonMovement>().transform;
    }

    public override void OnStateEnter()
    {
        _characterAnimationController.SetBool(CharacterAnimationType.Attack, true);
    }

    public override void OnUpdate(float deltaTime)
    {
        var lookDirection = (_target.position - _npc.position).normalized;
        lookDirection.y = 0;
        _npc.rotation = Quaternion.LookRotation(lookDirection);
    }

    public override void OnStateExit()
    {
        _characterAnimationController.SetBool(CharacterAnimationType.Attack, false);
    }
}