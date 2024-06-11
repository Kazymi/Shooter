public class ShowWeaponNPCState : State
{
    private readonly CharacterAnimationController _characterAnimationController;

    public ShowWeaponNPCState(CharacterAnimationController characterAnimationController)
    {
        _characterAnimationController = characterAnimationController;
    }

    public override void OnStateEnter()
    {
        _characterAnimationController.SetTrigger(CharacterAnimationType.Show);
    }
}