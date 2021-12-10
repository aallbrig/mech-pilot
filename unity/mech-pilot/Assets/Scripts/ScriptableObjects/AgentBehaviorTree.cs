using Controllers;
using Core.AI;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New agent behavior tree", menuName = "BehaviorTrees/AgentBT", order = 0)]
    public class AgentBehaviorTree : ScriptableObject, IBehaviorTree<AgentController>
    {
        public AgentBehavior rootNode;
        public IBehavior<AgentController> RootNode { get; set; }

        private AgentBehavior _currentNode;

        public void Tick(AgentController context)
        {
            if (_currentNode == null) _currentNode = (AgentBehavior) RootNode;
            var status = _currentNode.Execute(context);
            Debug.Log($"Status: {status}");
        }

        private void OnEnable() => RootNode = rootNode;
        private void OnValidate() => RootNode = rootNode;
    }
}