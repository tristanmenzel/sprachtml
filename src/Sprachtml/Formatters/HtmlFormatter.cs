using System;
using System.Collections.Generic;
using System.IO;
using Sprachtml.Models;

namespace Sprachtml.Formatters
{
    [Flags]
    public enum HtmlFormatterOptions : long
    {
        None = 0x00,
        RemoveComments = 0x01
    }

    public class HtmlFormatter : IDisposable
    {
        private readonly StreamWriter _writer;
        private readonly HtmlFormatterOptions _options;

        public HtmlFormatter(StreamWriter writer, HtmlFormatterOptions options = HtmlFormatterOptions.None)
        {
            _writer = writer;
            _options = options;
        }

        public void WriteNodes(ICollection<IHtmlNode> nodes)
        {
            foreach (var node in nodes)
            {
                switch (node.NodeType)
                {
                    case HtmlNodeType.Comment:
                        WriteComment(node as CommentNode);
                        continue;
                    case HtmlNodeType.DocType:
                        WriteDocType(node as DocTypeNode);
                        continue;
                    case HtmlNodeType.Style:
                    case HtmlNodeType.Script:
                        WriteScriptOrStyle(node.NodeType.ToString().ToLower(), node);

                        continue;
                    case HtmlNodeType.Text:
                        WriteTextNode(node as TextNode);
                        continue;
                    default:
                        WriteHtmlNode(node as HtmlNode);
                        continue;



                }
            }
        }

        private void WriteScriptOrStyle(string tag, IHtmlNode node)
        {
            _writer.Write($"<{tag}");
            WriteAttributes(node.Attributes);
            _writer.Write('>');
            _writer.Write(node.Contents);
            _writer.Write($"</{tag}>");
        }

        private void WriteTextNode(TextNode node)
        {
            _writer.Write(node.Contents);
        }

        private void WriteAttributes(HtmlAttribute[] attributes)
        {
            foreach (var attr in attributes)
            {
                _writer.Write(' ');
                _writer.Write(attr.Name);
                if (attr.Binary)
                    continue;
                _writer.Write('=');
                WriteQuotedString(attr.Value);
            }
        }

        private void WriteHtmlNode(HtmlNode node)
        {
            var tag = node.NodeType.ToString().ToLower();

            _writer.Write('<');
            _writer.Write(tag);
            WriteAttributes(node.Attributes);
            if (node.TagStyle == TagStyle.SelfClosing)
                _writer.Write('/');
            _writer.Write('>');
            if (node.TagStyle != TagStyle.Closed)
                return;
            WriteNodes(node.Children);
            _writer.Write($"</{tag}>");
        }

        private void WriteDocType(DocTypeNode node)
        {
            _writer.Write("<!DOCTYPE");
            foreach (var prop in node.Properties)
            {
                _writer.Write(' ');
                WriteQuotedString(prop);
            }
            _writer.Write('>');
        }

        private void WriteComment(CommentNode node)
        {
            if (_options.HasFlag(HtmlFormatterOptions.RemoveComments))
                return;
            _writer.Write("<!--");
            _writer.Write(node.Contents);
            _writer.Write("-->");
        }

        private void WriteQuotedString(QuotedString quotedString)
        {
            var quote = GetQuote(quotedString.QuoteType);
            _writer.Write($"{quote}{quotedString.Text}{quote}");

        }

        private string GetQuote(QuoteType quoteType)
        {
            switch (quoteType)
            {
                case QuoteType.Double:
                    return "\"";
                case QuoteType.Single:
                    return "'";
                default:
                    return "";
            }
        }

        public static string WriteAsString(ICollection<IHtmlNode> nodes, HtmlFormatterOptions options = HtmlFormatterOptions.None)
        {
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms))
            using (var formatter = new HtmlFormatter(writer, options))
            using (var sr = new StreamReader(ms))
            {
                formatter.WriteNodes(nodes);
                writer.Flush();

                ms.Position = 0;
                return sr.ReadToEnd();
            }
        }


        #region IDisposable
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _writer?.Dispose();
            }
            _disposed = true;
        }
        #endregion
    }
}