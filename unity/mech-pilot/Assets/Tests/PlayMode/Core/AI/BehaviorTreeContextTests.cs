using System.Collections;
using Core.AI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.Core.AI
{
    public class FakeBehavior {}

    public class BehaviorTreeSpy : IBehaviorTree<BehaviorTreeContextTestBehaviour, FakeBehavior>
    {
        public BehaviorTreeContextTestBehaviour Context;
        public FakeBehavior RootNode { get; set; }
        public void Tick(BehaviorTreeContextTestBehaviour behaviorTreeContext) => Context = behaviorTreeContext;
    }

    public class BehaviorTreeContextTestBehaviour : BehaviorTreeContext<BehaviorTreeContextTestBehaviour, FakeBehavior>
    {
        public readonly BehaviorTreeSpy Spy = new BehaviorTreeSpy();
        protected override IBehaviorTree<BehaviorTreeContextTestBehaviour, FakeBehavior> BuildBehaviorTree() => Spy;
    }

    public class BehaviorTreeContextTests
    {
        [UnityTest]
        public IEnumerator BehaviorTreeContext_ContextIsPassedToBehaviorTree()
        {
            var sut = new GameObject().AddComponent<BehaviorTreeContextTestBehaviour>();
            yield return null;

            sut.Tick();

            Assert.NotNull(sut.Spy.Context);
            Assert.AreSame(sut, sut.Spy.Context);
        }

        [UnityTest]
        public IEnumerator BehaviorTreeContext_OffersBlackboardWithBasicData()
        {
            var sut = new GameObject().AddComponent<BehaviorTreeContextTestBehaviour>();

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(sut.Blackboard.AvailableKeys.Contains("GameObject"));
        }

        [UnityTest]
        public IEnumerator BehaviorTreeContext_BlackboardIsUpdatedWithCurrentPlaytimeByDefault()
        {
            var sut = new GameObject().AddComponent<BehaviorTreeContextTestBehaviour>();

            yield return new WaitForEndOfFrame();
            // The frame of initialization does not run the "Update" behavior
            Assert.IsFalse(sut.Blackboard.AvailableKeys.Contains("CurrentPlayTime"));
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(sut.Blackboard.AvailableKeys.Contains("CurrentPlayTime"));
        }
    }
}