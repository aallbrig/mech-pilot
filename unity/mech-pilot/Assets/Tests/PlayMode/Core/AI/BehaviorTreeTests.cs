using Core.AI;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.Core.AI
{
    public class BehaviorTreeTester : BehaviorTree
    {
        public override void Tick(BehaviorTreeData context) {}
    }

    public class BehaviorTreeTests
    {
        [Test]
        public void BehaviorTrees_CanBeInstantiated() => Assert.NotNull(ScriptableObject.CreateInstance<BehaviorTreeTester>());
    }
}