using System;
using System.Linq;
using Program.List;
using Program.Tree;

namespace Program
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Monadically labeled List:");

            var list = new[] { "C#", "F#", "Haskell", "Clojure" };
            var labeledList = LabelList.Label(list, new StateMonad<char, char>(s => StateContentPair.Create((char)(s + 1), s)), 'a');
            labeledList.ToList().ForEach(Console.WriteLine);
        }

        private static void LabelTrees()
        {
            Console.WriteLine("Unlabeled Tree:");
            var tree = Branch.Create(
                Leaf.Create("a"),
                Branch.Create(
                    Branch.Create(
                        Leaf.Create("b"),
                        Leaf.Create("c")),
                    Leaf.Create("d")));
            tree.Show(2);

            Console.WriteLine();
            Console.WriteLine("Hand-Labeled Tree:");
            var tree1 = Branch.Create(
                Leaf.Create(StateContentPair.Create(0, "a")),
                Branch.Create(
                    Branch.Create(
                        Leaf.Create(StateContentPair.Create(1, "b")),
                        Leaf.Create(StateContentPair.Create(2, "c"))),
                    Leaf.Create(StateContentPair.Create(3, "d"))));
            tree1.Show(2);

            Console.WriteLine();
            Console.WriteLine("Non-monadically Labeled Tree:");
            var tree2 = HandLabeledTree.Label(tree, n => (n + 1), 0);
            tree2.Show(2);

            Console.WriteLine();
            Console.WriteLine("Monadically Labeled Tree:");
            var tree3 = MonadicallyLabeledTree.Label(tree, new StateMonad<int, int>(n => StateContentPair.Create(n + 1, n)), 0);
            tree3.Show(2);

            Console.WriteLine();
        }
    }
}
