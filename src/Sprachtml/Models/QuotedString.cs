namespace Sprachtml.Models
{
    public class QuotedString
    {
        public QuotedString(QuoteType quoteType, string text)
        {
            QuoteType = quoteType;
            Text = text;
        }
        public QuoteType QuoteType { get;  }
        public string Text { get;  }
    }
}