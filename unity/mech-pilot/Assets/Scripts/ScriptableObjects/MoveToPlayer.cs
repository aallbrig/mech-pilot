using Controllers;
using Core.AI;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New move action", menuName = "BehaviorTrees/Agent/MoveAction", order = 0)]
    public class MoveToPlayer : AgentAction
    {
        [Range(0f, 1f)]
        public float speed = 1.0f;
        [Range(1f, 10f)]
        public float withinRange = 5.0f;

        public override BehaviorStatus Status { get; set; }

        public override BehaviorStatus Execute(AgentController context)
        {
            if (context.Locomotion == null)
            {
                Status = BehaviorStatus.Failure;
                return Status;
            }

            var player = context.Blackboard.Read<GameObject>(new BlackboardQueryRequest("Player"));
            var self = context.Blackboard.Read<GameObject>(new BlackboardQueryRequest("GameObject"));

            if (self.Status != BlackboardOperationStatus.Success || player.Status != BlackboardOperationStatus.Success)
            {
                Status = BehaviorStatus.Failure;
                return Status;
            }

            context.Locomotion.CurrentSpeed = speed;
            context.Locomotion.CurrentVector = Vector3.Normalize(player.Data.transform.position - self.Data.transform.position);

            if (Vector3.Distance(self.Data.transform.position, player.Data.transform.position) > withinRange)
                Status = BehaviorStatus.Running;
            else
                Status = BehaviorStatus.Success;

            Status = Vector3.Distance(player.Data.transform.position, self.Data.transform.position) > withinRange
                ? BehaviorStatus.Running
                : BehaviorStatus.Success;

            return Status;
        }
    }
}