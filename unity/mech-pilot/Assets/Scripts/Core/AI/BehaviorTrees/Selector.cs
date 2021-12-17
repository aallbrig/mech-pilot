using System.Collections.Generic;

namespace Core.AI.BehaviorTrees
{
    public class Selector: Behavior
    {
        private readonly List<Behavior> _children;
        public Selector(List<Behavior> children) => _children = children;

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
                        Status = BehaviorStatus.Success;
                        return Status;
                    case BehaviorStatus.Failure:
                        break;
                }
            }

            return Status;
        }
    }
}