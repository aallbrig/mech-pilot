using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.BuildingBlocks;
using NUnit.Framework;

namespace Tests.EditMode.Core.AI.BehaviorTrees.Behaviors
{
    public class ActionUnitTests
    {
        [Test]
        public void ActionBehaviors_DeferExecutionDetailsToCaller()
        {
            var spyCalled = false;
            var spyAction = new Action.ActionCommand(() =>
            {
                spyCalled = true;
                return Behavior.Status.Success;
            });
            var sut = new Action(spyAction);

            sut.Tick();

            Assert.IsTrue(spyCalled);
        }

        [Test]
        public void ActionBehaviors_OptionallyAllowCustomSetupLogic()
        {
            var spyCalled = false;
            var spySetup = new Action.ActionSetup(() => spyCalled = true);
            var sut = new Action(() => Behavior.Status.Success, spySetup);

            sut.Tick();

            Assert.IsTrue(spyCalled);
        }

        [Test]
        public void ActionBehaviors_OptionallyAllowCustomTeardownLogic()
        {
            var spyCalled = false;
            var spyTeardown = new Action.ActionTeardown(() => spyCalled = true);
            var sut = new Action(() => Behavior.Status.Success, null, spyTeardown);

            sut.Tick();

            Assert.IsTrue(spyCalled);
        }
    }
}