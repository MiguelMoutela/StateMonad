using System;

namespace Program.Tree
{
    public class HandLabeledTree
    {
        public static Tree<StateContentPair<TState, T>> Label<TState, T>(Tree<T> tree, Func<TState, TState> updateState, TState initialState)
        {
            var labelLabeledTreePair = LabelWithState(tree, updateState, initialState);
            return labelLabeledTreePair.LltpTree;
        }

        private static LabelLabeledTreePair<TState, T> LabelWithState<TState, T>(Tree<T> tree, Func<TState, TState> updateState, TState state)
        {
            if (tree is Leaf<T>)
            {
                var leaf = (tree as Leaf<T>);
                return LabelLabeledTreePair.Create(updateState(state), Leaf.Create(StateContentPair.Create(state, leaf.Content)));
            }
            if (tree is Branch<T>)
            {
                var branch = (tree as Branch<T>);
                var left = LabelWithState(branch.Left, updateState, state); // recursive call
                var right = LabelWithState(branch.Right, updateState, left.State); // threading
                return LabelLabeledTreePair.Create(right.State, Branch.Create(left.LltpTree, right.LltpTree));
            }
            throw new Exception("Lab/Label: impossible tree subtype");
        }
    }
}
