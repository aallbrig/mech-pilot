using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Tests.EditMode.Core.AI.TestDoubles
{
    public class BehaviorFake: Behavior
    {
        public override void Initialize() {}
        public override Status Execute() => Behavior.Status.Success;
        public override void Terminate() {}
    }
}