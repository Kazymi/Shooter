public class WeaponScoopeState : State
{
    private readonly WeaponAnimationController _weaponAnimationController;

    public WeaponScoopeState(WeaponAnimationController weaponAnimationController)
    {
        _weaponAnimationController = weaponAnimationController;
    }

    public override void OnStateEnter()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Scope, true);
    }

    public override void OnStateExit()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Scope, false);
    }
}