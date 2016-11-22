using System;
using System.Collections.Generic;
using Sprache;

namespace Sprachtml.Parsers
{
    public class Tracer
    {
        public Stack<string> Nodes { get; }

        public Tracer()
        {
            Nodes = new Stack<string>();
            Positions = new Stack<Position>();
        }

        public Stack<Position> Positions { get; set; }

        public Parser<object> Push(string nodeName)
        {
            Nodes.Push(nodeName);

            return VoidParser(true);
        }

        public Parser<object> Pop()
        {
            Nodes.Pop();
            Positions.Pop();
            return VoidParser(false);
        }

        public Func<bool, Parser<object>> VoidParser => pushPosition =>
            input =>
            {
                if (pushPosition)
                    Positions.Push(new Position(input.Position, input.Line, input.Column));
                return Result.Success<object>(null, input);
            };
    }
}