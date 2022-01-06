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

    public abstract class State : IState
    {
        public static string GenerateId() => Guid.NewGuid().ToString();
        private List<ITransition> _transitions;
        private string _id;
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();

        public List<ITransition> Transitions => _transitions ??= new List<ITransition>();

        public string Id => _id ??= GenerateId();
    }
}