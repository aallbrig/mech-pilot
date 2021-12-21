using System.Collections.Generic;
using Core.AI.BehaviorTrees;
using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.BuildingBlocks;
using UnityEngine;

namespace Controllers
{
    public class HostileAgent : MonoBehaviour, IProvideBehaviorTree
    {
        public float playerDetectRadius = 5f;

        public BehaviorTree Build()
        {
            var attackSequence = new Sequence(new List<Behavior>
            {
                new ConditionInstant(DetectPlayer),
                new Action(MoveWithinAttackRange),
                new Repeater(new Action(AttackPlayer), 3)
            });

            var patrolSequence = new Sequence(new List<Behavior>
            {
                new Action(MoveToRandomLocation)
            });

            var rootNode = new Selector(new List<Behavior>
            {
                attackSequence,
                patrolSequence
            });

            return new BehaviorTree(rootNode);
        }

        private bool DetectPlayer()
        {
            Debug.Log("Detect player called");
            var collisions = Physics.OverlapSphere(transform.position, playerDetectRadius);
            foreach (var collision in collisions)
                if (collision.transform.GetComponent<PlayerController>())
                    return true;
            return false;
        }

        private Behavior.Status MoveToRandomLocation()
        {
            Debug.Log("MoveToRandomLocation called");
            return Behavior.Status.Success;
        }

        private Behavior.Status MoveWithinAttackRange()
        {
            Debug.Log("MoveToAttackRange called");
            return Behavior.Status.Success;
        }

        private Behavior.Status AttackPlayer()
        {
            Debug.Log("AttackPlayer called");
            return Behavior.Status.Success;
        }
    }
}