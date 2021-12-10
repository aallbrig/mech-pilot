using UnityEngine;

namespace Core.AI
{
    public enum BehaviorExecutionStatus
    {
        Running,
        Success,
        Failure
    }

    public interface Behavior<T>
    {
        public abstract BehaviorExecutionStatus Execute(T context);
    }

    public abstract class Behavior : ScriptableObject
    {
        // public Behavior Child { get; set; }

        // public Behavior Sibling { get; set; }

        // public abstract BehaviorExecutionStatus Execute(MissingType context);
    }
}