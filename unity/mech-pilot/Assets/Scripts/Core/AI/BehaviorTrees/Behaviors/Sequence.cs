using System.Collections.Generic;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class Sequence : Composite
    {
        public Sequence(List<Behavior> children) => Children = children;

        public override Status Execute()
        {
            var child = Children[CurrentIndex];
            var childStatus = child.Tick();

            if (childStatus != Status.Success)
                CurrentStatus = childStatus;
            else
                CurrentStatus = ++CurrentIndex == Children.Count ? Status.Success : Status.Running;

            return CurrentStatus;
        }

        public override void Terminate() {}
    }
}