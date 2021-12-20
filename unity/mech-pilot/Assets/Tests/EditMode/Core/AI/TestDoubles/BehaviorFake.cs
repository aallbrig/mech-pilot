using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Tests.EditMode.Core.AI.TestDoubles
{
    public class BehaviorFake: Behavior
    {
        protected override void Initialize() {}
        protected override Status Execute() => Behavior.Status.Success;
        protected override void Terminate() {}
    }
}