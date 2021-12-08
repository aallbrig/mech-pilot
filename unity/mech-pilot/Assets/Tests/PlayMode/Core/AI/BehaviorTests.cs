using Core.AI;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.Core.AI
{
    public class BehaviorTests
    {
        [Test]
        public void Behavior_CanBeEvaluated()
        {
            var sut = ScriptableObject.CreateInstance<Behavior>();

            var result = sut.Execute(new GameObject());
            Assert.AreEqual(BehaviorExecuteStatus.Success, result.Status);
        }
    }
}