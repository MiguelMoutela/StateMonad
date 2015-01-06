using System;

namespace Program
{
    public class StateMonad<TState, TContent>
    {
        public Func<TState, StateContentPair<TState, TContent>> ToStateContentPair { get; private set; }

        public StateMonad(Func<TState, StateContentPair<TState, TContent>> toStateContentPair)
        {
            ToStateContentPair = toStateContentPair;
        }
    }

    public static class StateMonad
    {
        public static StateMonad<TState, TContent> Return<TState, TContent>(TContent contents)
        {
            return new StateMonad<TState, TContent>(st => new StateContentPair<TState, TContent>(st, contents));
        } 
    }

    public static class StateMonadExtensions
    {
        public static StateMonad<TState, TResult> Bind<TState, TContent, TResult>(this StateMonad<TState, TContent> source, Func<TContent, StateMonad<TState, TResult>> selector)
        {
            return new StateMonad<TState, TResult>(x =>
            {
                var stateContentPair = source.ToStateContentPair(x);
                var state = stateContentPair.State;
                var content = stateContentPair.Content;
                return selector(content).ToStateContentPair(state);
            });
        }
    }
}