using System.Collections.Immutable;
using System.Linq;
using System.Text;
using JsonConstantsGenerator.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;


namespace JsonConstantsGenerator;

/// <summary>
///
/// </summary>
[Generator]
public class ConstantsFromListGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var additionalFiles =
            context.AdditionalTextsProvider
                .Where(file => file.Path.EndsWith(".json"))
                .Select((file, _) => 
                    new { file.Path, Content = file.GetText()?.ToString() ?? string.Empty });
        
        
        context.RegisterSourceOutput(additionalFiles, (context, source) => { });
        
    }
}