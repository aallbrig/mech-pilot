using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI
{
    public enum BlackboardOperationStatus
    {
        Success,
        Failure
    }

    public struct BlackboardCommandResult
    {
        public BlackboardOperationStatus Status;
        public object Data;
    }

    // DTO from monobehavior context to interface implementer
    public struct BehaviorTreeData
    {
        public float CurrentPlayTime;
        public GameObject GameObject;
    }

    public interface IBehaviorTree
    {
        public void Tick(BehaviorTreeData context);
    }

    public abstract class BehaviorTreeContext : MonoBehaviour
    {
        public IBehaviorTree behaviorTree;
        private IBehaviorTree _behaviorTree;
        private readonly Dictionary<string, object> _blackboardData = new Dictionary<string, object>();

        public BlackboardCommandResult BlackboardWrite(string blackboardKey, object blackboardValue)
        {
            _blackboardData[blackboardKey] = blackboardValue;

            return new BlackboardCommandResult
            {
                Status = BlackboardOperationStatus.Success,
                Data = blackboardValue
            };
        }

        public BlackboardCommandResult BlackboardRead(string blackboardKey)
        {
            if (_blackboardData.ContainsKey(blackboardKey))
            {
                return new BlackboardCommandResult
                {
                    Status = BlackboardOperationStatus.Success,
                    Data = _blackboardData[blackboardKey]
                };
            }

            return new BlackboardCommandResult
            {
                Status = BlackboardOperationStatus.Failure
            };
        }

        public void Tick(float currentPlayTime)
        {
            _behaviorTree.Tick(new BehaviorTreeData { CurrentPlayTime = currentPlayTime, GameObject = gameObject });
        }

        private void Start()
        {
            if (behaviorTree != null) _behaviorTree = behaviorTree;
            else Debug.LogError("Behavior tree is required for context to work");
        }
    }
}