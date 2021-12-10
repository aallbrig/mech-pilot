using UnityEngine;

namespace Core.AI
{
    public interface IBehaviorProvider
    {
        // A provider should provide types of behaviors
        // e.g. action, decorator, sequences, filters/preconditions,
        // selectors,
    }

    public class BehaviorTreeBuilder
    {
        private BehaviorTree _behaviorTree;
        public BehaviorTreeBuilder(IBehaviorProvider behaviorProvider)
        {
            _behaviorTree = ScriptableObject.CreateInstance<BehaviorTree>();
        }

        public BehaviorTreeBuilder() {}
        public BehaviorTree Build()
        {
            return _behaviorTree;
        }
    }
}