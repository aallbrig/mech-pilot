using UnityEngine;

namespace Core.AI
{
    public enum BehaviorExecuteStatus
    {
        Success,
        Failure
    }

    public struct BehaviorOperation
    {
        public BehaviorExecuteStatus Status;
    }

    public class Behavior: ScriptableObject
    {

        public BehaviorOperation Execute(GameObject gameObject)
        {
            return new BehaviorOperation
            {
                Status = BehaviorExecuteStatus.Success
            };
        }
    }
}