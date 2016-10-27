using System;
using System.Linq;
using Sprachtml.Helpers;

namespace Sprachtml.Models
{
    public class TagIdentifier
    {
        public TagIdentifier(string identifier)
        {
            Value = identifier;
            NodeType = TagHelper.GetTypeFromName(identifier);


        }
        public string Value { get; }
        public HtmlNodeType NodeType { get; }

    }
}