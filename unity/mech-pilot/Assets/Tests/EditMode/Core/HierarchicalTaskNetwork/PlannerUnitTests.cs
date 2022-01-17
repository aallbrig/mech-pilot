using Core.AI.HierarchicalTaskNetworks;
using NUnit.Framework;

namespace Tests.EditMode.Core.HierarchicalTaskNetwork
{
    public class PlannerUnitTests
    {
        [Test]
        public void APlannerCanComeUpWithAFinalPlan()
        {
            var sut = new Planner(new CompoundTask(), new WorldState());

            var result = sut.Plan();
            
            Assert.NotNull(result);
        }
    }
}