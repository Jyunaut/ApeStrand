public abstract class State
{
    public virtual void EnterState() {}
    public virtual void DoStateBehaviour() {}
    public virtual void DoStateBehaviourFixedUpdate() {}
    public virtual void ExitState() {}
    public virtual void Transitions() {}
}