using System;
using UnityEngine;

namespace Core.AI
{
    public interface IBehavior<T>
    {
        public BehaviorStatus Status { get; }

        public abstract BehaviorStatus Execute(T context);
    }

    [Serializable]
    public enum BehaviorStatus
    {
        Clean,
        Running,
        Success,
        Failure
    }

    public abstract class Behavior<T>: IBehavior<T>
    {
        public abstract BehaviorStatus Status { get; }
        public abstract BehaviorStatus Execute(T context);
    }

    public abstract class BehaviorSO<T> : ScriptableObject, IBehavior<T>
    {
        public abstract BehaviorStatus Status { get; set; }

        public abstract BehaviorStatus Execute(T context);
    }
}