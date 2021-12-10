using System.Collections;
using Core.AI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.Core.AI
{
    public class FakeBehavior: IBehavior<BehaviorTreeContextMbTestBehaviour>
    {
        public BehaviorStatus Status { get; private set; }

        public BehaviorStatus Execute(BehaviorTreeContextMbTestBehaviour contextMb)
        {
            Status = BehaviorStatus.Success;
            return Status;
        }
    }

    public class BehaviorTreeSpy : IBehaviorTree<BehaviorTreeContextMbTestBehaviour>
    {
        public BehaviorTreeSpy()
        {
            RootNode = new FakeBehavior();
        }
        public BehaviorTreeContextMbTestBehaviour ContextMb;
        public IBehavior<BehaviorTreeContextMbTestBehaviour> RootNode { get; set; }

        public void Tick(BehaviorTreeContextMbTestBehaviour contextMb) => ContextMb = contextMb;
    }

    public class BehaviorTreeContextMbTestBehaviour : BehaviorTreeContextMB<BehaviorTreeContextMbTestBehaviour>
    {
        public readonly BehaviorTreeSpy Spy = new BehaviorTreeSpy();
        protected override IBehaviorTree<BehaviorTreeContextMbTestBehaviour> BuildBehaviorTree() => Spy;
    }

    public class BehaviorTreeContextTests
    {
        [UnityTest]
        public IEnumerator BehaviorTreeContext_ContextIsPassedToBehaviorTree()
        {
            var sut = new GameObject().AddComponent<BehaviorTreeContextMbTestBehaviour>();
            yield return null;

            sut.Tick();

            Assert.NotNull(sut.Spy.ContextMb);
            Assert.AreSame(sut, sut.Spy.ContextMb);
        }
    }
}