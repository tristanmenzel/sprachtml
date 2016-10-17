using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Conventional.Conventions.Solution;
using Sprachtml.Conventions.Rules;
using Sprachtml.Exceptions;

namespace Sprachtml.Conventions
{
    public class HtmlConvention : SolutionConventionSpecification
    {
        private readonly IConventionRule[] _rules;
        private readonly string _searchPattern;
        private readonly string[] _excludedFiles;

        public HtmlConvention(IConventionRule[] rules, string searchPattern = "*.html", string[] excludedFiles = null)
        {
            _rules = rules;
            _searchPattern = searchPattern;
            _excludedFiles = excludedFiles ?? new string[0];
        }

        private IEnumerable<FileResult> Test(string directory)
        {
            foreach (var filePath in Directory.GetFiles(directory, _searchPattern, SearchOption.AllDirectories)
                .Where(p => !_excludedFiles.Any(x => p.EndsWith(x, StringComparison.InvariantCultureIgnoreCase))))
            {
                var fileContents = File.ReadAllText(filePath);
                RuleResult[] results = new RuleResult[0];
                SprachtmlParseException exception = null;

                try
                {
                    var parsed = SprachtmlParser.Parse(fileContents);
                    results = _rules
                        .Select(r => r.Test(parsed))
                        .Where(r => !r.Passed)
                        .ToArray();
                }
                catch (SprachtmlParseException ex)
                {
                    exception = ex;
                }
                if (exception != null)
                    yield return new FileResult(filePath, exception);
                else if (results.Any())
                    yield return new FileResult(filePath, results);

            }
        }

        private class FileResult
        {
            public SprachtmlParseException Ex { get; }
            public string Path { get; }
            public RuleResult[] RuleResults { get; }

            public FileResult(string path, RuleResult[] ruleResults)
            {
                Path = path;
                RuleResults = ruleResults;
            }

            public FileResult(string path, SprachtmlParseException ex)
            {
                Ex = ex;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                if (RuleResults != null)
                {
                    sb.AppendLine(Path);
                    foreach (var ruleResult in RuleResults)
                    {
                        sb.AppendLine($"\t{ruleResult.Message}");
                        foreach (var offendingNode in ruleResult.OffendingNodes)
                        {
                            sb.AppendLine($"\t\tNode: {offendingNode.NodeLocation}");
                        }
                    }
                }
                return sb.ToString();
            }
        }

        public override Conventional.ConventionResult IsSatisfiedBy(string solutionRoot)
        {
            var failedFiles = Test(solutionRoot).ToArray();

            if (failedFiles.Any())
            {
                return Conventional.ConventionResult.NotSatisfied("Solution convention", failedFiles.Aggregate(string.Empty, (existing, @new) => existing + Environment.NewLine + @new.ToString()));
            }
            return Conventional.ConventionResult.Satisfied("Solution convention");
        }

        protected override string FailureMessage => "TODO";
    }

    public class ConventionResult
    {

    }
}