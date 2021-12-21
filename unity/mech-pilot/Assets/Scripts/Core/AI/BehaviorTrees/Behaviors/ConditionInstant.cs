using System;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class ConditionInstant : Behavior
    {
        private readonly Func<bool> _predicate;

        public ConditionInstant(Func<bool> predicate) =>
            _predicate = predicate;

        protected override void Initialize() {}
        protected override Status Execute()
        {
            var result = _predicate.Invoke();
            CurrentStatus = result ? Status.Success : Status.Failure;

            return CurrentStatus;
        }
        protected override void Terminate() {}
    }
}