using System.Collections;
using Core.AI;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests.PlayMode.Core.AI
{
    public class TestBehaviorTreeMonoBehaviour: BehaviorTreeContext
    {
        public BehaviorTreeSpy spy = new BehaviorTreeSpy();
        protected override IBehaviorTree BuildBehaviorTree() => spy;
    };

    public class BehaviorTreeSpy: IBehaviorTree
    {
        public BehaviorTreeContext context;
        public void Tick(BehaviorTreeContext context) => this.context = context;
    }

    public class BehaviorTreeContextTests
    {

        [UnityTest]
        public IEnumerator BehaviorTreeContext_CollaboratesWith_BehaviorTree()
        {
            var gameObject = new GameObject();
            var sut = gameObject.AddComponent<TestBehaviorTreeMonoBehaviour>();
            yield return null;

            sut.Tick();

            Assert.NotNull(sut.spy.context);
        }
    }
}