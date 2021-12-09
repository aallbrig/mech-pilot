using UnityEngine;

namespace Core.AI
{
    public enum BehaviorExecutionStatus
    {
        Running,
        Success,
        Failure
    }

    public abstract class Behavior: ScriptableObject
    {
        public abstract BehaviorExecutionStatus Execute(BehaviorTreeContext context);

        public Behavior Child { get; set; }
        public Behavior Sibling { get; set; }
    }
}