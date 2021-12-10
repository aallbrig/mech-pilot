using Controllers;
using Core.AI;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class AgentBehavior : ScriptableObject, IBehavior<AgentController>
    {
        public abstract BehaviorExecutionStatus Execute(AgentController context);
    }
}