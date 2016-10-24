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
        public IConventionRule[] Rules { get; }
        public string SearchPattern { get; }
        public string[] ExcludedFiles { get; }


        public string SolutionSubdirectory { get; set; }

        public HtmlConvention(IConventionRule[] rules, string searchPattern = "*.html", string[] excludedFiles = null)
        {
            Rules = rules;
            SearchPattern = searchPattern;
            ExcludedFiles = excludedFiles ?? new string[0];
        }

        private string GetDirectory(string solutionDirectory)
        {
            if(SolutionSubdirectory == null)
                return solutionDirectory;
            return Path.GetFullPath(Path.Combine(solutionDirectory, SolutionSubdirectory));
        }

        private IEnumerable<FileResult> Test(string solutionDirectory)
        {
            var dir = GetDirectory(solutionDirectory);

            foreach (var filePath in Directory.GetFiles(dir, SearchPattern, SearchOption.AllDirectories)
                .Where(p => !ExcludedFiles.Any(x => p.EndsWith(x, StringComparison.InvariantCultureIgnoreCase))))
            {
                var fileContents = File.ReadAllText(filePath);
                RuleResult[] results = new RuleResult[0];
                SprachtmlParseException exception = null;

                try
                {
                    var parsed = SprachtmlParser.Parse(fileContents);
                    results = Rules
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
                Path = path;
                Ex = ex;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine(Path);

                if (RuleResults != null)
                {
                    foreach (var ruleResult in RuleResults)
                    {
                        sb.AppendLine($"\t{ruleResult.Message}");
                        foreach (var offendingNode in ruleResult.OffendingNodes)
                        {
                            sb.AppendLine($"\t\tLoc: {offendingNode.NodeLocation}");
                        }
                    }
                }
                if (Ex != null)
                {
                    sb.AppendLine($"\t{Ex.Message}");
                    sb.AppendLine($"\t\tLoc: {Ex.Location}");
                    sb.AppendLine($"\t\tXPath: {Ex.XPath}");
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