using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(ShotSystem), typeof(Reloader))]
public class WeaponStateMachine : MonoBehaviour
{
    [SerializeField] private string gradadeStat;
    [SerializeField] private Transform granadeSpawnPosition;
    [SerializeField] private Granade prefab;
    [SerializeField] private float fireRate;
    private StateMachine _stateMachine;
    private StateMachine _shotMachine;
    private StateMachine _granadeMachine;

    public float FireRate => fireRate;

    private bool isReadyToShot => CheckReadyShot();

    private Reloader reloader;
    private WeaponScoopeState scopeState;
    private IdleWeaponState idleState;
    private ReloadWeaponState reloadWeaponState;

    private IPool<Granade> granadePool;

    private void Awake()
    {
        var factory = new FactoryMonoObject<Granade>(prefab.gameObject, transform);
        granadePool = new Pool<Granade>(factory, 1);
        reloader = GetComponent<Reloader>();
        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
        _shotMachine.OnUpdate();
        _granadeMachine.OnUpdate();
        gradadeStat = _granadeMachine.CurrentState.ToString();
        if (Input.GetKeyDown(KeyCode.R) && reloader.IsMagFull() == false)
        {
            StartReload();
        }
    }

    private void FixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
        _shotMachine.OnFixedUpdate();
        _granadeMachine.OnFixedUpdate();
    }

    public void SpawnGrenade()
    {
        var grenada = granadePool.Pull();
        grenada.transform.position = granadeSpawnPosition.position;
        grenada.transform.rotation = granadeSpawnPosition.rotation;
        grenada.AddVelocity();
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
            new AnimationTransitionCondition(animatorController.Animator, WeaponAnimationType.Reload.ToString(), 1f)));
        _stateMachine = new StateMachine(showState);
        InitializeShotStateMachine(animatorController);
        InitializeGranadeStateMachine(animatorController);
    }

    private void StartReload()
    {
        if (_stateMachine.CurrentState != reloadWeaponState)
        {
            _stateMachine.SetState(reloadWeaponState);
        }
    }

    private void InitializeGranadeStateMachine(WeaponAnimationController weaponAnimationController)
    {
        var idleState = new WeaponIdleGranadeState(weaponAnimationController);
        var granadeState = new WeaponGranadeState(weaponAnimationController);

        idleState.AddTransition(new StateTransition(granadeState,
            new FuncCondition(() => Input.GetKeyDown(KeyCode.G) && _stateMachine.CurrentState != reloadWeaponState)));
        granadeState.AddTransition(new StateTransition(idleState,
            new AnimationTransitionCondition(weaponAnimationController.Animator, "Granade",
                0.9f, 2)));

        _granadeMachine = new StateMachine(idleState);
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

public class WeaponGranadeState : State
{
    private readonly WeaponAnimationController _weaponAnimationController;

    public WeaponGranadeState(WeaponAnimationController weaponAnimationController)
    {
        _weaponAnimationController = weaponAnimationController;
    }

    public override void OnStateEnter()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Granade, true);
    }

    public override void OnStateExit()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Granade, false);
    }
}

public class WeaponIdleGranadeState : State
{
    private readonly WeaponAnimationController _weaponAnimationController;

    public WeaponIdleGranadeState(WeaponAnimationController weaponAnimationController)
    {
        _weaponAnimationController = weaponAnimationController;
    }

    public override void OnStateEnter()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Idlegrenade, true);
    }

    public override void OnStateExit()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Idlegrenade, false);
    }
}