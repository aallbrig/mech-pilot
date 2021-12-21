using Core.AI.BehaviorTrees.BuildingBlocks;

namespace Tests.EditMode.Core.AI.TestDoubles
{
    public class BehaviorSpy : Behavior
    {

        private readonly Status _desiredExecuteStatus;
        public bool ExecuteMethodCalled;

        public bool InitializeMethodCalled;
        public bool TerminateMethodCalled;
        public BehaviorSpy(Status desiredExecuteStatus) => _desiredExecuteStatus = desiredExecuteStatus;

        protected override Status Execute()
        {
            ExecuteMethodCalled = true;
            CurrentStatus = _desiredExecuteStatus;
            return CurrentStatus;
        }

        protected override void Terminate() => TerminateMethodCalled = true;

        protected override void Initialize() => InitializeMethodCalled = true;
    }
}