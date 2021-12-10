using System;
using System.Collections;
using Core.AI;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class AgentController : BehaviorTreeContext<AgentController, AgentBehavior>
    {
        public AgentBehaviorTree agentBehaviorTree;
        public float thinkingRate = 0.25f;
        private Coroutine _thinking;

        protected override IBehaviorTree<AgentController, AgentBehavior> BuildBehaviorTree()
        {
            return agentBehaviorTree;
        }

        private void OnEnable() => _thinking = StartCoroutine(Think());

        private void OnDisable() => StopCoroutine(_thinking);

        private IEnumerator Think()
        {
            Tick();
            yield return new WaitForSeconds(thinkingRate);
        }
    }
}