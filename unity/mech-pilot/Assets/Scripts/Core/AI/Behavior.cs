using UnityEngine;

namespace Core.AI
{
    public enum BehaviorExecutionStatus
    {
        Success,
        InProgress,
        Failure
    }

    public abstract class Behavior: ScriptableObject
    {
        public abstract BehaviorExecutionStatus Execute(BehaviorTreeData context);

        public Behavior Child { get; set; }
        public Behavior Sibling { get; set; }
    }
}