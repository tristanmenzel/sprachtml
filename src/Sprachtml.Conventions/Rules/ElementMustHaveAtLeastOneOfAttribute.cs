using System;
using System.Collections.Generic;
using System.Linq;
using Sprachtml.Conventions.Models;
using Sprachtml.Extensions;
using Sprachtml.Helpers;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Rules
{
    public class ElementMustHaveAtLeastOneOfAttribute : ConventionRuleBase
    {
        private readonly HtmlNodeType _nodeType;
        private readonly string _tagName;
        private readonly string[] _attributes;

        public ElementMustHaveAtLeastOneOfAttribute(HtmlNodeType nodeType, string[] attributes)
        {
            _nodeType = nodeType;
            _attributes = attributes;
        }

        public ElementMustHaveAtLeastOneOfAttribute(string tagName, string[] attributes)
        {
            _nodeType = TagHelper.GetTypeFromName(tagName);
            if (_nodeType == HtmlNodeType.Custom)
                _tagName = tagName;
            _attributes = attributes;
        }

        protected override bool IsOffending(IHtmlNode node)
        {
            if (node.NodeType != _nodeType)
                return false;

            if (_tagName != null &&
                !string.Equals((node as HtmlNode)?.TagName, _tagName, StringComparison.InvariantCultureIgnoreCase))
                return false;

            return !node.Attributes
                .Any(a => _attributes
                    .Any(rq => rq.Equals(a.Name, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}