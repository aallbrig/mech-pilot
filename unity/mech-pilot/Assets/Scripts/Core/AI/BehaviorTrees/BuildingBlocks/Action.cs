using System;

namespace Core.AI.BehaviorTrees.BuildingBlocks
{
    public class Action : Behavior
    {
        private readonly Func<Status> _action;
        private readonly System.Action _setup;
        private readonly System.Action _teardown;
        public Action(Func<Status> action, System.Action setup = null, System.Action teardown = null)
        {
            _action = action;
            _setup = setup;
            _teardown = teardown;
        }

        protected override void Initialize() => _setup?.Invoke();

        protected override Status Execute()
        {
            CurrentStatus = _action.Invoke();
            return CurrentStatus;
        }

        protected override void Terminate() => _teardown?.Invoke();
    }
}