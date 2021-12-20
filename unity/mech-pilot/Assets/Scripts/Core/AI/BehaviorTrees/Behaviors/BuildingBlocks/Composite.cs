using System.Collections.Generic;

namespace Core.AI.BehaviorTrees.Behaviors.BuildingBlocks
{
    public abstract class Composite : Behavior, IAddChild
    {
        protected readonly List<Behavior> Children;
        protected int CurrentIndex;

        protected Composite(List<Behavior> children)
        {
            Children = children ?? new List<Behavior>();
        }

        protected override void Initialize() => CurrentIndex = 0;
        public void AddChild(Behavior childBehavior) => Children.Add(childBehavior);
        public int ChildrenCount() => Children.Count;
    }
}