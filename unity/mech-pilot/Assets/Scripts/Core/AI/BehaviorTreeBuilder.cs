using UnityEngine;

namespace Core.AI
{
    public interface IBehaviorProvider<B>
    {
        // A provider should provide types of behaviors
        // e.g. action, decorator, sequences, filters/preconditions,
        // selectors,
        public B Root();
    }

    public class NoopBehavior: IBehavior<MonoBehaviour>
    {
        private BehaviorExecutionStatus _status;
        public NoopBehavior(BehaviorExecutionStatus status = BehaviorExecutionStatus.Success) => _status = status;
        
        public BehaviorExecutionStatus Execute(MonoBehaviour context) => _status;
    }

    public class NoopBehaviorProvider : IBehaviorProvider<NoopBehavior>
    {
        public NoopBehavior Root() => new NoopBehavior();
    }

    public class NoopBehaviorTree : IBehaviorTree<MonoBehaviour, NoopBehavior>
    {
        public NoopBehavior RootNode { get; set; }

        public void Tick(MonoBehaviour behaviorTreeContext) {}
    }

    public class BehaviorTreeBuilder<Cxt, B>
    {
        private readonly IBehaviorProvider<B> _behaviorProvider;
        private readonly IBehaviorTree<Cxt, B> _behaviorTree;

        public BehaviorTreeBuilder(IBehaviorProvider<B> behaviorProvider, IBehaviorTree<Cxt, B> behaviorTree)
        {
            _behaviorProvider = behaviorProvider;
            _behaviorTree = behaviorTree;
            if (_behaviorTree.RootNode == null) _behaviorTree.RootNode = _behaviorProvider.Root();
        }

        public IBehaviorTree<Cxt, B> Build() => _behaviorTree;
    }
}