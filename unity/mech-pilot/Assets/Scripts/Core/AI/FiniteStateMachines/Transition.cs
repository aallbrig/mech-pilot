namespace Core.AI.FiniteStateMachines
{
    public interface ITransition
    {
        public bool IsValid();
        public IState NextState();
        public void OnTransition();
    }

    public abstract class Transition : ITransition
    {
        public abstract bool IsValid();
        public abstract IState NextState();
        public abstract void OnTransition();
    }
}