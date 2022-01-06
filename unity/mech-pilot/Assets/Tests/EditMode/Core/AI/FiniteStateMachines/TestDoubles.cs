using System;
using System.Collections.Generic;
using Core.AI.FiniteStateMachines;

namespace Tests.EditMode.Core.AI.FiniteStateMachines
{
    public class FakeState : IState
    {
        public void Enter() {}
        public void Update() {}
        public void Exit() {}

        public List<ITransition> Transition => new List<ITransition>();
    }

    public class SpyState : IState
    {
        public int EnterCallCount;
        public int ExitCallCount;
        public int UpdateCallCount;
        public SpyState(List<ITransition> transitions) => Transition = transitions;
        public void Enter() => EnterCallCount++;
        public void Update() => UpdateCallCount++;
        public void Exit() => ExitCallCount++;

        public List<ITransition> Transition { get; }
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