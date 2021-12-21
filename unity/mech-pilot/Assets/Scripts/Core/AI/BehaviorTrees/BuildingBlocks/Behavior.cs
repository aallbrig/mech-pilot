namespace Core.AI.BehaviorTrees.Behaviors.BuildingBlocks
{

    public abstract class Behavior
    {
        // Order matters. Don't have "Running" at the top of the list of statuses
        public enum Status
        {
            Success,
            Failure,
            Running
        }

        public Status CurrentStatus { get; protected set; }

        protected abstract void Initialize();
        protected abstract Status Execute();
        protected abstract void Terminate();

        public Status Tick()
        {
            if (CurrentStatus != Status.Running) Initialize();
            CurrentStatus = Execute();
            if (CurrentStatus != Status.Running) Terminate();
            return CurrentStatus;
        }
    }
}