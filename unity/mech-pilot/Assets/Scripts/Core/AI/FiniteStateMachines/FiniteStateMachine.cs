namespace Core.AI.FiniteStateMachines
{
    public interface IFiniteStateMachine
    {
        public IState CurrentState { get; }

        public void Evaluate();
        public void NewState(IState newState);
    }

    public class FiniteStateMachine : IFiniteStateMachine
    {
        public FiniteStateMachine(IState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public IState CurrentState { get; private set; }

        public void Evaluate()
        {
            // Design decision: do I want the transitions to evaluate
            // before the current state gets a chance to execute?
            // For now, current state is okay executing before.
            CurrentState.Execute();

            foreach (var transition in CurrentState.Transitions)
            {
                transition.OnTransition();
                if (!transition.IsValid())
                    continue;
                NewState(transition.NextState());
                return;
            }
        }

        public void NewState(IState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}