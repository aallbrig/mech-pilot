using Core.AI;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.PlayMode.Core.AI
{
    public class FakeBehaviorProvider : IBehaviorProvider {}

    public class BehaviorTreeBuilderTests
    {
        [Test]
        public void BehaviorTreeBuilder_LetsUsersBuildBehaviorTrees()
        {
            var behaviorProvider = new FakeBehaviorProvider();
            var sut = new BehaviorTreeBuilder(behaviorProvider);

            var result = sut.Build();
            
            Assert.IsNotNull(result);
        }
    }
}