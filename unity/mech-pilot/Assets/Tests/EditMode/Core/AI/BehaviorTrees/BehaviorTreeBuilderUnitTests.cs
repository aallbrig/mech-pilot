using Core.AI.BehaviorTrees;
using NUnit.Framework;

namespace Tests.EditMode.Core.AI.BehaviorTrees
{
    public class BehaviorTreeBuilderUnitTests
    {
        [Test]
        public void BTBuilder_RequiresAtLeastOneBehavior_ToBeBuilt()
        {
            var sut = new BehaviorTreeBuilder();

            var result = sut.Build();

            Assert.IsNull(result);
        }
    }
}