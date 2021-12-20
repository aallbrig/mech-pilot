using System;
using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;
using NUnit.Framework;

namespace Tests.EditMode.Core.AI.BehaviorTrees.Behaviors
{
    public class ConditionInstantUnitTests
    {
        [Test]
        public void ConditionInstant_ReturnsSuccessWhenPredicateIsTrue()
        {
            var predicate = new Func<bool>(() => true);
            var sut = new ConditionInstant(predicate);

            sut.Tick();

            Assert.AreEqual(Behavior.Status.Success, sut.CurrentStatus);
        }

        [Test]
        public void ConditionInstant_ReturnsFailureWhenPredicateIsFalse()
        {
            var predicate = new Func<bool>(() => false);
            var sut = new ConditionInstant(predicate);

            sut.Tick();

            Assert.AreEqual(Behavior.Status.Failure, sut.CurrentStatus);
        }
    }
}