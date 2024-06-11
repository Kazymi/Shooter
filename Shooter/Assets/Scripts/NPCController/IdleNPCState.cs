public class IdleNPCState : State
{
    private readonly CharacterAnimationController _characterAnimationController;

    public IdleNPCState(CharacterAnimationController characterAnimationController)
    {
        _characterAnimationController = characterAnimationController;
    }

    public override void OnStateEnter()
    {
        _characterAnimationController.SetBool(CharacterAnimationType.Idle, true);
    }

    public override void OnStateExit()
    {
        _characterAnimationController.SetBool(CharacterAnimationType.Idle, false);
    }
}