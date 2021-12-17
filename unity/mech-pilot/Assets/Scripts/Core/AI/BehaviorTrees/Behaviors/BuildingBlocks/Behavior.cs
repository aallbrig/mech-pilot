namespace Core.AI.BehaviorTrees.Behaviors.BuildingBlocks
{

    public abstract class Behavior
    {
        public enum Status
        {
            Invalid,
            Running,
            Success,
            Failure
        }

        public Status CurrentStatus { get; protected set; }

        public abstract void Initialize();
        public abstract Status Execute();
        public abstract void Terminate();

        public Status Tick()
        {
            if (CurrentStatus != Status.Running) Initialize();
            CurrentStatus = Execute();
            if (CurrentStatus != Status.Running) Terminate();
            return CurrentStatus;
        }
    }
}