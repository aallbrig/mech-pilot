using System;
using Core.AI.BehaviorTrees.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class ConditionInstant : Behavior
    {
        private readonly Func<bool> _predicate;

        public ConditionInstant(Func<bool> predicate) =>
            _predicate = predicate;

        protected override Status Execute()
        {
            var conditionCheck = _predicate.Invoke();
            CurrentStatus = conditionCheck ? Status.Success : Status.Failure;
            return CurrentStatus;
        }
    }
}