using System;
using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;
using NUnit.Framework;
using Tests.EditMode.Core.AI.TestDoubles;

namespace Tests.EditMode.Core.AI.BehaviorTrees.Behaviors
{
    public class ConditionInstantUnitTests
    {
        private static Behavior.Status[] _childBehaviorStatuses = new Behavior.Status[]
        {
            Behavior.Status.Success,
            Behavior.Status.Running,
            Behavior.Status.Failure
        };
        [Test]
        public void ConditionInstant_ExecutesChildBehavior_WhenPredicateEvaluatesTrue([ValueSource(nameof(_childBehaviorStatuses))] Behavior.Status spyReturnStatus)
        {
            var predicate = new Func<bool>(() => true);
            var spy = new BehaviorSpy(spyReturnStatus);
            var sut = new ConditionInstant(spy, predicate);

            sut.Tick();

            Assert.IsTrue(spy.ExecuteMethodCalled);
            Assert.AreEqual(spyReturnStatus, sut.CurrentStatus);
        }

        [Test]
        public void ConditionInstant_ReturnsFailure_WhenPredicateEvaluatesFalse()
        {
            var predicate = new Func<bool>(() => false);
            var behaviorFake = new BehaviorFake();
            var sut = new ConditionInstant(behaviorFake, predicate);

            sut.Tick();

            Assert.AreEqual(Behavior.Status.Failure, sut.CurrentStatus);
        }
    }
}