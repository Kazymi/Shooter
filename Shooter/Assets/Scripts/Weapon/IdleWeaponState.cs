public class IdleWeaponState : State
{
    private readonly WeaponAnimationController _weaponAnimationController;

    public IdleWeaponState(WeaponAnimationController weaponAnimationController)
    {
        _weaponAnimationController = weaponAnimationController;
    }

    public override void OnStateEnter()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Idle, true);
    }

    public override void OnStateExit()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Idle, false);
    }
}

public class ReloadWeaponState : State
{
    private readonly WeaponAnimationController _weaponAnimationController;

    public ReloadWeaponState(WeaponAnimationController weaponAnimationController)
    {
        _weaponAnimationController = weaponAnimationController;
    }

    public override void OnStateEnter()
    {
        _weaponAnimationController.SetTrigger(WeaponAnimationType.Reload);
    }
}