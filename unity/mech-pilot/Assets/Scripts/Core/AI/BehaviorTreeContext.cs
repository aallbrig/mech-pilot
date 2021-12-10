using UnityEngine;

namespace Core.AI
{
    public interface IBehaviorTree<T>
    {
        public void Tick(T behaviorTreeContext);
    }

    public abstract class BehaviorTreeContext<T> : MonoBehaviour
    {
        public readonly Blackboard Blackboard = new Blackboard();
        private IBehaviorTree<T> _behaviorTree;
        private T _self;

        // Consumers can overwrite this method if they need anything else special
        protected virtual void Start()
        {
            _self = GetComponent<T>();
            if (_self == null)
            {
                Debug.LogError("Behavior Tree Context implementations must pass in a monobehavior 'context'");
                return;
            }

            RebuildBehaviorTree();
            SetupDefaultBlackboardValues();
        }

        private void SetupDefaultBlackboardValues() => Blackboard.Write("GameObject", gameObject);
        public void RebuildBehaviorTree() => _behaviorTree = BuildBehaviorTree();

        protected virtual void Update() => Blackboard.Write("CurrentPlayTime", Time.time);

        public void Tick() => _behaviorTree.Tick(_self);

        // Extension point for subclasses
        protected abstract IBehaviorTree<T> BuildBehaviorTree();
    }
}