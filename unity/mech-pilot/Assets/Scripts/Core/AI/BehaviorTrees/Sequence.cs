using System.Collections.Generic;

namespace Core.AI.BehaviorTrees
{
    public class Sequence: Behavior
    {
        public Sequence(List<Behavior> children) => _children = children;

        private readonly List<Behavior> _children;

        public override BehaviorStatus Execute()
        {
            foreach (var behavior in _children)
            {
                switch (behavior.Execute())
                {
                    case BehaviorStatus.Running:
                        Status = BehaviorStatus.Running;
                        return Status;
                    case BehaviorStatus.Success:
                        break;
                    case BehaviorStatus.Failure:
                        Status = BehaviorStatus.Failure;
                        return Status;
                }
            }

            return BehaviorStatus.Success;
        }
    }
}