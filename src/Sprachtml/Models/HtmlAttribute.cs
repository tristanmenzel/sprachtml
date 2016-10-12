using Sprache;

namespace Sprachtml.Models
{
    public class HtmlAttribute
    {
        public string Name { get; }
        public string Value { get; }

        public bool Binary { get; }

        public HtmlAttribute(string name, IOption<QuotedString> value )
        {
            Name = name;
            if (value.IsDefined)
            {
                Value = value.Get().Text;
                QuoteType = value.Get().QuoteType;
            }
            else
            {
                Binary = true;
            }
        }

        public QuoteType QuoteType { get;  }
    }
}