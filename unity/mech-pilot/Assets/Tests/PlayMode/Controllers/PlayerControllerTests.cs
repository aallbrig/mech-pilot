using Controllers;
using NUnit.Framework;
using UnityEngine;
using Locomotion;

namespace Tests.PlayMode.Controllers
{
    public class LocomotionSpy : MonoBehaviour, ILocomotion
    {
        public Vector3 currentNormalizedVector = Vector3.zero;
        public void SetNormalizedVector(Vector3 normalizedVector) => currentNormalizedVector = normalizedVector;
        public void Stop() => throw new System.NotImplementedException();
    }

    public class PlayerControllerTests
    {
        [Test]
        public void PlayerControllerSetsVector()
        {
            var gameObject = new GameObject();
            var spy = gameObject.AddComponent<LocomotionSpy>();
            var sut = gameObject.AddComponent<PlayerController>();
            // TouchSimulation.Enable();
            // InputSystem.QueueDeltaStateEvent(Pointer.current.position, expectedPosition);
            // InputSystem.QueueDeltaStateEvent(Pointer.current.press, 1f);
            // InputSystem.QueueDeltaStateEvent(Pointer.current.press, 0f);

            // TODO: Make this test actually do something. Leaving this in as an incomplete thought
            Assert.AreEqual(true, true);
        }
    }
}