using System;

namespace Program
{
    public static class StateContentPair
    {
        public static StateContentPair<TState, TContent> Create<TState, TContent>(TState state, TContent content)
        {
            return new StateContentPair<TState, TContent>(state, content);
        }
    }

    public class StateContentPair<TState, TContent>
    {
        public StateContentPair(TState state, TContent content)
        {
            State = state;
            Content = content;
        }

        public TState State { get; private set; }
        public TContent Content { get; private set; }

        public override string ToString()
        {
            return String.Format("State: {0}, Contents: {1}", State, Content);
        }
    }
}