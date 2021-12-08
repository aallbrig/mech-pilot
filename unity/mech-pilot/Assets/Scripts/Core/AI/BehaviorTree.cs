using UnityEngine;

namespace Core.AI
{
    public enum BehaviorTreeOperationStatus
    {
        Success,
        Failure
    }

    public struct BehaviorTreeOperation
    {
        public BehaviorTreeOperationStatus Status;
    }

    public interface BehaviorTreeService
    {
        public BehaviorTreeOperation Tick(GameObject context);
    }

    public class BehaviorTree : ScriptableObject, BehaviorTreeService
    {
        public BehaviorTreeOperation Tick(GameObject context) => new BehaviorTreeOperation
        {
            Status = BehaviorTreeOperationStatus.Success
        };
    }
}