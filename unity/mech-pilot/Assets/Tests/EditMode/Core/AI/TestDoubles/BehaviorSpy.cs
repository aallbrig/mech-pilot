using Core.AI.BehaviorTrees;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Tests.EditMode.Core.AI.TestDoubles
{
    public class BehaviorSpy : Behavior
    {

        public bool InitializeMethodCalled;
        public bool ExecuteMethodCalled;
        public bool TerminateMethodCalled;

        private readonly Status _desiredExecuteStatus;
        public BehaviorSpy(Status desiredExecuteStatus)
        {
            _desiredExecuteStatus = desiredExecuteStatus;
        }

        public override Status Execute()
        {
            ExecuteMethodCalled = true;
            CurrentStatus = _desiredExecuteStatus;
            return CurrentStatus;
        }

        public override void Terminate()
        {
            TerminateMethodCalled = true;
        }

        public override void Initialize()
        {
            InitializeMethodCalled = true;
        }
    }
}