using UnityEngine;

namespace Core.AI
{
    public interface IBehaviorTree
    {
        public void Tick(BehaviorTreeContext btContext);
    }

    public abstract class BehaviorTreeContext : MonoBehaviour
    {
        public readonly Blackboard Blackboard = new Blackboard();
        private BehaviorTreeContext _self;
        private IBehaviorTree _behaviorTree;

        public void Tick() => _behaviorTree.Tick(_self);

        protected abstract IBehaviorTree BuildBehaviorTree();

        protected virtual void Start()
        {
            _self = GetComponent<BehaviorTreeContext>();
            _behaviorTree = BuildBehaviorTree();

            Blackboard.Write("GameObject", gameObject);
        }

        protected virtual void Update() => Blackboard.Write("CurrentPlayTime", Time.time);
    }
}