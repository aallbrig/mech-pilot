namespace Core.AI.HierarchicalTaskNetworks
{
    // Compound
    // Primitive
    public class Planner
    {
        private readonly CompoundTask _rootCompound;
        // Decomposing list
        // final plan
        public Planner(CompoundTask rootCompound, WorldState state)
        {
            _rootCompound = rootCompound;
        }

        public Plan Plan()
        {
            return new Plan();
        }
    }
}