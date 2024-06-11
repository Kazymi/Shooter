public class WeaponRunState : State
{
    private readonly WeaponAnimationController _weaponAnimationController;

    public WeaponRunState(WeaponAnimationController weaponAnimationController)
    {
        _weaponAnimationController = weaponAnimationController;
    }

    public override void OnStateEnter()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Run, true);
    }

    public override void OnStateExit()
    {
        _weaponAnimationController.SetBool(WeaponAnimationType.Run, false);
    }
}