using System;
using System.Collections.Generic;
using Core.AI.FiniteStateMachines;

namespace Tests.EditMode.Core.AI.FiniteStateMachines
{
    public class FakeState : IState
    {
        public void Enter() {}
        public void Execute() {}
        public void Exit() {}

        private string _id;
        public string Id => _id ??= State.GenerateId();

        private List<ITransition> _transitions;
        public List<ITransition> Transitions => _transitions ??= new List<ITransition>();
    }

    public class SpyState : IState
    {
        private string _id;
        public string Id => _id ??= State.GenerateId();

        public int EnterCallCount;
        public int ExitCallCount;
        public int UpdateCallCount;
        public SpyState(List<ITransition> transitions)
        {
            Transitions = transitions;
        }
        public void Enter() => EnterCallCount++;
        public void Execute() => UpdateCallCount++;
        public void Exit() => ExitCallCount++;

        public List<ITransition> Transitions { get; }
    }

    public class FakeTransition : ITransition
    {
        public bool IsValid() => true;
        public IState NextState() => new FakeState();
        public void OnTransition() {}
    }

    public class SpyTransition : ITransition
    {
        private readonly Func<bool> _isValid;
        private readonly Func<IState> _nextState;
        private readonly Action _onTransition;
        public SpyTransition(Func<bool> isValid, Func<IState> nextState, Action onTransition)
        {
            _isValid = isValid;
            _nextState = nextState;
            _onTransition = onTransition;
        }
        public bool IsValid() => _isValid();
        public IState NextState() => _nextState();
        public void OnTransition() => _onTransition();
    }
}