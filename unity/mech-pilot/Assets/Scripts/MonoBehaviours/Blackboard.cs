using UnityEngine;

namespace MonoBehaviours
{
    public class Blackboard : MonoBehaviour
    {
        [SerializeField]
        public Core.AI.Blackboard instance = new Core.AI.Blackboard();
    }
}