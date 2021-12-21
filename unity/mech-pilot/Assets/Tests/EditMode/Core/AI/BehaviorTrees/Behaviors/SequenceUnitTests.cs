using System.Collections.Generic;
using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.BuildingBlocks;
using NUnit.Framework;
using Tests.EditMode.Core.AI.TestDoubles;

namespace Tests.EditMode.Core.AI.BehaviorTrees.Behaviors
{
    public class SequenceUnitTests
    {
        [Test]
        public void Sequence_NextBehaviorExecuted_WhenPreviousIsSuccessful()
        {
            var firstSpy = new BehaviorSpy(() => Behavior.Status.Success);
            var secondSpy = new BehaviorSpy(() => Behavior.Status.Success);
            var children = new List<Behavior> {firstSpy, secondSpy};
            var sut = new Sequence(children);

            sut.Tick();
            sut.Tick();

            Assert.IsTrue(firstSpy.ExecuteMethodCalled);
            Assert.IsTrue(secondSpy.ExecuteMethodCalled);
            Assert.AreEqual(Behavior.Status.Success, sut.CurrentStatus);
        }

        [Test]
        public void Sequence_NextBehaviorNotExecuted_WhenPreviousIsFailure()
        {
            var firstSpy = new BehaviorSpy(() => Behavior.Status.Failure);
            var secondSpy = new BehaviorSpy(() => Behavior.Status.Success);
            var children = new List<Behavior> {firstSpy, secondSpy};
            var sut = new Sequence(children);

            sut.Tick();

            Assert.IsTrue(firstSpy.ExecuteMethodCalled);
            Assert.IsFalse(secondSpy.ExecuteMethodCalled);
            Assert.AreEqual(Behavior.Status.Failure, sut.CurrentStatus);
        }

        [Test]
        public void Sequence_WillEventuallySucceed_EvenWithLongRunningBehaviors()
        {
            var firstSpyCounter = 0;
            var firstSpy = new BehaviorSpy(() =>
            {
                if (firstSpyCounter >= 3)
                    return Behavior.Status.Success;

                firstSpyCounter++;
                return Behavior.Status.Running;
            });
            var secondSpy = new BehaviorSpy(() => Behavior.Status.Success);
            var children = new List<Behavior> {firstSpy, secondSpy};
            var sut = new Sequence(children);
            Behavior.Status status = Behavior.Status.Failure;

            for (var i = 0; i < 1000; i++)
            {
                status = sut.Tick();
                if (status != Behavior.Status.Running) break;
            }

            Assert.AreEqual(4, firstSpy.ExecuteMethodCallCount);
            Assert.AreEqual(Behavior.Status.Success, status);
        }
    }
}