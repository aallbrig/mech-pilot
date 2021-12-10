using System.Collections;
using Core.AI;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public interface ILocomotion
    {
        public Vector3 CurrentVector { get; set; }

        public float CurrentSpeed { get; set; }

        public void Move(float deltaTime);
    }

    public class AgentController : BehaviorTreeContext<AgentController>, ILocomotion
    {
        public AgentBehaviorTree agentBehaviorTree;
        public float thinkingRate = 0.25f;
        // Locomotion
        public ILocomotion Locomotion;
        [SerializeField] private Vector3 vector = Vector3.zero;
        [SerializeField] private float speed = 0f;
        private IEnumerator _thinking;

        protected override IBehaviorTree<AgentController> BuildBehaviorTree()
        {
            return agentBehaviorTree;
        }

        private void OnEnable()
        {
            _thinking = Think();
            StartCoroutine(_thinking);
        }

        private void OnDisable() => StopCoroutine(_thinking);

        protected override void Awake()
        {
            base.Awake();
            Locomotion = this;
            Locomotion.CurrentSpeed = speed;
            Locomotion.CurrentVector = vector;

            var player = GameObject.FindObjectOfType<PlayerController>();
            Blackboard.Write("Player", player.gameObject);
        }

        private IEnumerator Think()
        {
            while (true)
            {
                Tick();
                yield return new WaitForSeconds(thinkingRate);
            }
        }

        public Vector3 CurrentVector { get; set; }
        public float CurrentSpeed { get; set; }

        protected override void Update()
        {
            base.Update();
            Move(Time.deltaTime);
        }

        public void Move(float deltaTime)
        {
            gameObject.transform.Translate(CurrentVector * CurrentSpeed * deltaTime);
        }
    }
}