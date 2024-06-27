using Antlr4.Runtime;
using GASLanguageProcessor.Frontend;
using GASLanguageProcessor.TableType;
using Microsoft.AspNetCore.Mvc;

namespace GASLanguageProcessor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SvgController : ControllerBase
{
    [HttpPut]
    public async Task<ActionResult<List<string>>> GetSvgFromCode([FromBody] CodeRequest request)
    {
        string code = request.Code;
        if (string.IsNullOrEmpty(request.Code))
        {
            return BadRequest(new { message = "The code field is required." });
        }
        var inputStream = CharStreams.fromString(code);
        var lexer = new GASLexer(inputStream);

        var errorListener = new ParserErrorListener();

        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);
        var parseTree = parser.program();
        if (errorListener.Errors.Count > 0) return errorListener.Errors;
        var ast = parseTree.Accept(new ToAstVisitor());
        var combinedAstVisitor = new CombinedAstVisitor();
        ast.Accept(combinedAstVisitor, new TypeEnv());
        combinedAstVisitor.errors.ForEach(Console.Error.WriteLine);
        if (combinedAstVisitor.errors.Count > 0) return combinedAstVisitor.errors;

        var interpreter = new Interpreter();
        var envV = new VarEnv();
        var sto = new Store();
        var envF = new FuncEnv(sto, envV);
        var finalStore =
            interpreter.EvaluateProgram(ast as GASLanguageProcessor.AST.Expressions.Terms.Program, envV, envF, sto);
        interpreter.errors.ForEach(Console.Error.WriteLine);
        if (interpreter.errors.Count > 0) return interpreter.errors;
        var recordEvaluator = new RecordEvaluator();
        var recordStore = recordEvaluator.EvaluateRecords(finalStore);
        var svgGenerator = new SvgGenerator();
        var lines = svgGenerator.GenerateSvg(recordStore);
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
        return lines;
    }

    public class CodeRequest
    {
        public string Code { get; set; }
    }
}
