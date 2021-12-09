using Core.AI;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.Core.AI
{
    public class FakeBehaviorTree: IBehaviorTree
    {
        public void Tick(BehaviorTreeContext btContext) {}
    }

    public class SpyBehaviorTreeContext: BehaviorTreeContext
    {
        protected override IBehaviorTree BuildBehaviorTree() => new FakeBehaviorTree();
    }

    public class BehaviorTester : Behavior
    {
        public BehaviorExecutionStatus desiredReturnStatus;
        public override BehaviorExecutionStatus Execute(BehaviorTreeContext context) => desiredReturnStatus;
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