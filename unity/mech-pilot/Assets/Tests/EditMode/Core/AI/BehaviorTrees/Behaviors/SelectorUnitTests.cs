using System.Collections.Generic;
using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.Behaviors.BuildingBlocks;
using NUnit.Framework;
using Tests.EditMode.Core.AI.TestDoubles;

namespace Tests.EditMode.Core.AI.BehaviorTrees.Behaviors
{
    public class SelectorUnitTests
    {
        [Test]
        public void Selector_FirstBehaviorSuccessIsSelected()
        {
            var firstSpy = new BehaviorSpy(Behavior.Status.Failure);
            var secondSpy = new BehaviorSpy(Behavior.Status.Success);
            var children = new List<Behavior> {firstSpy, secondSpy};
            var sut = new Selector(children);

            sut.Tick();
            sut.Tick();

            Assert.IsTrue(firstSpy.ExecuteMethodCalled);
            Assert.IsTrue(secondSpy.ExecuteMethodCalled);
            Assert.AreEqual(Behavior.Status.Success, sut.CurrentStatus);
        }

        [Test]
        public void Selector_FailsWhenAllChildrenFail()
        {
            var firstSpy = new BehaviorSpy(Behavior.Status.Failure);
            var secondSpy = new BehaviorSpy(Behavior.Status.Failure);
            var children = new List<Behavior> {firstSpy, secondSpy};
            var sut = new Selector(children);

            sut.Tick();
            sut.Tick();

            Assert.IsTrue(firstSpy.ExecuteMethodCalled);
            Assert.IsTrue(secondSpy.ExecuteMethodCalled);
            Assert.AreEqual(Behavior.Status.Failure, sut.CurrentStatus);
        }

        [Test]
        public void Selector_DoesNotCallOtherChildrenAfterSuccess()
        {
            var firstSpy = new BehaviorSpy(Behavior.Status.Success);
            var secondSpy = new BehaviorSpy(Behavior.Status.Failure);
            var children = new List<Behavior> {firstSpy, secondSpy};
            var sut = new Selector(children);

            sut.Tick();
            sut.Tick();

            Assert.IsTrue(firstSpy.ExecuteMethodCalled);
            Assert.IsFalse(secondSpy.ExecuteMethodCalled);
            Assert.AreEqual(Behavior.Status.Success, sut.CurrentStatus);
        }
    }
}