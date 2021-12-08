using System.Collections;
using Core.AI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.Core.AI
{
    public class TestBehaviorTreeMonoBehaviour: BehaviorTreeContext {};

    public class BehaviorTreeSpy: IBehaviorTree
    {
        public BehaviorTreeData Data;
        public void Tick(BehaviorTreeData context)
        {
            Data = context;
        }
    }

    public class BehaviorTreeContextTests
    {
        [Test]
        public void BehaviorTreeContext_Blackboard_CanBeWritten()
        {
            var sut = new GameObject().AddComponent<TestBehaviorTreeMonoBehaviour>();

            var result = sut.BlackboardWrite("test-key", new object());
            
            Assert.AreEqual(BlackboardOperationStatus.Success, result.Status);
        }
        
        [Test]
        public void BehaviorTreeContext_Blackboard_WrittenDataCanBeRead()
        {
            var sut = new GameObject().AddComponent<TestBehaviorTreeMonoBehaviour>();
            var value = new object();
            var key = "test-key";
            sut.BlackboardWrite(key, value);

            var result = sut.BlackboardRead(key);
            
            Assert.AreEqual(BlackboardOperationStatus.Success, result.Status);
            Assert.AreEqual(value, result.Data );
        }

        [UnityTest]
        public IEnumerator BehaviorTreeContext_CollaboratesWith_BehaviorTree()
        {
            var gameObject = new GameObject();
            var sut = gameObject.AddComponent<TestBehaviorTreeMonoBehaviour>();
            var spy = new BehaviorTreeSpy();
            sut.behaviorTree = spy;
            yield return null;

            sut.Tick(0);

            Assert.AreEqual(spy.Data, new BehaviorTreeData { CurrentPlayTime = 0, GameObject = gameObject });
        }
    }
}