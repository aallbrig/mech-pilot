using System.Collections.Generic;
using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees
{
    public class BehaviorTreeBuilder
    {
        private Behavior _rootNode;
        private Behavior _currentBehavior;

        private void AssignRootIfCurrentNodeExists()
        {
            if (_rootNode == default && _currentBehavior != default)
            {
                _rootNode = _currentBehavior;
            }
        }

        public BehaviorTreeBuilder SelectorStart()
        {
            _currentBehavior = new Selector(new List<Behavior>());
            return this;
        }

        public BehaviorTreeBuilder SelectorEnd()
        {
            AssignRootIfCurrentNodeExists();
            return this;
        }

        public BehaviorTreeBuilder AddChild(Behavior childBehavior)
        {
            if (_currentBehavior is IAddChild acceptsChild)
                acceptsChild.AddChild(childBehavior);
            // What happens if it isn't?

            return this;
        }

        public BehaviorTree Build() =>
            // I choose to allow empty "build" by declaring that this method can return null
            _rootNode == default ? null : new BehaviorTree(_rootNode);
    }
}