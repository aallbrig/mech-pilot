using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Tests.EditMode.Core.AI.TestDoubles
{
    public class BehaviorSpy : Behavior
    {

        private readonly Status _desiredExecuteStatus;
        public bool ExecuteMethodCalled;

        public bool InitializeMethodCalled;
        public bool TerminateMethodCalled;
        public BehaviorSpy(Status desiredExecuteStatus) => _desiredExecuteStatus = desiredExecuteStatus;

        public override Status Execute()
        {
            ExecuteMethodCalled = true;
            CurrentStatus = _desiredExecuteStatus;
            return CurrentStatus;
        }

        public override void Terminate() => TerminateMethodCalled = true;

        public override void Initialize() => InitializeMethodCalled = true;
    }
}