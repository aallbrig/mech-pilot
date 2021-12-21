using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.BuildingBlocks;
using NUnit.Framework;
using Tests.EditMode.Core.AI.TestDoubles;

namespace Tests.EditMode.Core.AI.BehaviorTrees.Behaviors
{
    public class RepeaterUnitTests
    {
        [Test]
        public void Repeaters_CanRepeatAnAction_MultipleTimes()
        {
            var spy = new BehaviorSpy(() => Behavior.Status.Success);
            var sut = new Repeater(spy, 3);

            // for loop instead of a while loop, to protect me from my code ^_^
            // "1000" is arbitrary
            for (int i = 0; i < 1000; i++)
            {
                 var status = sut.Tick();
                 if (status == Behavior.Status.Success) break;
            }

            Assert.AreEqual(3, spy.InitializeMethodCallCount);
            Assert.AreEqual(3, spy.ExecuteMethodCallCount);
            Assert.AreEqual(3, spy.TerminateMethodCallCount);
        }
    }
}