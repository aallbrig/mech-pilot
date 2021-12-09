using System;
using System.Collections.Generic;

namespace Core.AI
{
    public enum BlackboardOperationStatus
    {
        Success,
        Failure
    }

    public class BlackboardQueryOperation<T>
    {
        public BlackboardQueryOperation(BlackboardOperationStatus status, T data)
        {
            Status = status;
            Data = data;
        }

        public BlackboardOperationStatus Status { get; }

        public T Data { get; }
    }

    public class BlackboardCommandOperation
    {

        private BlackboardCommandOperation() {}

        public BlackboardOperationStatus Status { get; private set; }

        public static BlackboardCommandOperation Of(BlackboardOperationStatus status) =>
            new BlackboardCommandOperation
            {
                Status = status
            };
    }

    public class Blackboard
    {

        private readonly Dictionary<string, object> _blackboardData;
        public readonly HashSet<string> AvailableKeys;
        public Blackboard()
        {
            _blackboardData = new Dictionary<string, object>();
            AvailableKeys = new HashSet<string>();
        }

        public BlackboardCommandOperation Write(string blackboardKey, object blackboardValue)
        {
            AvailableKeys.Add(blackboardKey);
            _blackboardData[blackboardKey] = blackboardValue;

            return BlackboardCommandOperation.Of(BlackboardOperationStatus.Success);
        }

        public BlackboardCommandOperation Remove(string blackboardKey)
        {
            if (_blackboardData.ContainsKey(blackboardKey))
            {
                AvailableKeys.Remove(blackboardKey);
                _blackboardData.Remove(blackboardKey);
                return BlackboardCommandOperation.Of(BlackboardOperationStatus.Success);
            }

            return BlackboardCommandOperation.Of(BlackboardOperationStatus.Failure);
        }

        public BlackboardQueryOperation<object> Read(string blackboardKey)
        {
            if (_blackboardData.ContainsKey(blackboardKey))
                return new BlackboardQueryOperation<object>(BlackboardOperationStatus.Success,
                    _blackboardData[blackboardKey]);

            return new BlackboardQueryOperation<object>(BlackboardOperationStatus.Failure, null);
        }

        public BlackboardQueryOperation<T> Read<T>(string blackboardKey)
        {
            if (_blackboardData.ContainsKey(blackboardKey))
                try
                {
                    return new BlackboardQueryOperation<T>(BlackboardOperationStatus.Success,
                        (T) _blackboardData[blackboardKey]);
                }
                catch (InvalidCastException e)
                {
                    return new BlackboardQueryOperation<T>(BlackboardOperationStatus.Failure, default);
                }

            return new BlackboardQueryOperation<T>(BlackboardOperationStatus.Failure, default);
        }
    }

}