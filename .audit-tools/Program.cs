using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

class Audit
{
    static void Main(string[] args)
    {
        var files = new[] { "capaVisual", "capaLogica", "capaDatos" }
            .SelectMany(d => Directory.GetFiles(d, "*.cs", SearchOption.AllDirectories))
            .Where(f => !f.EndsWith(".Designer.cs")).Append("Program.cs").ToList();
        var descriptions = args[0] == "comments" ? JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(".audit-comments.json")) : null;
        var rows = new List<object>();
        foreach (var file in files)
        {
            var source = File.ReadAllText(file);
            var root = CSharpSyntaxTree.ParseText(source).GetRoot();
            var changes = new List<TextChange>();
            foreach (var node in root.DescendantNodes().Where(n => n is BaseMethodDeclarationSyntax || n is BaseTypeDeclarationSyntax || n is AccessorDeclarationSyntax a && a.Body != null))
            {
                var owner = node.Ancestors().OfType<TypeDeclarationSyntax>().FirstOrDefault()?.Identifier.Text ?? "";
                var name = node is MethodDeclarationSyntax m ? m.Identifier.Text : node is ConstructorDeclarationSyntax c ? c.Identifier.Text : node is BaseTypeDeclarationSyntax t ? t.Identifier.Text : node is AccessorDeclarationSyntax a ? a.Keyword.Text + "_" + ((PropertyDeclarationSyntax)a.Parent.Parent).Identifier.Text : node.Kind().ToString();
                var key = Path.GetFileName(file) + "|" + owner + "|" + name;
                if (descriptions == null)
                    rows.Add(new { file, owner, name, key, kind = node.Kind().ToString(), body = node.ToString() });
                else
                {
                    if (!descriptions.TryGetValue(key, out var comment)) throw new Exception("Falta comentario: " + key);
                    var leading = node.GetLeadingTrivia();
                    var clean = string.Concat(leading.Where(t => !t.IsKind(SyntaxKind.SingleLineCommentTrivia) && !t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) && !t.IsKind(SyntaxKind.MultiLineCommentTrivia)).Select(t => t.ToFullString()));
                    changes.Add(new TextChange(TextSpan.FromBounds(node.FullSpan.Start, node.SpanStart), clean + "/* " + comment + " */\r\n"));
                }
            }
            if (descriptions != null)
            {
                var updated = SourceText.From(source).WithChanges(changes).ToString();
                File.WriteAllText(file, CSharpSyntaxTree.ParseText(updated).GetRoot().NormalizeWhitespace("    ", "\r\n").ToFullString() + "\r\n");
            }
        }
        if (descriptions == null) File.WriteAllText(".audit-inventory.json", JsonSerializer.Serialize(rows));
        else Console.WriteLine("Comentarios y formato aplicados a " + files.Count + " archivos.");
    }
}
