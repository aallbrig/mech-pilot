using System.Collections.Generic;

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

    public class Blackboard
    {
        private readonly Dictionary<string, object> _blackboardData = new Dictionary<string, object>();

        public BlackboardCommandResult Write(string blackboardKey, object blackboardValue)
        {
            _blackboardData[blackboardKey] = blackboardValue;

            return new BlackboardCommandResult
            {
                Status = BlackboardOperationStatus.Success,
                Data = blackboardValue
            };
        }

        public BlackboardCommandResult Read(string blackboardKey)
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

    }

}