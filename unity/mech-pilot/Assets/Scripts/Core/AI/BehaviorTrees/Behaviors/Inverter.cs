using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class Inverter : Decorator
    {
        private readonly Behavior _child;
        public Inverter(Behavior child) => _child = child;

        public override void Initialize() {}
        public override Status Execute()
        {
            var childStatus = _child.Tick();
            switch (childStatus)
            {
                case Status.Success:
                    CurrentStatus = Status.Failure;
                    break;
                case Status.Failure:
                    CurrentStatus = Status.Success;
                    break;
                default:
                    CurrentStatus = childStatus;
                    break;
            }
            return CurrentStatus;
        }
        public override void Terminate() {}
    }
}