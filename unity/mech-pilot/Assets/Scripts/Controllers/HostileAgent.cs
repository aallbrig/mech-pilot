using System;
using System.Collections;
using System.Collections.Generic;
using Core.AI.BehaviorTrees;
using Core.AI.BehaviorTrees.Behaviors;
using Core.AI.BehaviorTrees.BuildingBlocks;
using Locomotion;
using UnityEngine;
using Action = Core.AI.BehaviorTrees.BuildingBlocks.Action;

namespace Controllers
{
    [RequireComponent(typeof(MechAgent))]
    public class HostileAgent : MonoBehaviour, IProvideBehaviorTree
    {
        public float playerDetectRadius = 5f;
        public float attackRange = 2.5f;
        public float attackCooldown = 3;
        public bool debugLog;
        private bool _attacking;
        private AgentLocomotion _locomotion;
        private MechAgent _mechAgent;
        private Transform _target;
        private float _waitTimeStart;

        private void Awake()
        {
            // TODO: complain if _mechAgent is null
            _mechAgent = GetComponent<MechAgent>();
            // TODO: complain if _locomotion is null
            _locomotion = GetComponent<AgentLocomotion>();
        }

        public BehaviorTree Build()
        {
            var attackSequence = new Sequence(new List<Behavior>
            {
                new Action(AttackPlayer, () => StartCoroutine(Attack()), () => StopCoroutine(Attack())),
                new Action(() => Wait(attackCooldown), () => _waitTimeStart = Time.time)
            });

            var attackBehavior = new Sequence(new List<Behavior>
            {
                new Condition(DetectPlayer),
                new Action(MoveWithinRange(attackRange), () => _mechAgent.SetColor(Color.yellow), () =>
                {
                    _mechAgent.ResetColor();
                    _locomotion.Stop();
                }),
                new ConditionMonitor(
                    new Condition(() => WithinAttackRange(_target, attackRange)),
                    new Repeater(attackSequence, 3)
                )
            });

            var patrolBehavior = new Sequence(new List<Behavior>
            {
                new Action(MoveToRandomLocation, null, _locomotion.Stop),
                new Action(() => Wait(1)) // wait 1 second
            });

            var rootNode = new Selector(new List<Behavior>
            {
                attackBehavior,
                patrolBehavior
            });

            return new BehaviorTree(rootNode);
        }

        private Behavior.Status Wait(float seconds)
        {
            if (debugLog)
                Debug.Log(
                    $"Wait(waitTime) called {name}, {Time.time}, {_waitTimeStart}, {seconds}, {Time.time - _waitTimeStart}");
            return Time.time - _waitTimeStart > seconds ? Behavior.Status.Success : Behavior.Status.Running;
        }

        private bool WithinAttackRange(Transform target, float range) =>
            Vector3.Distance(transform.position, target.position) < range;

        private bool DetectPlayer()
        {
            if (debugLog) Debug.Log($"DetectPlayer called {name}");
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
            if (debugLog) Debug.Log($"MoveToRandomLocation called {name}");
            // TODO: fill in with actual action
            return Behavior.Status.Success;
        }

        private Func<Behavior.Status> MoveWithinRange(float range) => () =>
        {
            if (debugLog) Debug.Log($"MoveWithinRange called {name}");

            if (_target == null) return Behavior.Status.Failure;

            if (Vector3.Distance(_target.transform.position, transform.position) >= playerDetectRadius)
                return Behavior.Status.Failure;

            if (Vector3.Distance(_target.transform.position, transform.position) <= range)
                return Behavior.Status.Success;

            var vectorToTarget = (_target.transform.position - transform.position).normalized;
            _locomotion.SetNormalizedVector(vectorToTarget);
            return Behavior.Status.Running;
        };

        private Behavior.Status AttackPlayer()
        {
            if (debugLog) Debug.Log($"AttackPlayer called {name}");
            if (_target == null) return Behavior.Status.Failure;

            // TODO fill in with monobehaviour activities
            return _attacking ? Behavior.Status.Running : Behavior.Status.Success;
        }

        private IEnumerator Attack()
        {
            _attacking = true;
            _mechAgent.SetColor(Color.magenta);
            yield return new WaitForSeconds(0.25f);
            _mechAgent.ResetColor();
            _attacking = false;
        }
    }
}