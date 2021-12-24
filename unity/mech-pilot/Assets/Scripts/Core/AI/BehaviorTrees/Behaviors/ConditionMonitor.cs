using System;
using Core.AI.BehaviorTrees.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class ConditionMonitor : Decorator
    {
        private readonly Func<bool> _predicate;
        public ConditionMonitor(Func<bool> predicate, Behavior child) : base(child) =>
            _predicate = predicate;

        protected override Status Execute()
        {
            var status = _predicate.Invoke();
            CurrentStatus = status ? Child.Tick() : Status.Failure;
            return CurrentStatus;
        }
    }
}