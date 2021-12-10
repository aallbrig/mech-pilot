using System;
using System.Collections.Generic;

namespace Core.AI
{
    public enum BlackboardOperationStatus
    {
        Success,
        FailureTypecast,
        FailureKeyNotFound
    }

    public class BlackboardQueryRequest
    {
        public BlackboardQueryRequest(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }

    public class BlackboardQueryResult<TData>
    {
        public BlackboardQueryResult(BlackboardOperationStatus status, TData data)
        {
            Status = status;
            Data = data;
        }

        public BlackboardOperationStatus Status { get; }

        public TData Data { get; }
    }

    public class BlackboardCommandResult
    {
        private BlackboardCommandResult() {}

        public BlackboardOperationStatus Status { get; private set; }

        public static BlackboardCommandResult Of(BlackboardOperationStatus status) =>
            new BlackboardCommandResult
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

        public BlackboardCommandResult Write(string blackboardKey, object blackboardValue)
        {
            AvailableKeys.Add(blackboardKey);
            _blackboardData[blackboardKey] = blackboardValue;

            return BlackboardCommandResult.Of(BlackboardOperationStatus.Success);
        }

        public BlackboardCommandResult Remove(string blackboardKey)
        {
            if (_blackboardData.ContainsKey(blackboardKey))
            {
                AvailableKeys.Remove(blackboardKey);
                _blackboardData.Remove(blackboardKey);
                return BlackboardCommandResult.Of(BlackboardOperationStatus.Success);
            }

            return BlackboardCommandResult.Of(BlackboardOperationStatus.FailureKeyNotFound);
        }

        public BlackboardQueryResult<object> Read(BlackboardQueryRequest request)
        {
            if (_blackboardData.ContainsKey(request.Key))
                return new BlackboardQueryResult<object>(
                    BlackboardOperationStatus.Success,
                    _blackboardData[request.Key]
                );

            return new BlackboardQueryResult<object>(BlackboardOperationStatus.FailureKeyNotFound, null);
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
                catch
                {
                    return new BlackboardQueryResult<T>(BlackboardOperationStatus.FailureTypecast, default);
                }

            return new BlackboardQueryResult<T>(BlackboardOperationStatus.FailureKeyNotFound, default);
        }
    }
}