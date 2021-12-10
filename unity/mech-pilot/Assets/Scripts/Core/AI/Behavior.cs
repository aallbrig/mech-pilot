using System;

namespace Core.AI
{
    public interface IBehavior<T>
    {
        public abstract BehaviorExecutionStatus Execute(T context);
    }

    [Serializable]
    public enum BehaviorExecutionStatus
    {
        Clean,
        Running,
        Success,
        Failure
    }
}