# Sprachtml - Html parsing with sprache

## Build
[![Build status](https://ci.appveyor.com/api/projects/status/43utb38g5xcrj3fp?svg=true)](https://ci.appveyor.com/project/tristanmenzel/sprachtml)

## Basic usage

```csharp
SprachtmlParser.Parse("<h1>Testing 123"</h1>");
```

## Enumerate nodes

```csharp
var nodes = SprachtmlParser.Parse(someHtml);
var buttons = nodes.TraverseAll().Where(n=>n.NodeType == HtmlNodeType.Button).ToArray();
```

---

# Sprachtml.Conventions - Conventions for your solution's html files

```csharp
[TestFixture]
public class AllHtmlFiles
{
    public static IConventionRule[] GetRules => new IConventionRule[]
    {
        new ButtonMustSpecifyTypeAttribute(),
        new AnchorsMustHaveHrefOrUiSref(),
    };

    public HtmlConvention AgreedConventions => new HtmlConvention(GetRules)
        .WithSolutionSubdirectory(@"MyProject.Web\app\");

    [Test]
    public void ShouldAdhereToAgreedConventions()
    {
        ThisSolution.MustConformTo(AgreedConventions).WithFailureAssertion(Assert.Fail);
    }
}
```
