using Sprache;

namespace Sprachtml.Models
{
    public class HtmlAttribute
    {
        public string Name { get; }
        public QuotedString Value { get; }

        public bool Binary { get; }

        public HtmlAttribute(string name, IOption<QuotedString> value )
        {
            Name = name;
            if (value.IsDefined)
            {
                Value = value.Get();
            }
            else
            {
                Binary = true;
            }
        }

    }
}