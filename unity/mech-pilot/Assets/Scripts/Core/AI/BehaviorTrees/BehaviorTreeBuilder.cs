using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees
{
    public class BehaviorTreeBuilder
    {
        private Behavior _rootNode;

        public BehaviorTree Build()
        {
            // I choose to allow empty "build" by declaring that this method can return null
            return _rootNode == default ? null : new BehaviorTree(_rootNode);
        }
    }
}