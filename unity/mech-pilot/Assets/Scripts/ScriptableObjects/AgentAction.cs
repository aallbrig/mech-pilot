using Core.AI;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new agent action", menuName = "BehaviorTrees/AgentAction", order = 0)]
    public abstract class AgentAction : AgentBehavior {}
}