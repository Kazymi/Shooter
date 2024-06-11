public class WeaponShotState : State
{
    private readonly WeaponAnimationController _weaponAnimationController;

    public WeaponShotState(WeaponAnimationController weaponAnimationController)
    {
        _weaponAnimationController = weaponAnimationController;
    }

    public override void OnStateEnter()
    {
        _weaponAnimationController.SetTrigger(WeaponAnimationType.Shot);
    }
}