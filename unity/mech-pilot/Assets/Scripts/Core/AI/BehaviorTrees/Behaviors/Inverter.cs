using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class Inverter : Decorator
    {
        public Inverter(Behavior child) : base(child) {}

        protected override void Initialize() {}
        protected override Status Execute()
        {
            var childStatus = Child.Tick();
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
        protected override void Terminate() {}
    }
}