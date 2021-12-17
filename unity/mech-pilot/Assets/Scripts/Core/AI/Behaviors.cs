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

    // Should the core package define the scriptable object,
    // or is a scriptable object just a concrete impl of the abstract interface?
    public abstract class BehaviorSO<T> : ScriptableObject, IBehavior<T>
    {
        public abstract BehaviorStatus Status { get; set; }

        public abstract BehaviorStatus Execute(T context);
    }
}