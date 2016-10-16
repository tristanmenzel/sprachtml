using System;
using System.Collections.Generic;
using Sprache;

namespace Sprachtml.Parsers
{
    public class Tracer
    {
        [ThreadStatic] public static Tracer Instance;

        public Stack<string> Nodes { get; }

        public Tracer()
        {
            Nodes = new Stack<string>();
        }

        public Parser<object> Push(string nodeName)
        {
            Nodes.Push(nodeName);

            return VoidParser;
        }

        public Parser<object> Pop()
        {
            Nodes.Pop();

            return VoidParser;
        }

        public Parser<object> VoidParser => input => Result.Success<object>(null, input);
    }
}