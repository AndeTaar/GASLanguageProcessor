using Antlr4.Runtime;

namespace GASLanguageProcessor.Frontend;

public class ParserErrorListener : BaseErrorListener
{
    public List<string> Errors { get; } = new List<string>();

    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine,
        string msg, RecognitionException e)
    {
        Errors.Add("Line " + line + ":" + charPositionInLine + " " + msg);
    }
    
    public void StopIfErrors()
    {
        if (Errors.Count == 0) return;
        Console.WriteLine("Parsing error(s) found:");
        foreach (var error in Errors) Console.WriteLine(error);
        Environment.Exit(1);
    }
}