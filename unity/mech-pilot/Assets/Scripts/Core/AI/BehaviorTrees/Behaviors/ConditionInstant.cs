using System;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class ConditionInstant : Decorator
    {
        private readonly Func<bool> _predicate;

        public ConditionInstant(Behavior child, Func<bool> predicate)
        {
            Child = child;
            _predicate = predicate;
        }

        public override void Initialize() {}
        public override Status Execute()
        {
            var result = _predicate.Invoke();
            CurrentStatus = result ? Child.Tick() : Status.Failure;

            return CurrentStatus;
        }
        public override void Terminate() {}
    }
}