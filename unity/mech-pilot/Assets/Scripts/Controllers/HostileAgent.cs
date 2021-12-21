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
        public float attackRange = 2.5f;
        public float speed = 2f;
        private Vector3? _destination;
        private Transform _target;

        private void Update()
        {
            if (_destination != null)
                transform.position = Vector3.MoveTowards(transform.position, (Vector3) _destination, Time.deltaTime * speed);
        }

        public BehaviorTree Build()
        {
            var attackSequence = new Sequence(new List<Behavior>
            {
                new ConditionInstant(DetectPlayer),
                new Action(MoveWithinRange(attackRange), null, () => _destination = null),
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
                {
                    _target = collision.transform;
                    return true;
                }
            return false;
        }

        private Behavior.Status MoveToRandomLocation()
        {
            Debug.Log("MoveToRandomLocation called");
            return Behavior.Status.Success;
        }

        private Action.ActionCommand MoveWithinRange(float range) => () =>
        {
            if (_target == null) return Behavior.Status.Failure;

            if (Vector3.Distance(_target.transform.position, transform.position) <= range)
                return Behavior.Status.Success;

            _destination = _target.position;
            return Behavior.Status.Running;
        };

        private Behavior.Status AttackPlayer()
        {
            Debug.Log("AttackPlayer called");
            return Behavior.Status.Success;
        }
    }
}