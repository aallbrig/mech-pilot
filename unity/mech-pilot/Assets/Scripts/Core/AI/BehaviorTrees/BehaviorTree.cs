using System;
using Core.AI.BehaviorTrees.BuildingBlocks;

namespace Core.AI.BehaviorTrees
{
    public class BehaviorTree
    {
        private Behavior _currentNode;
        public BehaviorTree(Behavior rootNode) =>
            // The root node can't be a leaf node?
            // Or is the concept of having the root node be composite very important?
            // For now, I will only care that the root node is a behavior
            RootBehavior = rootNode ?? throw new ArgumentNullException(nameof(rootNode));

        public Behavior RootBehavior { get; }

        public void Tick()
        {
            if (_currentNode == null) _currentNode = RootBehavior;
            _currentNode.Tick();
        }
    }
}