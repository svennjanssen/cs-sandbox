using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SourceGeneration.Generators {
    [Generator]
    public class MappableGenerator : ISourceGenerator {
        public void Initialize(GeneratorInitializationContext context){
            #if DEBUG
                if (!Debugger.IsAttached)
                    Debugger.Launch();
            #endif
        }

        public void Execute(GeneratorExecutionContext context){
            var syntaxTrees = context.Compilation.SyntaxTrees;

            foreach (var syntaxTree in syntaxTrees)
            {
                var mappableTypeDeclarations = syntaxTree
                .GetRoot().DescendantNodes().OfType<TypeDeclarationSyntax>()
                .Where(x => x.AttributeLists.Any(xx => xx.ToString().StartsWith("[Mappable"))).ToList();

                foreach (var mappableTypeDeclaration in mappableTypeDeclarations)
                {
                    var usingDirectives = syntaxTree.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>();
                    var usingDirectivesAsText = string.Join("\r\n", usingDirectives);
                    var sourceBuilder = new StringBuilder(usingDirectivesAsText);

                    var className = mappableTypeDeclaration.Identifier.ToString();
                    var genClassName = $"{className}Dto";

                    var ignoredProperties = mappableTypeDeclaration.ChildNodes()
                        .Where(x => x is PropertyDeclarationSyntax pdc &&
                            pdc.AttributeLists.Any(xx => xx.ToString().StartsWith("[MappableIgnore]")));

                    var newMappableTypeDeclaration = mappableTypeDeclaration.RemoveNodes(ignoredProperties, SyntaxRemoveOptions.KeepEndOfLine);

                    var splitClass = newMappableTypeDeclaration.ToString().Split(new []{'{'}, 2);

                    sourceBuilder.Append($@"
namespace GeneratedMappers
{{
    public class {genClassName}
    {{                    
                    ");

                    sourceBuilder.Append(splitClass[1].Replace(className, genClassName));
                    sourceBuilder.Append("}");
                    context.AddSource($"MapperGenerator_{genClassName}", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
                }
            }
        }
    }
}