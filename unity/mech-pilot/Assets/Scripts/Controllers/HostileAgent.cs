using System;
using System.Collections;
using System.Collections.Generic;
using Editor.BehaviorTrees;
using Editor.BehaviorTrees.Behaviors;
using Editor.BehaviorTrees.BuildingBlocks;
using Locomotion;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private Transform _player;
        private float _waitTimeStart;

        private void Awake()
        {
            // TODO: complain if _mechAgent is null
            _mechAgent = GetComponent<MechAgent>();
            // TODO: complain if _locomotion is null
            _locomotion = GetComponent<AgentLocomotion>();
            // TODO: complain if _player is null
            _player = FindObjectOfType<PlayerController>().transform;
        }

        public BehaviorTree Build()
        {
            var attackSequence = new Sequence(new List<Behavior>
            {
                new TaskAction(AttackPlayer, () => StartCoroutine(Attack()), () => StopCoroutine(Attack())),
                new TaskAction(() => Wait(attackCooldown), () => _waitTimeStart = Time.time)
            });

            var attackBehavior = new Sequence(new List<Behavior>
            {
                new Condition(DetectPlayer),
                new TaskAction(MoveWithinRange(attackRange), () => _mechAgent.SetColor(Color.yellow), () =>
                {
                    _mechAgent.ResetColor();
                    _locomotion.Stop();
                }),
                new ConditionMonitor(
                    new Condition(() => WithinRange(_player, attackRange)),
                    new Repeater(attackSequence, 3)
                )
            });

            var chaseBehavior = new Sequence(new List<Behavior>
            {
                new TaskAction(() => MoveTowards(_player)),
                new TaskAction(() => Wait(PatrolWaitTime()))
            });

            var rootNode = new Selector(new List<Behavior>
            {
                attackBehavior,
                chaseBehavior
            });

            return new BehaviorTree(rootNode);
        }

        private float PatrolWaitTime() => Random.Range(0f, 1.0f) * 6;

        private Behavior.Status Wait(float seconds)
        {
            if (debugLog)
                Debug.Log(
                    $"Wait(waitTime) called {name}, {Time.time}, {_waitTimeStart}, {seconds}, {Time.time - _waitTimeStart}");
            return Time.time - _waitTimeStart > seconds ? Behavior.Status.Success : Behavior.Status.Running;
        }

        private bool WithinRange(Transform target, float range) =>
            Vector3.Distance(transform.position, target.position) < range;

        private bool DetectPlayer()
        {
            if (debugLog) Debug.Log($"DetectPlayer called {name}");
            return Vector3.Distance(_player.position, transform.position) < playerDetectRadius;
        }

        private Behavior.Status MoveTowards(Transform target)
        {
            var normalizedVector = (target.position - transform.position).normalized;
            if (debugLog) Debug.Log($"MoveToRandomLocation called {name} {normalizedVector}");
            _locomotion.NewMovementDirection(normalizedVector);
            return Behavior.Status.Success;
        }

        private Func<Behavior.Status> MoveWithinRange(float range) => () =>
        {
            if (debugLog) Debug.Log($"MoveWithinRange called {name}");

            if (_player == null) return Behavior.Status.Failure;

            if (Vector3.Distance(_player.position, transform.position) >= playerDetectRadius)
                return Behavior.Status.Failure;

            if (WithinRange(_player, range))
                return Behavior.Status.Success;

            var vectorToTarget = (_player.position - transform.position).normalized;
            _locomotion.NewMovementDirection(vectorToTarget);
            return Behavior.Status.Running;
        };

        private Behavior.Status AttackPlayer()
        {
            if (debugLog) Debug.Log($"AttackPlayer called {name}");
            if (_player == null) return Behavior.Status.Failure;

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