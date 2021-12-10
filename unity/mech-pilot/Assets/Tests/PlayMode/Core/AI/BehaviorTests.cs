using Core.AI;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.Core.AI
{
    public class FakeBehaviorTree : IBehaviorTree<SpyBehaviorTreeContext>
    {
        public void Tick(SpyBehaviorTreeContext behaviorTreeContext) {}
    }

    public class SpyBehaviorTreeContext : BehaviorTreeContext<SpyBehaviorTreeContext>
    {
        protected override IBehaviorTree<SpyBehaviorTreeContext> BuildBehaviorTree() => new FakeBehaviorTree();
    }

    public class BehaviorTester : Behavior
    {
        public BehaviorExecutionStatus desiredReturnStatus;
        public BehaviorExecutionStatus Execute(SpyBehaviorTreeContext context) => desiredReturnStatus;
    }

    public class BehaviorTests
    {
        [Test]
        public void Behavior_CanBeRun()
        {
            var spy = new GameObject().AddComponent<SpyBehaviorTreeContext>();
            var sut = ScriptableObject.CreateInstance<BehaviorTester>();
            sut.desiredReturnStatus = BehaviorExecutionStatus.Success;

            var result = sut.Execute(spy);

            Assert.AreEqual(BehaviorExecutionStatus.Success, result);
        }
    }
}