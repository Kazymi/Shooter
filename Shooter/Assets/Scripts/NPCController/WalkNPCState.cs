using UnityEngine;
using UnityEngine.AI;

public class WalkNPCState : State
{
    private readonly CharacterAnimationController _characterAnimationController;
    private readonly NavMeshAgent _meshAgent;
    private readonly Transform _target;

    public bool IsOnPosition => Vector3.Distance(_meshAgent.transform.position, _target.position) < 1.6f;
    public bool IsOnAgrPosition => Vector3.Distance(_meshAgent.transform.position, _target.position) < 2.6f;

    public WalkNPCState(CharacterAnimationController characterAnimationController, NavMeshAgent meshAgent)
    {
        _characterAnimationController = characterAnimationController;
        _meshAgent = meshAgent;
        _target = GameObject.FindObjectOfType<FirstPersonMovement>().transform;
    }

    public override void OnStateEnter()
    {
        _meshAgent.enabled = true;
        _characterAnimationController.SetBool(CharacterAnimationType.Walk, true);
    }

    public override void OnStateExit()
    {
        _meshAgent.enabled = false;
        _characterAnimationController.SetBool(CharacterAnimationType.Walk, false);
    }

    public override void OnUpdate(float deltaTime)
    {
        _meshAgent.SetDestination(_target.position);
    }
}