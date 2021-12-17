namespace Core.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class Behavior
    {
        public BehaviorStatus Status { get; set; }
        public abstract BehaviorStatus Execute();
    }
}