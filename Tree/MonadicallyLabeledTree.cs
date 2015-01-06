using System;

namespace Program.Tree
{
    public class MonadicallyLabeledTree
    {
        public static Tree<StateContentPair<TState, T>> Label<TState, T>(Tree<T> tree, StateMonad<TState, TState> updateMonad, TState initialState)
        {
            var monad = CreateLabeledTree(tree, updateMonad);
            var stateContentPair = monad.ToStateContentPair(initialState);
            return stateContentPair.Content;
        }

        private static StateMonad<TState, Tree<StateContentPair<TState, T>>> CreateLabeledTree<TState, T>(Tree<T> tree, StateMonad<TState, TState> updateMonad)
        {
            if (tree is Leaf<T>)
            {
                var leaf = (tree as Leaf<T>);

                return updateMonad.Bind(n => StateMonad.Return<TState, Tree<StateContentPair<TState, T>>>(Leaf.Create(StateContentPair.Create(n, leaf.Content))));
            }
            if (tree is Branch<T>)
            {
                var branch = (tree as Branch<T>);

                // recursion
                return CreateLabeledTree(branch.Left, updateMonad)
                    .Bind(newleft => CreateLabeledTree(branch.Right, updateMonad)
                        .Bind(newright => StateMonad.Return<TState, Tree<StateContentPair<TState, T>>>(Branch.Create(newleft, newright))));
            }
            throw new Exception("MakeMonad/MLabel: impossible tree subtype");
        }
    }
}
