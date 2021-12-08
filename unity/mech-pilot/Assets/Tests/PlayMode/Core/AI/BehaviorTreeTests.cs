using Core.AI;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.Core.AI
{
    public class BehaviorTreeTests
    {
        [Test]
        public void BehaviorTrees_Tick()
        {
            var sut = ScriptableObject.CreateInstance<BehaviorTree>();

            var result = sut.Tick(new GameObject());

            Assert.AreEqual(BehaviorTreeOperationStatus.Success, result.Status);
        }
    }
}