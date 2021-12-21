using Core.AI.BehaviorTrees.BuildingBlocks;

namespace Tests.EditMode.Core.AI.TestDoubles
{
    public class BehaviorFake : Behavior
    {
        protected override void Initialize() {}
        protected override Status Execute() => Status.Success;
        protected override void Terminate() {}
    }
}