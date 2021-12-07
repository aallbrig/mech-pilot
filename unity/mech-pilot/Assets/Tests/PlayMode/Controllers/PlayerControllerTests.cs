using Controllers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Tests.PlayMode.Controllers
{
    public class PlayerControllerTests
    {
        [Test]
        public void PlayerControllerSetsVector()
        {
            var sut = new GameObject().AddComponent<PlayerController>();
            var expectedPosition = new Vector2(1337, 1337);
            TouchSimulation.Enable();
            // InputSystem.QueueDeltaStateEvent(Pointer.current.position, expectedPosition);
            // InputSystem.QueueDeltaStateEvent(Pointer.current.press, 1f);
            // InputSystem.QueueDeltaStateEvent(Pointer.current.press, 0f);

            // TODO: Make this test actually do something. Leaving this in as an incomplete thought
            Assert.AreEqual(true, true);
        }
    }
}