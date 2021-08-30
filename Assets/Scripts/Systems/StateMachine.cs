public abstract class StateMachine
{
    public virtual void EnterState() {}
    public virtual void Update() {}
    public virtual void FixedUpdate() {}
    public virtual void ExitState() {}
    public virtual void Transitions() {}
}