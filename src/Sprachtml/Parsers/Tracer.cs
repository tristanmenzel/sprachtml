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
            NodePositions = new Stack<Position>();
            AttrPositions = new Stack<Position>();
        }

        public Stack<Position> NodePositions { get; set; }
        public Stack<Position> AttrPositions { get; set; }

        public Parser<object> Push(string nodeName, Position position)
        {
            Nodes.Push(nodeName);
            NodePositions.Push(position);
            return VoidParser;
        }

        public Parser<object> PushAttr(Position position)
        {
            AttrPositions.Push(position);
            return VoidParser;
        }

        public Parser<object> ResetAttrPositions()
        {
            AttrPositions.Clear();
            return VoidParser;
        }

        public Parser<Position> CurrentPosition
            => input => Result.Success(new Position(input.Position, input.Line, input.Column), input);

        public Parser<object> Pop()
        {
            Nodes.Pop();
            NodePositions.Pop();
            return VoidParser;
        }

        public Parser<object> VoidParser => input => Result.Success<object>(null, input);
    }
}