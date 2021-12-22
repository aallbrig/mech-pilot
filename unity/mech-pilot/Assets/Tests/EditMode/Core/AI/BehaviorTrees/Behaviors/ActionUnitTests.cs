using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.BuildingBlocks;
using NUnit.Framework;
using Tests.EditMode.Core.AI.BehaviorTrees.Utilities;

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

            BehaviorTestHarness.RunToComplete(sut);

            Assert.IsTrue(spyCalled);
        }

        [Test]
        public void ActionBehaviors_OptionallyAllowCustomSetupLogic()
        {
            var spyCalled = false;
            var spySetup = new Action.ActionSetup(() => spyCalled = true);
            var sut = new Action(() => Behavior.Status.Success, spySetup);

            BehaviorTestHarness.RunToComplete(sut);

            Assert.IsTrue(spyCalled);
        }

        [Test]
        public void ActionBehaviors_OptionallyAllowCustomTeardownLogic()
        {
            var spyCalled = false;
            var spyTeardown = new Action.ActionTeardown(() => spyCalled = true);
            var sut = new Action(() => Behavior.Status.Success, null, spyTeardown);

            BehaviorTestHarness.RunToComplete(sut);

            Assert.IsTrue(spyCalled);
        }
    }
}