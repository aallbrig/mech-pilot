namespace Core.AI.FiniteStateMachines
{
    public interface IFiniteStateMachine
    {
        public State CurrentState { get; }
    }

    public class FiniteStateMachine {}
}