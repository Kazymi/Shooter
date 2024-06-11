using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class NPCStateMachine : MonoPooled
{
    [SerializeField] private string currentState;
    [SerializeField] private NPCHealthController healthController;

    private StateMachine stateMachine;
    private bool isDead;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        healthController.OnHealthEmpty += OnNPCDead;
    }

    public override void Initialize()
    {
        base.Initialize();
        InitializeStateMachine();
        isDead = false;
        collider.enabled = true;
    }

    private void OnNPCDead()
    {
        isDead = true;
        collider.enabled = false;
        stateMachine.SetState(new State());
        StartCoroutine(ReturnToPoolCoroutine());
    }

    private IEnumerator ReturnToPoolCoroutine()
    {
        yield return new WaitForSeconds(6f);
        ReturnToPool();
    }

    private void Update()
    {
        if (isDead) return;
        currentState = stateMachine.CurrentState.ToString();
        stateMachine?.OnUpdate();
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        stateMachine?.OnFixedUpdate();
    }

    private void InitializeStateMachine()
    {
        var characterNPCAnimatorController = new CharacterAnimationController(GetComponent<Animator>());

        var showWeapon = new ShowWeaponNPCState(characterNPCAnimatorController);
        var idleState = new IdleNPCState(characterNPCAnimatorController);
        var walkState = new WalkNPCState(characterNPCAnimatorController, GetComponent<NavMeshAgent>());
        var attackState = new AttackNPCState(characterNPCAnimatorController, transform);

        showWeapon.AddTransition(new StateTransition(idleState,
            new AnimationTransitionCondition(characterNPCAnimatorController.Animator,
                CharacterAnimationType.Show.ToString())));

        idleState.AddTransition(new StateTransition(walkState, new FuncCondition(() => true)));

        walkState.AddTransition(new StateTransition(attackState, new FuncCondition(() => walkState.IsOnPosition)));
        attackState.AddTransition(new StateTransition(walkState,
            new AnimationTransitionConditionWithFunk(() => walkState.IsOnAgrPosition == false,
                characterNPCAnimatorController.Animator, CharacterAnimationType.Attack.ToString())));


        stateMachine = new StateMachine(showWeapon);
    }
}