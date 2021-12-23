using System.Collections.Generic;
using Core.AI.BehaviorTrees;
using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.BuildingBlocks;
using UnityEngine;
using Action = Core.AI.BehaviorTrees.Behaviors.Action;

namespace Controllers
{
    [RequireComponent(typeof(MechAgent))]
    public class HostileAgent : MonoBehaviour, IProvideBehaviorTree
    {
        public float playerDetectRadius = 5f;
        public float attackRange = 2.5f;
        public float attackCooldown;
        public float speed = 2f;
        private Vector3? _destination;
        private Transform _target;
        private MechAgent _mechAgent;
        private float _attackTime;

        private void Awake()
        {
            _mechAgent = GetComponent<MechAgent>();
            // TODO: complain if _mechAgent is null
        }

        private void Update()
        {
            if (_destination != null)
                transform.position = Vector3.MoveTowards(transform.position, (Vector3) _destination, Time.deltaTime * speed);
        }

        public BehaviorTree Build()
        {
            var attackSequence = new Sequence(new List<Behavior>
            {
                new Action(AttackPlayer, null, () => _attackTime = Time.time),
                new Action(AttackCooldown)
            });

            var attackBehavior = new Sequence(new List<Behavior>
            {
                new ConditionInstant(DetectPlayer),
                new Action(MoveWithinRange(attackRange), () => _mechAgent.SetColor(Color.yellow), () =>
                {
                    _mechAgent.ResetColor();
                    ResetDestination();
                }),
                new ConditionMonitor(
                    () => WithinAttackRange(_target, attackRange),
                    new Repeater(attackSequence, 3)
                )
            });

            var patrolBehavior = new Sequence(new List<Behavior>
            {
                new Action(MoveToRandomLocation, null, ResetDestination)
            });

            var rootNode = new Selector(new List<Behavior>
            {
                attackBehavior,
                patrolBehavior
            });

            return new BehaviorTree(rootNode);
        }

        private Behavior.Status AttackCooldown()
        {
            return Time.time - _attackTime > attackCooldown ? Behavior.Status.Success : Behavior.Status.Running;
        }

        private bool WithinAttackRange(Transform target, float range) =>
            Vector3.Distance(transform.position, target.position) < range;

        private bool DetectPlayer()
        {
            var collisions = Physics.OverlapSphere(transform.position, playerDetectRadius);
            foreach (var collision in collisions)
                if (collision.transform.GetComponent<PlayerController>())
                {
                    // This is awesome! It's like using the monobehavior itself as the blackboard
                    // ... which right now seems like a good idea :)
                    _target = collision.transform;
                    return true;
                }
            return false;
        }

        private Behavior.Status MoveToRandomLocation()
        {
            Debug.Log("MoveToRandomLocation called");
            // TODO: fill in with actual action
            return Behavior.Status.Success;
        }

        private Action.ActionCommand MoveWithinRange(float range) => () =>
        {
            if (_target == null) return Behavior.Status.Failure;

            if (Vector3.Distance(_target.transform.position, transform.position) >= playerDetectRadius)
                return Behavior.Status.Failure;

            if (Vector3.Distance(_target.transform.position, transform.position) <= range)
                return Behavior.Status.Success;

            _destination = _target.position;
            return Behavior.Status.Running;
        };

        private Behavior.Status AttackPlayer()
        {
            Debug.Log("AttackPlayer called");
            if (_target == null) return Behavior.Status.Failure;

            // TODO fill in with monobehavior activities
            return Behavior.Status.Success;
        }

        private void ResetDestination() => _destination = null;
    }
}