public abstract class ACondition : ICondition
{
    public abstract bool IsConditionSuccess();

    public virtual void OnStateEntered()
    {
    }

    public virtual void OnStateExited()
    {
    }

    public virtual void OnTick(float deltaTime)
    {
    }
}