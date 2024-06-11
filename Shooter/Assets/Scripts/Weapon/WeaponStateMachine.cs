using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(ShotSystem), typeof(Reloader))]
public class WeaponStateMachine : MonoBehaviour
{
    [SerializeField] private float fireRate;
    private StateMachine _stateMachine;
    private StateMachine _shotMachine;

    public float FireRate => fireRate;

    private bool isReadyToShot => CheckReadyShot();

    private Reloader reloader;
    private WeaponScoopeState scopeState;
    private IdleWeaponState idleState;
    private ReloadWeaponState reloadWeaponState;

    private void Awake()
    {
        reloader = GetComponent<Reloader>();
        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
        _shotMachine.OnUpdate();
        if (Input.GetKeyDown(KeyCode.R) && reloader.IsMagFull() == false)
        {
            StartReload();
        }
    }

    private void FixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
        _shotMachine.OnFixedUpdate();
    }

    private bool CheckReadyShot()
    {
        if (reloader.CheckAmount() == false || _stateMachine.CurrentState == reloadWeaponState)
        {
            return false;
        }

        if (_stateMachine.CurrentState == idleState)
        {
            return true;
        }

        if (_stateMachine.CurrentState == scopeState)
        {
            return true;
        }

        return false;
    }

    private void InitializeStateMachine()
    {
        var animatorController = new WeaponAnimationController(GetComponent<Animator>());
        idleState = new IdleWeaponState(animatorController);
        var showState = new ShowWeaponState(animatorController);
        scopeState = new WeaponScoopeState(animatorController);
        reloadWeaponState = new ReloadWeaponState(animatorController);
        var weaponRunState = new WeaponRunState(animatorController);


        showState.AddTransition(new StateTransition(idleState,
            new AnimationTransitionCondition(animatorController.Animator, WeaponAnimationType.Show.ToString())));

        idleState.AddTransition(new StateTransition(scopeState, new FuncCondition(() => Input.GetMouseButton(1))));
        idleState.AddTransition(new StateTransition(weaponRunState,
            new FuncCondition(() => Input.GetKey(KeyCode.LeftShift))));

        weaponRunState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => Input.GetKey(KeyCode.LeftShift) == false)));
        weaponRunState.AddTransition(new StateTransition(scopeState, new FuncCondition(() => Input.GetMouseButton(1))));

        scopeState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => Input.GetMouseButton(1) == false)));

        reloadWeaponState.AddTransition(new StateTransition(idleState,
            new AnimationTransitionCondition(animatorController.Animator, WeaponAnimationType.Reload.ToString(),1f)));
        _stateMachine = new StateMachine(showState);
        InitializeShotStateMachine(animatorController);
    }

    private void StartReload()
    {
        if (_stateMachine.CurrentState != reloadWeaponState)
        {
            _stateMachine.SetState(reloadWeaponState);
        }
    }

    private void InitializeShotStateMachine(WeaponAnimationController weaponAnimationController)
    {
        var idleState = new State();
        var shotstate = new WeaponShotState(weaponAnimationController);

        idleState.AddTransition(new StateTransition(shotstate,
            new FuncCondition(() => isReadyToShot && Input.GetMouseButton(0))));
        idleState.AddTransition(new StateTransition(idleState, new FuncCondition(() =>
        {
            if (Input.GetMouseButton(0) && reloader.CheckAmount() == false)
            {
                StartReload();
                return true;
            }

            return false;
        })));
        shotstate.AddTransition(new StateTransition(idleState, new TemporaryCondition(fireRate)));

        _shotMachine = new StateMachine(idleState);
    }
}