using System.Collections;
using Core.AI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.Core.AI
{
    public class FakeBehavior: IBehavior<BehaviorTreeContextTestBehaviour>
    {
        public BehaviorStatus Status { get; private set; }

        public BehaviorStatus Execute(BehaviorTreeContextTestBehaviour context)
        {
            Status = BehaviorStatus.Success;
            return Status;
        }
    }

    public class BehaviorTreeSpy : IBehaviorTree<BehaviorTreeContextTestBehaviour>
    {
        public BehaviorTreeSpy()
        {
            RootNode = new FakeBehavior();
        }
        public BehaviorTreeContextTestBehaviour Context;
        public IBehavior<BehaviorTreeContextTestBehaviour> RootNode { get; set; }

        public void Tick(BehaviorTreeContextTestBehaviour context) => Context = context;
    }

    public class BehaviorTreeContextTestBehaviour : BehaviorTreeContext<BehaviorTreeContextTestBehaviour>
    {
        public readonly BehaviorTreeSpy Spy = new BehaviorTreeSpy();
        protected override IBehaviorTree<BehaviorTreeContextTestBehaviour> BuildBehaviorTree() => Spy;
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
            Assert.IsFalse(sut.Blackboard.AvailableKeys.Contains("CurrentPlayTime"));

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(sut.Blackboard.AvailableKeys.Contains("CurrentPlayTime"));
        }
    }
}