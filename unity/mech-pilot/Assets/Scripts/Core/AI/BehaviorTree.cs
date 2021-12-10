using UnityEngine;

namespace Core.AI
{
    public class BehaviorTree : ScriptableObject
    {
        private Behavior CurrentBehavior { get; set; }

        // public virtual void Tick(BehaviorTreeContext behaviorTreeContext)
        // {
            // var status = CurrentBehavior.Execute(behaviorTreeContext);
// 
            // switch (status)
            // {
                // case BehaviorExecutionStatus.Running:
                    // break;
                // case BehaviorExecutionStatus.Success:
                    // if (CurrentBehavior.Child) CurrentBehavior = CurrentBehavior.Child;
                    // break;
                // case BehaviorExecutionStatus.Failure:
                    // if (CurrentBehavior.Sibling) CurrentBehavior = CurrentBehavior.Sibling;
                    // break;
            // }
        // }
    }
}