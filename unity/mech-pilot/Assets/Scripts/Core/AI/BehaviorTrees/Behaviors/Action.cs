using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;

namespace Core.AI.BehaviorTrees.Behaviors
{
    public class Action : Behavior
    {
        public delegate Status ActionCommand();

        public delegate void ActionSetup();

        public delegate void ActionTeardown();

        private readonly ActionCommand _action;
        private readonly ActionSetup _setup;
        private readonly ActionTeardown _teardown;

        public Action(ActionCommand action, ActionSetup setup = null, ActionTeardown teardown = null)
        {
            _action = action;
            _setup = setup;
            _teardown = teardown;
        }

        public override void Initialize() => _setup?.Invoke();

        public override Status Execute()
        {
            CurrentStatus = _action.Invoke();
            return CurrentStatus;
        }

        public override void Terminate() => _teardown?.Invoke();
    }
}