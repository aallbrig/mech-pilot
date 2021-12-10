using Controllers;
using Core.AI;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New agent behavior tree", menuName = "BehaviorTrees/AgentBT", order = 0)]
    public class AgentBehaviorTree : ScriptableObject, IBehaviorTree<AgentController, AgentBehavior>
    {
        public AgentBehavior rootNode;
        public AgentBehavior RootNode { get; set; }

        private AgentBehavior currentNode;

        public void Tick(AgentController behaviorTreeContext)
        {
            if (currentNode == null) currentNode = RootNode;
        }

        private void OnEnable() => RootNode = rootNode;
        private void OnValidate() => RootNode = rootNode;
    }
}