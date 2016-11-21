using System;
using System.Linq;
using Sprachtml.Meta;
using Sprachtml.Models;

namespace Sprachtml.Helpers
{
    public class TagHelper
    {
        public static HtmlNodeType[] GetVoidNodeTypes()
        {
            return Enum.GetValues(typeof(HtmlNodeType))
                .Cast<HtmlNodeType>()
                .Where(x => typeof(HtmlNodeType)
                    .GetMember(x.ToString())
                    .Single()
                    .GetCustomAttributes(typeof(VoidElementAttribute), false)
                    .Any())
                .ToArray();
        }

        public static HtmlNodeType GetTypeFromName(string identifier)
        {
            // SingleOrDefault will default to custom for no match
            return Enum.GetValues(typeof(HtmlNodeType))
                  .Cast<HtmlNodeType>()
                  .Where(n=>n != HtmlNodeType.Comment && n != HtmlNodeType.DocType)
                  .SingleOrDefault(n => string.Equals(n.ToString(), identifier, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}