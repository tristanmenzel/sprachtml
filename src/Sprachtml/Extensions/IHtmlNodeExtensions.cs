using System.Collections.Generic;
using System.Linq;
using Sprachtml.Models;

namespace Sprachtml.Extensions
{
    public static class HtmlNodeExtensions
    {
        public static IEnumerable<IHtmlNode> TraverseAll(this IEnumerable<IHtmlNode> nodes)
        {
            return nodes.SelectMany(n => n.TraverseAll());
        }

        public static IEnumerable<IHtmlNode> TraverseAll(this IHtmlNode node)
        {
            yield return node;
            foreach (var descendant in node.Children.TraverseAll())
                yield return descendant;
        }
    }
}