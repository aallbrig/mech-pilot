using System;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees
{
    public class BehaviorTree
    {
        private Behavior _rootNode;
        private Behavior _currentNode;
        public BehaviorTree(Behavior rootNode)
        {
            // The root node can't be a leaf node?
            // Or is the concept of having the root node be composite very important?
            // For now, I will only care that the root node is a behavior
            _rootNode = rootNode ?? throw new ArgumentNullException(nameof(rootNode));
        }

        public void Tick()
        {
            if (_currentNode == null) _currentNode = _rootNode;
            _currentNode.Tick();
        }
    }
}