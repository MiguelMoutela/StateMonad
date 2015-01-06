using System.Collections.Generic;
using System.Linq;

namespace Program.List
{
    public static class LabelList
    {
        public static IEnumerable<StateContentPair<TState, TContent>> Label<TState, TContent>(IEnumerable<TContent> list, StateMonad<TState, TState> updateMonad, TState initialState)
        {
            var monad = CreateStateMonadFunctionally(list, updateMonad);
            return monad.ToStateContentPair(initialState).Content;
        }

        private static StateMonad<TState, IEnumerable<StateContentPair<TState, TContent>>> CreateStateMonadRecursively<TState, TContent>(IEnumerable<TContent> list, StateMonad<TState, TState> updateMonad)
        {
            return CreateStateMonadRecursively(list.ToList(), updateMonad, StateMonad.Return<TState, IEnumerable<StateContentPair<TState, TContent>>>(new List<StateContentPair<TState, TContent>>()));
        }

        private static StateMonad<TState, IEnumerable<StateContentPair<TState, TContent>>> CreateStateMonadRecursively<TState, TContent>(IReadOnlyCollection<TContent> list, StateMonad<TState, TState> updateMonad, StateMonad<TState, IEnumerable<StateContentPair<TState, TContent>>> result)
        {
            if (!list.Any())
                return result;

            return CreateStateMonadRecursively(list.Skip(1).ToList(), updateMonad, result
                .Bind(res => updateMonad
                    .Bind(state => StateMonad.Return<TState, IEnumerable<StateContentPair<TState, TContent>>>(new[] { StateContentPair.Create(state, list.First()) }))
                    .Bind(nextItem => StateMonad.Return<TState, IEnumerable<StateContentPair<TState, TContent>>>(res.Concat(nextItem)))));
        }

        public static StateMonad<TState, IEnumerable<StateContentPair<TState, TContent>>> CreateStateMonadFunctionally<TState, TContent>(IEnumerable<TContent> list, StateMonad<TState, TState> updateMonad)
        {
            var monad = list
                .Aggregate(StateMonad.Return<TState, IEnumerable<StateContentPair<TState, TContent>>>(new List<StateContentPair<TState, TContent>>()), (current, s) => current
                    .Bind(x1 => updateMonad
                        .Bind(x => StateMonad.Return<TState, IEnumerable<StateContentPair<TState, TContent>>>(new[] { StateContentPair.Create(x, s) }))
                        .Bind(x2 => StateMonad.Return<TState, IEnumerable<StateContentPair<TState, TContent>>>(x1.Concat(x2)))));
            return monad;
        }
    }
}
