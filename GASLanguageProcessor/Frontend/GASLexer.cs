//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/Daniel/CLionProjects/terminalgraphics/GASLanguageProcessor/GASLanguageProcessor/Frontend/GAS.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public partial class GASLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, T__44=45, 
		T__45=46, T__46=47, T__47=48, T__48=49, IDENTIFIER=50, NUM=51, ALLSTRINGS=52, 
		WS=53;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
		"T__25", "T__26", "T__27", "T__28", "T__29", "T__30", "T__31", "T__32", 
		"T__33", "T__34", "T__35", "T__36", "T__37", "T__38", "T__39", "T__40", 
		"T__41", "T__42", "T__43", "T__44", "T__45", "T__46", "T__47", "T__48", 
		"IDENTIFIER", "NUM", "ALLSTRINGS", "WS"
	};


	public GASLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public GASLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'canvas'", "'('", "','", "')'", "';'", "'='", "'if'", "'{'", "'}'", 
		"'else'", "'while'", "'for'", "'return'", "'.'", "'class'", "'list'", 
		"'<'", "'>'", "'number'", "'bool'", "'point'", "'rectangle'", "'square'", 
		"'circle'", "'polygon'", "'text'", "'colour'", "'group'", "'string'", 
		"'line'", "'T'", "'void'", "'||'", "'&&'", "'=='", "'!='", "'<='", "'>='", 
		"'+'", "'-'", "'*'", "'/'", "'!'", "'['", "']'", "'true'", "'false'", 
		"'null'", "'Group'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, "IDENTIFIER", "NUM", "ALLSTRINGS", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "GAS.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static GASLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,53,349,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,2,43,7,43,2,44,7,44,2,45,7,45,2,46,7,46,2,47,7,47,2,48,7,48,2,49,
		7,49,2,50,7,50,2,51,7,51,2,52,7,52,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,
		1,2,1,2,1,3,1,3,1,4,1,4,1,5,1,5,1,6,1,6,1,6,1,7,1,7,1,8,1,8,1,9,1,9,1,
		9,1,9,1,9,1,10,1,10,1,10,1,10,1,10,1,10,1,11,1,11,1,11,1,11,1,12,1,12,
		1,12,1,12,1,12,1,12,1,12,1,13,1,13,1,14,1,14,1,14,1,14,1,14,1,14,1,15,
		1,15,1,15,1,15,1,15,1,16,1,16,1,17,1,17,1,18,1,18,1,18,1,18,1,18,1,18,
		1,18,1,19,1,19,1,19,1,19,1,19,1,20,1,20,1,20,1,20,1,20,1,20,1,21,1,21,
		1,21,1,21,1,21,1,21,1,21,1,21,1,21,1,21,1,22,1,22,1,22,1,22,1,22,1,22,
		1,22,1,23,1,23,1,23,1,23,1,23,1,23,1,23,1,24,1,24,1,24,1,24,1,24,1,24,
		1,24,1,24,1,25,1,25,1,25,1,25,1,25,1,26,1,26,1,26,1,26,1,26,1,26,1,26,
		1,27,1,27,1,27,1,27,1,27,1,27,1,28,1,28,1,28,1,28,1,28,1,28,1,28,1,29,
		1,29,1,29,1,29,1,29,1,30,1,30,1,31,1,31,1,31,1,31,1,31,1,32,1,32,1,32,
		1,33,1,33,1,33,1,34,1,34,1,34,1,35,1,35,1,35,1,36,1,36,1,36,1,37,1,37,
		1,37,1,38,1,38,1,39,1,39,1,40,1,40,1,41,1,41,1,42,1,42,1,43,1,43,1,44,
		1,44,1,45,1,45,1,45,1,45,1,45,1,46,1,46,1,46,1,46,1,46,1,46,1,47,1,47,
		1,47,1,47,1,47,1,48,1,48,1,48,1,48,1,48,1,48,1,49,1,49,5,49,314,8,49,10,
		49,12,49,317,9,49,1,50,1,50,3,50,321,8,50,1,50,1,50,5,50,325,8,50,10,50,
		12,50,328,9,50,3,50,330,8,50,1,51,1,51,1,51,1,51,5,51,336,8,51,10,51,12,
		51,339,9,51,1,51,1,51,1,52,4,52,344,8,52,11,52,12,52,345,1,52,1,52,0,0,
		53,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,
		14,29,15,31,16,33,17,35,18,37,19,39,20,41,21,43,22,45,23,47,24,49,25,51,
		26,53,27,55,28,57,29,59,30,61,31,63,32,65,33,67,34,69,35,71,36,73,37,75,
		38,77,39,79,40,81,41,83,42,85,43,87,44,89,45,91,46,93,47,95,48,97,49,99,
		50,101,51,103,52,105,53,1,0,7,3,0,65,90,95,95,97,122,4,0,48,57,65,90,95,
		95,97,122,1,0,45,45,1,0,49,57,1,0,48,57,2,0,34,34,92,92,3,0,9,10,13,13,
		32,32,355,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,
		0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,
		1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,
		0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,
		1,0,0,0,0,45,1,0,0,0,0,47,1,0,0,0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,
		0,0,55,1,0,0,0,0,57,1,0,0,0,0,59,1,0,0,0,0,61,1,0,0,0,0,63,1,0,0,0,0,65,
		1,0,0,0,0,67,1,0,0,0,0,69,1,0,0,0,0,71,1,0,0,0,0,73,1,0,0,0,0,75,1,0,0,
		0,0,77,1,0,0,0,0,79,1,0,0,0,0,81,1,0,0,0,0,83,1,0,0,0,0,85,1,0,0,0,0,87,
		1,0,0,0,0,89,1,0,0,0,0,91,1,0,0,0,0,93,1,0,0,0,0,95,1,0,0,0,0,97,1,0,0,
		0,0,99,1,0,0,0,0,101,1,0,0,0,0,103,1,0,0,0,0,105,1,0,0,0,1,107,1,0,0,0,
		3,114,1,0,0,0,5,116,1,0,0,0,7,118,1,0,0,0,9,120,1,0,0,0,11,122,1,0,0,0,
		13,124,1,0,0,0,15,127,1,0,0,0,17,129,1,0,0,0,19,131,1,0,0,0,21,136,1,0,
		0,0,23,142,1,0,0,0,25,146,1,0,0,0,27,153,1,0,0,0,29,155,1,0,0,0,31,161,
		1,0,0,0,33,166,1,0,0,0,35,168,1,0,0,0,37,170,1,0,0,0,39,177,1,0,0,0,41,
		182,1,0,0,0,43,188,1,0,0,0,45,198,1,0,0,0,47,205,1,0,0,0,49,212,1,0,0,
		0,51,220,1,0,0,0,53,225,1,0,0,0,55,232,1,0,0,0,57,238,1,0,0,0,59,245,1,
		0,0,0,61,250,1,0,0,0,63,252,1,0,0,0,65,257,1,0,0,0,67,260,1,0,0,0,69,263,
		1,0,0,0,71,266,1,0,0,0,73,269,1,0,0,0,75,272,1,0,0,0,77,275,1,0,0,0,79,
		277,1,0,0,0,81,279,1,0,0,0,83,281,1,0,0,0,85,283,1,0,0,0,87,285,1,0,0,
		0,89,287,1,0,0,0,91,289,1,0,0,0,93,294,1,0,0,0,95,300,1,0,0,0,97,305,1,
		0,0,0,99,311,1,0,0,0,101,329,1,0,0,0,103,331,1,0,0,0,105,343,1,0,0,0,107,
		108,5,99,0,0,108,109,5,97,0,0,109,110,5,110,0,0,110,111,5,118,0,0,111,
		112,5,97,0,0,112,113,5,115,0,0,113,2,1,0,0,0,114,115,5,40,0,0,115,4,1,
		0,0,0,116,117,5,44,0,0,117,6,1,0,0,0,118,119,5,41,0,0,119,8,1,0,0,0,120,
		121,5,59,0,0,121,10,1,0,0,0,122,123,5,61,0,0,123,12,1,0,0,0,124,125,5,
		105,0,0,125,126,5,102,0,0,126,14,1,0,0,0,127,128,5,123,0,0,128,16,1,0,
		0,0,129,130,5,125,0,0,130,18,1,0,0,0,131,132,5,101,0,0,132,133,5,108,0,
		0,133,134,5,115,0,0,134,135,5,101,0,0,135,20,1,0,0,0,136,137,5,119,0,0,
		137,138,5,104,0,0,138,139,5,105,0,0,139,140,5,108,0,0,140,141,5,101,0,
		0,141,22,1,0,0,0,142,143,5,102,0,0,143,144,5,111,0,0,144,145,5,114,0,0,
		145,24,1,0,0,0,146,147,5,114,0,0,147,148,5,101,0,0,148,149,5,116,0,0,149,
		150,5,117,0,0,150,151,5,114,0,0,151,152,5,110,0,0,152,26,1,0,0,0,153,154,
		5,46,0,0,154,28,1,0,0,0,155,156,5,99,0,0,156,157,5,108,0,0,157,158,5,97,
		0,0,158,159,5,115,0,0,159,160,5,115,0,0,160,30,1,0,0,0,161,162,5,108,0,
		0,162,163,5,105,0,0,163,164,5,115,0,0,164,165,5,116,0,0,165,32,1,0,0,0,
		166,167,5,60,0,0,167,34,1,0,0,0,168,169,5,62,0,0,169,36,1,0,0,0,170,171,
		5,110,0,0,171,172,5,117,0,0,172,173,5,109,0,0,173,174,5,98,0,0,174,175,
		5,101,0,0,175,176,5,114,0,0,176,38,1,0,0,0,177,178,5,98,0,0,178,179,5,
		111,0,0,179,180,5,111,0,0,180,181,5,108,0,0,181,40,1,0,0,0,182,183,5,112,
		0,0,183,184,5,111,0,0,184,185,5,105,0,0,185,186,5,110,0,0,186,187,5,116,
		0,0,187,42,1,0,0,0,188,189,5,114,0,0,189,190,5,101,0,0,190,191,5,99,0,
		0,191,192,5,116,0,0,192,193,5,97,0,0,193,194,5,110,0,0,194,195,5,103,0,
		0,195,196,5,108,0,0,196,197,5,101,0,0,197,44,1,0,0,0,198,199,5,115,0,0,
		199,200,5,113,0,0,200,201,5,117,0,0,201,202,5,97,0,0,202,203,5,114,0,0,
		203,204,5,101,0,0,204,46,1,0,0,0,205,206,5,99,0,0,206,207,5,105,0,0,207,
		208,5,114,0,0,208,209,5,99,0,0,209,210,5,108,0,0,210,211,5,101,0,0,211,
		48,1,0,0,0,212,213,5,112,0,0,213,214,5,111,0,0,214,215,5,108,0,0,215,216,
		5,121,0,0,216,217,5,103,0,0,217,218,5,111,0,0,218,219,5,110,0,0,219,50,
		1,0,0,0,220,221,5,116,0,0,221,222,5,101,0,0,222,223,5,120,0,0,223,224,
		5,116,0,0,224,52,1,0,0,0,225,226,5,99,0,0,226,227,5,111,0,0,227,228,5,
		108,0,0,228,229,5,111,0,0,229,230,5,117,0,0,230,231,5,114,0,0,231,54,1,
		0,0,0,232,233,5,103,0,0,233,234,5,114,0,0,234,235,5,111,0,0,235,236,5,
		117,0,0,236,237,5,112,0,0,237,56,1,0,0,0,238,239,5,115,0,0,239,240,5,116,
		0,0,240,241,5,114,0,0,241,242,5,105,0,0,242,243,5,110,0,0,243,244,5,103,
		0,0,244,58,1,0,0,0,245,246,5,108,0,0,246,247,5,105,0,0,247,248,5,110,0,
		0,248,249,5,101,0,0,249,60,1,0,0,0,250,251,5,84,0,0,251,62,1,0,0,0,252,
		253,5,118,0,0,253,254,5,111,0,0,254,255,5,105,0,0,255,256,5,100,0,0,256,
		64,1,0,0,0,257,258,5,124,0,0,258,259,5,124,0,0,259,66,1,0,0,0,260,261,
		5,38,0,0,261,262,5,38,0,0,262,68,1,0,0,0,263,264,5,61,0,0,264,265,5,61,
		0,0,265,70,1,0,0,0,266,267,5,33,0,0,267,268,5,61,0,0,268,72,1,0,0,0,269,
		270,5,60,0,0,270,271,5,61,0,0,271,74,1,0,0,0,272,273,5,62,0,0,273,274,
		5,61,0,0,274,76,1,0,0,0,275,276,5,43,0,0,276,78,1,0,0,0,277,278,5,45,0,
		0,278,80,1,0,0,0,279,280,5,42,0,0,280,82,1,0,0,0,281,282,5,47,0,0,282,
		84,1,0,0,0,283,284,5,33,0,0,284,86,1,0,0,0,285,286,5,91,0,0,286,88,1,0,
		0,0,287,288,5,93,0,0,288,90,1,0,0,0,289,290,5,116,0,0,290,291,5,114,0,
		0,291,292,5,117,0,0,292,293,5,101,0,0,293,92,1,0,0,0,294,295,5,102,0,0,
		295,296,5,97,0,0,296,297,5,108,0,0,297,298,5,115,0,0,298,299,5,101,0,0,
		299,94,1,0,0,0,300,301,5,110,0,0,301,302,5,117,0,0,302,303,5,108,0,0,303,
		304,5,108,0,0,304,96,1,0,0,0,305,306,5,71,0,0,306,307,5,114,0,0,307,308,
		5,111,0,0,308,309,5,117,0,0,309,310,5,112,0,0,310,98,1,0,0,0,311,315,7,
		0,0,0,312,314,7,1,0,0,313,312,1,0,0,0,314,317,1,0,0,0,315,313,1,0,0,0,
		315,316,1,0,0,0,316,100,1,0,0,0,317,315,1,0,0,0,318,330,5,48,0,0,319,321,
		7,2,0,0,320,319,1,0,0,0,320,321,1,0,0,0,321,322,1,0,0,0,322,326,7,3,0,
		0,323,325,7,4,0,0,324,323,1,0,0,0,325,328,1,0,0,0,326,324,1,0,0,0,326,
		327,1,0,0,0,327,330,1,0,0,0,328,326,1,0,0,0,329,318,1,0,0,0,329,320,1,
		0,0,0,330,102,1,0,0,0,331,337,5,34,0,0,332,336,8,5,0,0,333,334,5,92,0,
		0,334,336,9,0,0,0,335,332,1,0,0,0,335,333,1,0,0,0,336,339,1,0,0,0,337,
		335,1,0,0,0,337,338,1,0,0,0,338,340,1,0,0,0,339,337,1,0,0,0,340,341,5,
		34,0,0,341,104,1,0,0,0,342,344,7,6,0,0,343,342,1,0,0,0,344,345,1,0,0,0,
		345,343,1,0,0,0,345,346,1,0,0,0,346,347,1,0,0,0,347,348,6,52,0,0,348,106,
		1,0,0,0,8,0,315,320,326,329,335,337,345,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
