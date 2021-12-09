using System.Collections.Generic;
using UnityEngine.AI;

namespace Core.AI
{
    public enum BlackboardOperationStatus
    {
        Success,
        Failure
    }

    public class BlackboardQueryOperation
    {
        public static BlackboardQueryOperation Of(BlackboardOperationStatus status, object data)
        {
            return new BlackboardQueryOperation
            {
                Status = status, Data = data
            };
        }

        private BlackboardQueryOperation() {}
        public BlackboardOperationStatus Status { get; private set; }
        public object Data { get; private set; }
    }

    public class BlackboardCommandOperation
    {
        public static BlackboardCommandOperation Of(BlackboardOperationStatus status) =>
            new BlackboardCommandOperation
            {
                Status = status
            };

        private BlackboardCommandOperation() {}

        public BlackboardOperationStatus Status { get; private set; }
    }

    public class Blackboard
    {
        public Blackboard()
        {
            _blackboardData = new Dictionary<string, object>();
            AvailableKeys = new HashSet<string>();
        }

        private readonly Dictionary<string, object> _blackboardData;
        public readonly HashSet<string> AvailableKeys;

        public BlackboardCommandOperation Write(string blackboardKey, object blackboardValue)
        {
            AvailableKeys.Add(blackboardKey);
            _blackboardData[blackboardKey] = blackboardValue;

            return BlackboardCommandOperation.Of(BlackboardOperationStatus.Success);
        }

        public BlackboardQueryOperation Read(string blackboardKey)
        {
            if (_blackboardData.ContainsKey(blackboardKey))
                return BlackboardQueryOperation.Of(BlackboardOperationStatus.Success, _blackboardData[blackboardKey]);

            return BlackboardQueryOperation.Of(BlackboardOperationStatus.Failure, null);
        }
    }

}