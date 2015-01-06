using System;

namespace Program.Tree
{
    public static class Branch
    {
        public static Branch<T> Create<T>(Tree<T> left, Tree<T> right)
        {
            return new Branch<T>(left, right);
        }
    }

    public class Branch<T> : Tree<T>
    {
        public Tree<T> Left { get; private set; }
        public Tree<T> Right { get; private set; }

        public Branch(Tree<T> left, Tree<T> right)
        {
            Left = left;
            Right = right;
        }

        public override void Show(int level)
        {
            Console.Write(new String(' ', level * Defaults.Indentation));
            Console.WriteLine("Branch:");
            Left.Show(level + 1);
            Right.Show(level + 1);
        }
    }
}