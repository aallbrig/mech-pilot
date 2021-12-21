using System;
using Core.AI.BehaviorTrees.BuildingBlocks;

namespace Tests.EditMode.Core.AI.TestDoubles
{
    public class BehaviorSpy : Behavior
    {

        private readonly Func<Status> _desiredExecuteStatus;
        public bool ExecuteMethodCalled;
        public int ExecuteMethodCallCount = 0;
        public bool InitializeMethodCalled;
        public int InitializeMethodCallCount = 0;
        public bool TerminateMethodCalled;
        public int TerminateMethodCallCount = 0;
        public BehaviorSpy(Func<Status> desiredExecuteStatus) => _desiredExecuteStatus = desiredExecuteStatus;

        protected override Status Execute()
        {
            ExecuteMethodCallCount++;
            ExecuteMethodCalled = true;
            CurrentStatus = _desiredExecuteStatus.Invoke();
            return CurrentStatus;
        }

        protected override void Terminate()
        {
            TerminateMethodCallCount++;
            TerminateMethodCalled = true;
        }

        protected override void Initialize()
        {
            InitializeMethodCallCount++;
            InitializeMethodCalled = true;
        }
    }
}