using Core.AI.FiniteStateMachines;
using NUnit.Framework;

namespace Tests.EditMode.Core.AI.FiniteStateMachines
{
    public class StateUnitTests
    {
        [Test]
        public void States_ExposesListOfTransitions_ForUpdating()
        {
            var sut = new State(null, null, null);
            
            sut.Transitions.Add(new FakeTransition());
            
            Assert.IsTrue(sut.Transitions.Count > 0);
        }
        [Test]
        public void States_AffordAbilityToSet_EnterCommand()
        {
            var called = false;
            var sut = new State(() => called = true, null, null);
            
            sut.Enter();
            
            Assert.IsTrue(called);
        }

        [Test]
        public void States_AffordAbilityToSet_ExecuteCommand()
        {
            var called = false;
            var sut = new State(null, () => called = true, null);

            sut.Execute();
            
            Assert.IsTrue(called);
        }
        
        [Test]
        public void States_AffordAbilityToSet_ExitCommand()
        {
            var called = false;
            var sut = new State(null, null, () => called = true);

            sut.Exit();
            
            Assert.IsTrue(called);
        }
    }
}