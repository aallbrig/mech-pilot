using Core.AI;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.Core.AI
{
    public class BehaviorTester : Behavior
    {
        public BehaviorExecutionStatus desiredReturnStatus;
        public override BehaviorExecutionStatus Execute(BehaviorTreeData context) => desiredReturnStatus;
    }
    public class BehaviorTests
    {
        [Test]
        public void Behavior_CanBeEvaluated()
        {
            var sut = ScriptableObject.CreateInstance<BehaviorTester>();
            sut.desiredReturnStatus = BehaviorExecutionStatus.Success;

            var result = sut.Execute(new BehaviorTreeData
            {
                CurrentPlayTime = 0, GameObject = new GameObject()
            });

            Assert.AreEqual(BehaviorExecutionStatus.Success, result);
        }
    }
}