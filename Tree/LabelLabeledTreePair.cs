namespace Program.Tree
{
    public static class LabelLabeledTreePair
    {
        public static LabelLabeledTreePair<TState, T> Create<TState, T>(TState label, Tree<StateContentPair<TState, T>> lltpTree)
        {
            return new LabelLabeledTreePair<TState, T>(label, lltpTree);
        }
    }

    public class LabelLabeledTreePair<TState, T>
    {
        public TState State { get; private set; }
        public Tree<StateContentPair<TState, T>> LltpTree { get; private set; }

        public LabelLabeledTreePair(TState state, Tree<StateContentPair<TState, T>> lltpTree)
        {
            State = state;
            LltpTree = lltpTree;
        }
    }
}