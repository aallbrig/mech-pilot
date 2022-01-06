using System;
using System.Collections.Generic;

namespace Core.AI.FiniteStateMachines
{
    public interface IState
    {
        public string Id { get; }

        public List<ITransition> Transitions { get; }

        public void Enter();
        public void Execute();
        public void Exit();
    }

    public class State : IState
    {
        private string _id;
        private List<ITransition> _transitions;
        private readonly Action _onEnter;
        private readonly Action _onExecute;
        private readonly Action _onExit;

        public State(Action onEnter, Action onExecute, Action onExit)
        {
            _id = GenerateId();
            _onEnter = onEnter;
            _onExecute = onExecute;
            _onExit = onExit;
        }
        public void Enter() => _onEnter();
        public void Execute() => _onExecute();
        public void Exit() => _onExit();

        public List<ITransition> Transitions => _transitions ??= new List<ITransition>();

        public string Id => _id ??= GenerateId();

        public static string GenerateId() => Guid.NewGuid().ToString();
    }
}