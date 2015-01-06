using System;

namespace Program.Tree
{
    public static class Extensions
    {
        public static void Show<T>(this T thing)
        {
            Console.Write("{0}", thing.ToString());
        }
    }
}