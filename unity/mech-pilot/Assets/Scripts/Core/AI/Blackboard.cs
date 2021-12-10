using System;
using System.Collections.Generic;

namespace Core.AI
{
    public enum BlackboardOperationStatus
    {
        Success,
        Failure
    }

    public class BlackboardQueryRequest
    {
        public BlackboardQueryRequest(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }

    public class BlackboardQueryResult<T>
    {
        public BlackboardQueryResult(BlackboardOperationStatus status, T data)
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

        public BlackboardQueryResult<object> Read(BlackboardQueryRequest request)
        {
            if (_blackboardData.ContainsKey(request.Key))
                return new BlackboardQueryResult<object>(
                    BlackboardOperationStatus.Success,
                    _blackboardData[request.Key]
                );

            return new BlackboardQueryResult<object>(BlackboardOperationStatus.Failure, null);
        }

        public BlackboardQueryResult<T> Read<T>(BlackboardQueryRequest request)
        {
            if (_blackboardData.ContainsKey(request.Key))
                try
                {
                    return new BlackboardQueryResult<T>(
                        BlackboardOperationStatus.Success,
                        (T) _blackboardData[request.Key]
                    );
                }
                catch (InvalidCastException e)
                {
                    return new BlackboardQueryResult<T>(BlackboardOperationStatus.Failure, default);
                }

            return new BlackboardQueryResult<T>(BlackboardOperationStatus.Failure, default);
        }
    }

}