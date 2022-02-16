using System.Collections;
using Editor.BehaviorTrees;
using UnityEngine;

namespace Controllers
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        public float thinkRate = 0.25f;
        private BehaviorTree _behaviorTree;
        private Coroutine _thinking;

        private void Awake()
        {
            // For now, assume only one BT is defined on the game object
            // Later, smart objects will require a different strategy
            var behaviorTreeBuilder = GetComponent<IProvideBehaviorTree>();
            if (behaviorTreeBuilder == null) return; // TODO: complain/notify creator of code expectation violation

            _behaviorTree = behaviorTreeBuilder.Build();
        }

        private void OnEnable() => _thinking = StartCoroutine(Think());
        private void OnDisable() => StopCoroutine(_thinking);

        private IEnumerator Think()
        {
            while (true)
            {
                _behaviorTree.Tick();
                yield return new WaitForSeconds(thinkRate);
            }
        }
    }
}