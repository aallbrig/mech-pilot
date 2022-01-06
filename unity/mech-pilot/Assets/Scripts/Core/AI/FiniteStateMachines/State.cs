using System.Collections.Generic;

namespace Core.AI.FiniteStateMachines
{
    public interface IState
    {
        public List<ITransition> Transition { get; }

        public void Enter();
        public void Update();
        public void Exit();
    }

    public abstract class State : IState
    {

        private List<ITransition> _transitions;
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();

        public List<ITransition> Transition => _transitions ??= new List<ITransition>();
    }
}