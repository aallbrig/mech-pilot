using System.Collections.Generic;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class Selector : Composite
    {
        public Selector(List<Behavior> children) => Children = children;

        public override Status Execute()
        {
            var currentChild = Children[CurrentIndex];
            var childStatus = currentChild.Tick();

            if (childStatus != Status.Failure)
                CurrentStatus = childStatus;
            else
                CurrentStatus = ++CurrentIndex == Children.Count ? Status.Failure : Status.Running;

            return CurrentStatus;
        }

        public override void Terminate() {}
    }
}