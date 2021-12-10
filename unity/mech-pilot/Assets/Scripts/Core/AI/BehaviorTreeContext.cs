using UnityEngine;

namespace Core.AI
{
    public interface IBehaviorTree<TContext>
    {
        public IBehavior<TContext> RootNode { get; set; }

        public void Tick(TContext context);
    }

    public abstract class BehaviorTreeContext<TContext> : MonoBehaviour
    {
        public readonly Blackboard Blackboard = new Blackboard();
        private IBehaviorTree<TContext> _behaviorTree;
        private TContext _self;

        // Consumers can overwrite this method if they need anything else special
        protected virtual void Awake()
        {
            _self = GetComponent<TContext>();
            if (_self == null)
            {
                Debug.LogError("Behavior Tree Context implementations must pass in a monobehavior 'context'");
                return;
            }

            RebuildBehaviorTree();
            SetupDefaultBlackboardValues();

            OnStart();
        }

        private void SetupDefaultBlackboardValues() => Blackboard.Write("GameObject", gameObject);
        public void RebuildBehaviorTree() => _behaviorTree = BuildBehaviorTree();

        protected virtual void Update()
        {
            Blackboard.Write("CurrentPlayTime", Time.time);
            OnUpdate();
        }

        public void Tick() => _behaviorTree.Tick(_self);

        // Extension point for subclasses
        protected abstract IBehaviorTree<TContext> BuildBehaviorTree();
        protected virtual void OnStart() {}
        protected virtual void OnUpdate() {}
    }
}
