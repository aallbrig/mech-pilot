using Core.AI;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.Core.AI
{
    public class TestBehaviorTreeMonoBehaviour : BehaviorTreeContext {};

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
    }
}