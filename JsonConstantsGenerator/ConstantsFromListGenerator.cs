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
        var classes = context.SyntaxProvider.ForAttributeWithMetadataName(
            typeof(GenerateConstantsFromList).FullName,
            predicate: (node, _) => true,
            transform: static (context, _) => "NewClassName");
        
        var additionalFiles =
            context.AdditionalTextsProvider
                .Where(file => file.Path.EndsWith(".json"))
                .Select((file, _) => 
                    new { file.Path, Content = file.GetText()?.ToString() ?? string.Empty });
        
        var combinedData = classes.Combine(additionalFiles.Collect());
        
        context.RegisterSourceOutput(combinedData, (sourceContext, source) =>
        {
            var matchingJson = source.Right.FirstOrDefault(f => f.Path.EndsWith("GermanColors.json"));
            if (matchingJson is not null)
            {
                // Build up the source code
                var code = $$"""
                             // <auto-generated/>

                             using System;
                             using System.Collections.Generic;

                             namespace Test;

                             partial class ExampleClass
                             {
                                 
                             }

                             """;
                
                // Add the source code to the compilation.
                sourceContext.AddSource($"ExampleClass.g.cs", SourceText.From(code, Encoding.UTF8));
            }
        });
        
    }
}