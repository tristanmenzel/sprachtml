using System;
using System.Linq;

namespace Sprachtml.Models
{
    public class TagIdentifier
    {
        public TagIdentifier(string identifier)
        {
            Value = identifier;
            // SingleOrDefault will default to custom for no match
            NodeType = Enum.GetValues(typeof(HtmlNodeType))
                .Cast<HtmlNodeType>()
                .SingleOrDefault(n => string.Equals(n.ToString(), identifier, StringComparison.InvariantCultureIgnoreCase));


        }
        public string Value { get; }
        public HtmlNodeType NodeType { get; }

    }
}