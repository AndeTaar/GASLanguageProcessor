//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from GAS.g4 by ANTLR 4.13.1

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
		T__45=46, T__46=47, T__47=48, T__48=49, T__49=50, T__50=51, T__51=52, 
		T__52=53, T__53=54, T__54=55, T__55=56, T__56=57, T__57=58, T__58=59, 
		COMMENT=60, IDENTIFIER=61, NUM=62, ALLSTRINGS=63, WS=64;
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
		"T__49", "T__50", "T__51", "T__52", "T__53", "T__54", "T__55", "T__56", 
		"T__57", "T__58", "COMMENT", "IDENTIFIER", "NUM", "ALLSTRINGS", "WS"
	};


	public GASLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public GASLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'canvas'", "'('", "','", "')'", "';'", "'='", "'+='", "'-='", "'*='", 
		"'/='", "':'", "'Struct'", "'{'", "'}'", "'++'", "'--'", "'if'", "'else'", 
		"'while'", "'for'", "'return'", "'num'", "'bool'", "'point'", "'rectangle'", 
		"'square'", "'circle'", "'polygon'", "'text'", "'col'", "'string'", "'line'", 
		"'void'", "'segLine'", "'ellipse'", "'arrow'", "'struct'", "'list'", "'<'", 
		"'>'", "'group'", "'||'", "'&&'", "'=='", "'!='", "'<='", "'>='", "'+'", 
		"'-'", "'*'", "'/'", "'%'", "'!'", "'.'", "'true'", "'false'", "'null'", 
		"'List'", "'Group'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		"COMMENT", "IDENTIFIER", "NUM", "ALLSTRINGS", "WS"
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
		4,0,64,437,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,2,43,7,43,2,44,7,44,2,45,7,45,2,46,7,46,2,47,7,47,2,48,7,48,2,49,
		7,49,2,50,7,50,2,51,7,51,2,52,7,52,2,53,7,53,2,54,7,54,2,55,7,55,2,56,
		7,56,2,57,7,57,2,58,7,58,2,59,7,59,2,60,7,60,2,61,7,61,2,62,7,62,2,63,
		7,63,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,2,1,2,1,3,1,3,1,4,1,4,1,5,1,
		5,1,6,1,6,1,6,1,7,1,7,1,7,1,8,1,8,1,8,1,9,1,9,1,9,1,10,1,10,1,11,1,11,
		1,11,1,11,1,11,1,11,1,11,1,12,1,12,1,13,1,13,1,14,1,14,1,14,1,15,1,15,
		1,15,1,16,1,16,1,16,1,17,1,17,1,17,1,17,1,17,1,18,1,18,1,18,1,18,1,18,
		1,18,1,19,1,19,1,19,1,19,1,20,1,20,1,20,1,20,1,20,1,20,1,20,1,21,1,21,
		1,21,1,21,1,22,1,22,1,22,1,22,1,22,1,23,1,23,1,23,1,23,1,23,1,23,1,24,
		1,24,1,24,1,24,1,24,1,24,1,24,1,24,1,24,1,24,1,25,1,25,1,25,1,25,1,25,
		1,25,1,25,1,26,1,26,1,26,1,26,1,26,1,26,1,26,1,27,1,27,1,27,1,27,1,27,
		1,27,1,27,1,27,1,28,1,28,1,28,1,28,1,28,1,29,1,29,1,29,1,29,1,30,1,30,
		1,30,1,30,1,30,1,30,1,30,1,31,1,31,1,31,1,31,1,31,1,32,1,32,1,32,1,32,
		1,32,1,33,1,33,1,33,1,33,1,33,1,33,1,33,1,33,1,34,1,34,1,34,1,34,1,34,
		1,34,1,34,1,34,1,35,1,35,1,35,1,35,1,35,1,35,1,36,1,36,1,36,1,36,1,36,
		1,36,1,36,1,37,1,37,1,37,1,37,1,37,1,38,1,38,1,39,1,39,1,40,1,40,1,40,
		1,40,1,40,1,40,1,41,1,41,1,41,1,42,1,42,1,42,1,43,1,43,1,43,1,44,1,44,
		1,44,1,45,1,45,1,45,1,46,1,46,1,46,1,47,1,47,1,48,1,48,1,49,1,49,1,50,
		1,50,1,51,1,51,1,52,1,52,1,53,1,53,1,54,1,54,1,54,1,54,1,54,1,55,1,55,
		1,55,1,55,1,55,1,55,1,56,1,56,1,56,1,56,1,56,1,57,1,57,1,57,1,57,1,57,
		1,58,1,58,1,58,1,58,1,58,1,58,1,59,1,59,1,59,1,59,5,59,383,8,59,10,59,
		12,59,386,9,59,1,59,1,59,1,59,1,59,1,59,1,60,1,60,5,60,395,8,60,10,60,
		12,60,398,9,60,1,61,1,61,5,61,402,8,61,10,61,12,61,405,9,61,1,61,1,61,
		4,61,409,8,61,11,61,12,61,410,1,61,4,61,414,8,61,11,61,12,61,415,3,61,
		418,8,61,1,62,1,62,1,62,1,62,5,62,424,8,62,10,62,12,62,427,9,62,1,62,1,
		62,1,63,4,63,432,8,63,11,63,12,63,433,1,63,1,63,1,384,0,64,1,1,3,2,5,3,
		7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,14,29,15,31,16,
		33,17,35,18,37,19,39,20,41,21,43,22,45,23,47,24,49,25,51,26,53,27,55,28,
		57,29,59,30,61,31,63,32,65,33,67,34,69,35,71,36,73,37,75,38,77,39,79,40,
		81,41,83,42,85,43,87,44,89,45,91,46,93,47,95,48,97,49,99,50,101,51,103,
		52,105,53,107,54,109,55,111,56,113,57,115,58,117,59,119,60,121,61,123,
		62,125,63,127,64,1,0,5,3,0,65,90,95,95,97,122,4,0,48,57,65,90,95,95,97,
		122,1,0,48,57,2,0,34,34,92,92,3,0,9,10,13,13,32,32,446,0,1,1,0,0,0,0,3,
		1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,
		0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,
		1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,
		0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,0,45,1,0,0,0,0,47,
		1,0,0,0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,
		0,0,59,1,0,0,0,0,61,1,0,0,0,0,63,1,0,0,0,0,65,1,0,0,0,0,67,1,0,0,0,0,69,
		1,0,0,0,0,71,1,0,0,0,0,73,1,0,0,0,0,75,1,0,0,0,0,77,1,0,0,0,0,79,1,0,0,
		0,0,81,1,0,0,0,0,83,1,0,0,0,0,85,1,0,0,0,0,87,1,0,0,0,0,89,1,0,0,0,0,91,
		1,0,0,0,0,93,1,0,0,0,0,95,1,0,0,0,0,97,1,0,0,0,0,99,1,0,0,0,0,101,1,0,
		0,0,0,103,1,0,0,0,0,105,1,0,0,0,0,107,1,0,0,0,0,109,1,0,0,0,0,111,1,0,
		0,0,0,113,1,0,0,0,0,115,1,0,0,0,0,117,1,0,0,0,0,119,1,0,0,0,0,121,1,0,
		0,0,0,123,1,0,0,0,0,125,1,0,0,0,0,127,1,0,0,0,1,129,1,0,0,0,3,136,1,0,
		0,0,5,138,1,0,0,0,7,140,1,0,0,0,9,142,1,0,0,0,11,144,1,0,0,0,13,146,1,
		0,0,0,15,149,1,0,0,0,17,152,1,0,0,0,19,155,1,0,0,0,21,158,1,0,0,0,23,160,
		1,0,0,0,25,167,1,0,0,0,27,169,1,0,0,0,29,171,1,0,0,0,31,174,1,0,0,0,33,
		177,1,0,0,0,35,180,1,0,0,0,37,185,1,0,0,0,39,191,1,0,0,0,41,195,1,0,0,
		0,43,202,1,0,0,0,45,206,1,0,0,0,47,211,1,0,0,0,49,217,1,0,0,0,51,227,1,
		0,0,0,53,234,1,0,0,0,55,241,1,0,0,0,57,249,1,0,0,0,59,254,1,0,0,0,61,258,
		1,0,0,0,63,265,1,0,0,0,65,270,1,0,0,0,67,275,1,0,0,0,69,283,1,0,0,0,71,
		291,1,0,0,0,73,297,1,0,0,0,75,304,1,0,0,0,77,309,1,0,0,0,79,311,1,0,0,
		0,81,313,1,0,0,0,83,319,1,0,0,0,85,322,1,0,0,0,87,325,1,0,0,0,89,328,1,
		0,0,0,91,331,1,0,0,0,93,334,1,0,0,0,95,337,1,0,0,0,97,339,1,0,0,0,99,341,
		1,0,0,0,101,343,1,0,0,0,103,345,1,0,0,0,105,347,1,0,0,0,107,349,1,0,0,
		0,109,351,1,0,0,0,111,356,1,0,0,0,113,362,1,0,0,0,115,367,1,0,0,0,117,
		372,1,0,0,0,119,378,1,0,0,0,121,392,1,0,0,0,123,417,1,0,0,0,125,419,1,
		0,0,0,127,431,1,0,0,0,129,130,5,99,0,0,130,131,5,97,0,0,131,132,5,110,
		0,0,132,133,5,118,0,0,133,134,5,97,0,0,134,135,5,115,0,0,135,2,1,0,0,0,
		136,137,5,40,0,0,137,4,1,0,0,0,138,139,5,44,0,0,139,6,1,0,0,0,140,141,
		5,41,0,0,141,8,1,0,0,0,142,143,5,59,0,0,143,10,1,0,0,0,144,145,5,61,0,
		0,145,12,1,0,0,0,146,147,5,43,0,0,147,148,5,61,0,0,148,14,1,0,0,0,149,
		150,5,45,0,0,150,151,5,61,0,0,151,16,1,0,0,0,152,153,5,42,0,0,153,154,
		5,61,0,0,154,18,1,0,0,0,155,156,5,47,0,0,156,157,5,61,0,0,157,20,1,0,0,
		0,158,159,5,58,0,0,159,22,1,0,0,0,160,161,5,83,0,0,161,162,5,116,0,0,162,
		163,5,114,0,0,163,164,5,117,0,0,164,165,5,99,0,0,165,166,5,116,0,0,166,
		24,1,0,0,0,167,168,5,123,0,0,168,26,1,0,0,0,169,170,5,125,0,0,170,28,1,
		0,0,0,171,172,5,43,0,0,172,173,5,43,0,0,173,30,1,0,0,0,174,175,5,45,0,
		0,175,176,5,45,0,0,176,32,1,0,0,0,177,178,5,105,0,0,178,179,5,102,0,0,
		179,34,1,0,0,0,180,181,5,101,0,0,181,182,5,108,0,0,182,183,5,115,0,0,183,
		184,5,101,0,0,184,36,1,0,0,0,185,186,5,119,0,0,186,187,5,104,0,0,187,188,
		5,105,0,0,188,189,5,108,0,0,189,190,5,101,0,0,190,38,1,0,0,0,191,192,5,
		102,0,0,192,193,5,111,0,0,193,194,5,114,0,0,194,40,1,0,0,0,195,196,5,114,
		0,0,196,197,5,101,0,0,197,198,5,116,0,0,198,199,5,117,0,0,199,200,5,114,
		0,0,200,201,5,110,0,0,201,42,1,0,0,0,202,203,5,110,0,0,203,204,5,117,0,
		0,204,205,5,109,0,0,205,44,1,0,0,0,206,207,5,98,0,0,207,208,5,111,0,0,
		208,209,5,111,0,0,209,210,5,108,0,0,210,46,1,0,0,0,211,212,5,112,0,0,212,
		213,5,111,0,0,213,214,5,105,0,0,214,215,5,110,0,0,215,216,5,116,0,0,216,
		48,1,0,0,0,217,218,5,114,0,0,218,219,5,101,0,0,219,220,5,99,0,0,220,221,
		5,116,0,0,221,222,5,97,0,0,222,223,5,110,0,0,223,224,5,103,0,0,224,225,
		5,108,0,0,225,226,5,101,0,0,226,50,1,0,0,0,227,228,5,115,0,0,228,229,5,
		113,0,0,229,230,5,117,0,0,230,231,5,97,0,0,231,232,5,114,0,0,232,233,5,
		101,0,0,233,52,1,0,0,0,234,235,5,99,0,0,235,236,5,105,0,0,236,237,5,114,
		0,0,237,238,5,99,0,0,238,239,5,108,0,0,239,240,5,101,0,0,240,54,1,0,0,
		0,241,242,5,112,0,0,242,243,5,111,0,0,243,244,5,108,0,0,244,245,5,121,
		0,0,245,246,5,103,0,0,246,247,5,111,0,0,247,248,5,110,0,0,248,56,1,0,0,
		0,249,250,5,116,0,0,250,251,5,101,0,0,251,252,5,120,0,0,252,253,5,116,
		0,0,253,58,1,0,0,0,254,255,5,99,0,0,255,256,5,111,0,0,256,257,5,108,0,
		0,257,60,1,0,0,0,258,259,5,115,0,0,259,260,5,116,0,0,260,261,5,114,0,0,
		261,262,5,105,0,0,262,263,5,110,0,0,263,264,5,103,0,0,264,62,1,0,0,0,265,
		266,5,108,0,0,266,267,5,105,0,0,267,268,5,110,0,0,268,269,5,101,0,0,269,
		64,1,0,0,0,270,271,5,118,0,0,271,272,5,111,0,0,272,273,5,105,0,0,273,274,
		5,100,0,0,274,66,1,0,0,0,275,276,5,115,0,0,276,277,5,101,0,0,277,278,5,
		103,0,0,278,279,5,76,0,0,279,280,5,105,0,0,280,281,5,110,0,0,281,282,5,
		101,0,0,282,68,1,0,0,0,283,284,5,101,0,0,284,285,5,108,0,0,285,286,5,108,
		0,0,286,287,5,105,0,0,287,288,5,112,0,0,288,289,5,115,0,0,289,290,5,101,
		0,0,290,70,1,0,0,0,291,292,5,97,0,0,292,293,5,114,0,0,293,294,5,114,0,
		0,294,295,5,111,0,0,295,296,5,119,0,0,296,72,1,0,0,0,297,298,5,115,0,0,
		298,299,5,116,0,0,299,300,5,114,0,0,300,301,5,117,0,0,301,302,5,99,0,0,
		302,303,5,116,0,0,303,74,1,0,0,0,304,305,5,108,0,0,305,306,5,105,0,0,306,
		307,5,115,0,0,307,308,5,116,0,0,308,76,1,0,0,0,309,310,5,60,0,0,310,78,
		1,0,0,0,311,312,5,62,0,0,312,80,1,0,0,0,313,314,5,103,0,0,314,315,5,114,
		0,0,315,316,5,111,0,0,316,317,5,117,0,0,317,318,5,112,0,0,318,82,1,0,0,
		0,319,320,5,124,0,0,320,321,5,124,0,0,321,84,1,0,0,0,322,323,5,38,0,0,
		323,324,5,38,0,0,324,86,1,0,0,0,325,326,5,61,0,0,326,327,5,61,0,0,327,
		88,1,0,0,0,328,329,5,33,0,0,329,330,5,61,0,0,330,90,1,0,0,0,331,332,5,
		60,0,0,332,333,5,61,0,0,333,92,1,0,0,0,334,335,5,62,0,0,335,336,5,61,0,
		0,336,94,1,0,0,0,337,338,5,43,0,0,338,96,1,0,0,0,339,340,5,45,0,0,340,
		98,1,0,0,0,341,342,5,42,0,0,342,100,1,0,0,0,343,344,5,47,0,0,344,102,1,
		0,0,0,345,346,5,37,0,0,346,104,1,0,0,0,347,348,5,33,0,0,348,106,1,0,0,
		0,349,350,5,46,0,0,350,108,1,0,0,0,351,352,5,116,0,0,352,353,5,114,0,0,
		353,354,5,117,0,0,354,355,5,101,0,0,355,110,1,0,0,0,356,357,5,102,0,0,
		357,358,5,97,0,0,358,359,5,108,0,0,359,360,5,115,0,0,360,361,5,101,0,0,
		361,112,1,0,0,0,362,363,5,110,0,0,363,364,5,117,0,0,364,365,5,108,0,0,
		365,366,5,108,0,0,366,114,1,0,0,0,367,368,5,76,0,0,368,369,5,105,0,0,369,
		370,5,115,0,0,370,371,5,116,0,0,371,116,1,0,0,0,372,373,5,71,0,0,373,374,
		5,114,0,0,374,375,5,111,0,0,375,376,5,117,0,0,376,377,5,112,0,0,377,118,
		1,0,0,0,378,379,5,47,0,0,379,380,5,42,0,0,380,384,1,0,0,0,381,383,9,0,
		0,0,382,381,1,0,0,0,383,386,1,0,0,0,384,385,1,0,0,0,384,382,1,0,0,0,385,
		387,1,0,0,0,386,384,1,0,0,0,387,388,5,42,0,0,388,389,5,47,0,0,389,390,
		1,0,0,0,390,391,6,59,0,0,391,120,1,0,0,0,392,396,7,0,0,0,393,395,7,1,0,
		0,394,393,1,0,0,0,395,398,1,0,0,0,396,394,1,0,0,0,396,397,1,0,0,0,397,
		122,1,0,0,0,398,396,1,0,0,0,399,418,5,48,0,0,400,402,7,2,0,0,401,400,1,
		0,0,0,402,405,1,0,0,0,403,401,1,0,0,0,403,404,1,0,0,0,404,406,1,0,0,0,
		405,403,1,0,0,0,406,408,5,46,0,0,407,409,7,2,0,0,408,407,1,0,0,0,409,410,
		1,0,0,0,410,408,1,0,0,0,410,411,1,0,0,0,411,418,1,0,0,0,412,414,7,2,0,
		0,413,412,1,0,0,0,414,415,1,0,0,0,415,413,1,0,0,0,415,416,1,0,0,0,416,
		418,1,0,0,0,417,399,1,0,0,0,417,403,1,0,0,0,417,413,1,0,0,0,418,124,1,
		0,0,0,419,425,5,34,0,0,420,424,8,3,0,0,421,422,5,92,0,0,422,424,9,0,0,
		0,423,420,1,0,0,0,423,421,1,0,0,0,424,427,1,0,0,0,425,423,1,0,0,0,425,
		426,1,0,0,0,426,428,1,0,0,0,427,425,1,0,0,0,428,429,5,34,0,0,429,126,1,
		0,0,0,430,432,7,4,0,0,431,430,1,0,0,0,432,433,1,0,0,0,433,431,1,0,0,0,
		433,434,1,0,0,0,434,435,1,0,0,0,435,436,6,63,0,0,436,128,1,0,0,0,10,0,
		384,396,403,410,415,417,423,425,433,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
