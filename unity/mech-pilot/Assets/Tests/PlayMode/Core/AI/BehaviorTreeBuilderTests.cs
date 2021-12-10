using Core.AI;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.PlayMode.Core.AI
{
    public class BehaviorTreeBuilderTests
    {
        [Test]
        public void BehaviorTreeBuilder_LetsUsersBuildBehaviorTrees()
        {
            var behaviorProvider = new NoopBehaviorProvider();
            var behaviorTree = new NoopBehaviorTree();
            var sut = new BehaviorTreeBuilder<MonoBehaviour, NoopBehavior>(behaviorProvider, behaviorTree);

            var result = sut.Build();

            Assert.IsNotNull(result);
        }
    }
}