using UnityEngine;

namespace Core.AI
{
    public abstract class BehaviorTree : ScriptableObject, IBehaviorTree
    {
        public Behavior CurrentBehavior { get; set; }

        public virtual void Tick(BehaviorTreeData context)
        {
            var status = CurrentBehavior.Execute(context);

            switch (status)
            {
                case BehaviorExecutionStatus.InProgress:
                    break;
                case BehaviorExecutionStatus.Success:
                    if (CurrentBehavior.Child) CurrentBehavior = CurrentBehavior.Child;
                    break;
                case BehaviorExecutionStatus.Failure:
                    if (CurrentBehavior.Sibling) CurrentBehavior = CurrentBehavior.Sibling;
                    break;
            }
        }
    }
}