using System;
using System.IO;
using Conventional;
using Sprachtml.Conventions.Rules;

namespace Sprachtml.Conventions.Tests.SolutionConventions.Shared
{
    public abstract class AllFilesInFolderBase<TConventionRule>
        where TConventionRule : IConventionRule
    {
        protected virtual TConventionRule GetRuleInstance() => Activator.CreateInstance<TConventionRule>();

        protected abstract string FileMask { get; }

        protected virtual string Subdirectory => $@"Sprachtml.Conventions.Tests\Html\{typeof(TConventionRule).Name}\";

        protected virtual int GetFileCount()
        {
            var path = Path.GetFullPath(Path.Combine(KnownPaths.SolutionRoot, Subdirectory));
            return Directory.GetFiles(path, FileMask, SearchOption.AllDirectories).Length;
        }
    }
}