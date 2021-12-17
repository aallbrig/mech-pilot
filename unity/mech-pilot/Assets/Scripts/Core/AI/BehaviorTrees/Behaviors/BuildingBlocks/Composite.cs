using System.Collections.Generic;

namespace Core.AI.BehaviorTrees.Behaviors.BuildingBlocks
{
    public abstract class Composite : Behavior
    {
        protected List<Behavior> Children;
        protected int CurrentIndex;

        public override void Initialize()
        {
            CurrentIndex = 0;
        }
    }
}