using System;

namespace Program.Tree
{
    public static class Leaf
    {
        public static Leaf<T> Create<T>(T content)
        {
            return new Leaf<T>(content);
        }
    }

    public class Leaf<T> : Tree<T>
    {
        public Leaf(T content)
        {
            Content = content;
        }

        public T Content { get; private set; }

        public override void Show(int level)
        {
            Console.Write(new String(' ', level * Defaults.Indentation));
            Console.Write("Leaf: ");
            Content.Show();
            Console.WriteLine();
        }
    }
}