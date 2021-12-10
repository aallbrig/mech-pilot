using UnityEngine;

namespace Core.AI
{
    public interface IBehaviorProvider<TContext>
    {
        // A provider should provide types of behaviors
        // e.g. action, decorator, sequences, filters/preconditions,
        // selectors,
        public IBehavior<TContext> Root();
    }

    public class NoopBehavior: IBehavior<MonoBehaviour>
    {
        private BehaviorStatus _status;
        
        public NoopBehavior(BehaviorStatus status = BehaviorStatus.Success) => _status = status;

        public BehaviorStatus Status { get; private set; }

        public BehaviorStatus Execute(MonoBehaviour context)
        {
            Status = _status;
            return Status;
        }
    }

    public class NoopBehaviorProvider : IBehaviorProvider<MonoBehaviour>
    {
        public IBehavior<MonoBehaviour> Root() => new NoopBehavior();
    }

    public class NoopBehaviorTree : IBehaviorTree<MonoBehaviour>
    {
        public IBehavior<MonoBehaviour> RootNode { get; set; }

        public void Tick(MonoBehaviour context) {}
    }

    public class BehaviorTreeBuilder<TContext>
    {
        private readonly IBehaviorProvider<TContext> _behaviorProvider;
        private readonly IBehaviorTree<TContext> _behaviorTree;

        public BehaviorTreeBuilder(IBehaviorProvider<TContext> behaviorProvider, IBehaviorTree<TContext> behaviorTree)
        {
            _behaviorProvider = behaviorProvider;
            _behaviorTree = behaviorTree;

            _behaviorTree.RootNode ??= _behaviorProvider.Root();
        }

        public IBehaviorTree<TContext> Build() => _behaviorTree;
    }
}