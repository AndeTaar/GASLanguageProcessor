//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /home/chris/desktop/coding/p4/GASLanguageProcessor/GASLanguageProcessor/Frontend/GAS.g4 by ANTLR 4.13.1

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
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, IDENTIFIER=45, 
		NUM=46, ALLSTRINGS=47, WS=48;
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
		"T__41", "T__42", "T__43", "IDENTIFIER", "NUM", "ALLSTRINGS", "WS"
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
		"'else'", "'while'", "'return'", "'.'", "'class'", "'list'", "'<'", "'>'", 
		"'['", "']'", "'number'", "'bool'", "'point'", "'rectangle'", "'square'", 
		"'circle'", "'polygon'", "'text'", "'colour'", "'group'", "'string'", 
		"'line'", "'T'", "'||'", "'=='", "'!='", "'<='", "'>='", "'+'", "'-'", 
		"'*'", "'!'", "'true'", "'false'", "'Group'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, "IDENTIFIER", "NUM", 
		"ALLSTRINGS", "WS"
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
		4,0,48,320,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,2,43,7,43,2,44,7,44,2,45,7,45,2,46,7,46,2,47,7,47,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,1,1,1,1,1,2,1,2,1,3,1,3,1,4,1,4,1,5,1,5,1,6,1,6,1,6,1,7,1,
		7,1,8,1,8,1,9,1,9,1,9,1,9,1,9,1,10,1,10,1,10,1,10,1,10,1,10,1,11,1,11,
		1,11,1,11,1,11,1,11,1,11,1,12,1,12,1,13,1,13,1,13,1,13,1,13,1,13,1,14,
		1,14,1,14,1,14,1,14,1,15,1,15,1,16,1,16,1,17,1,17,1,18,1,18,1,19,1,19,
		1,19,1,19,1,19,1,19,1,19,1,20,1,20,1,20,1,20,1,20,1,21,1,21,1,21,1,21,
		1,21,1,21,1,22,1,22,1,22,1,22,1,22,1,22,1,22,1,22,1,22,1,22,1,23,1,23,
		1,23,1,23,1,23,1,23,1,23,1,24,1,24,1,24,1,24,1,24,1,24,1,24,1,25,1,25,
		1,25,1,25,1,25,1,25,1,25,1,25,1,26,1,26,1,26,1,26,1,26,1,27,1,27,1,27,
		1,27,1,27,1,27,1,27,1,28,1,28,1,28,1,28,1,28,1,28,1,29,1,29,1,29,1,29,
		1,29,1,29,1,29,1,30,1,30,1,30,1,30,1,30,1,31,1,31,1,32,1,32,1,32,1,33,
		1,33,1,33,1,34,1,34,1,34,1,35,1,35,1,35,1,36,1,36,1,36,1,37,1,37,1,38,
		1,38,1,39,1,39,1,40,1,40,1,41,1,41,1,41,1,41,1,41,1,42,1,42,1,42,1,42,
		1,42,1,42,1,43,1,43,1,43,1,43,1,43,1,43,1,44,1,44,5,44,285,8,44,10,44,
		12,44,288,9,44,1,45,1,45,3,45,292,8,45,1,45,1,45,5,45,296,8,45,10,45,12,
		45,299,9,45,3,45,301,8,45,1,46,1,46,1,46,1,46,5,46,307,8,46,10,46,12,46,
		310,9,46,1,46,1,46,1,47,4,47,315,8,47,11,47,12,47,316,1,47,1,47,0,0,48,
		1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,14,
		29,15,31,16,33,17,35,18,37,19,39,20,41,21,43,22,45,23,47,24,49,25,51,26,
		53,27,55,28,57,29,59,30,61,31,63,32,65,33,67,34,69,35,71,36,73,37,75,38,
		77,39,79,40,81,41,83,42,85,43,87,44,89,45,91,46,93,47,95,48,1,0,7,3,0,
		65,90,95,95,97,122,4,0,48,57,65,90,95,95,97,122,1,0,45,45,1,0,49,57,1,
		0,48,57,2,0,34,34,92,92,3,0,9,10,13,13,32,32,326,0,1,1,0,0,0,0,3,1,0,0,
		0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,
		0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,
		0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,
		1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,0,45,1,0,0,0,0,47,1,0,0,
		0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,0,0,59,
		1,0,0,0,0,61,1,0,0,0,0,63,1,0,0,0,0,65,1,0,0,0,0,67,1,0,0,0,0,69,1,0,0,
		0,0,71,1,0,0,0,0,73,1,0,0,0,0,75,1,0,0,0,0,77,1,0,0,0,0,79,1,0,0,0,0,81,
		1,0,0,0,0,83,1,0,0,0,0,85,1,0,0,0,0,87,1,0,0,0,0,89,1,0,0,0,0,91,1,0,0,
		0,0,93,1,0,0,0,0,95,1,0,0,0,1,97,1,0,0,0,3,104,1,0,0,0,5,106,1,0,0,0,7,
		108,1,0,0,0,9,110,1,0,0,0,11,112,1,0,0,0,13,114,1,0,0,0,15,117,1,0,0,0,
		17,119,1,0,0,0,19,121,1,0,0,0,21,126,1,0,0,0,23,132,1,0,0,0,25,139,1,0,
		0,0,27,141,1,0,0,0,29,147,1,0,0,0,31,152,1,0,0,0,33,154,1,0,0,0,35,156,
		1,0,0,0,37,158,1,0,0,0,39,160,1,0,0,0,41,167,1,0,0,0,43,172,1,0,0,0,45,
		178,1,0,0,0,47,188,1,0,0,0,49,195,1,0,0,0,51,202,1,0,0,0,53,210,1,0,0,
		0,55,215,1,0,0,0,57,222,1,0,0,0,59,228,1,0,0,0,61,235,1,0,0,0,63,240,1,
		0,0,0,65,242,1,0,0,0,67,245,1,0,0,0,69,248,1,0,0,0,71,251,1,0,0,0,73,254,
		1,0,0,0,75,257,1,0,0,0,77,259,1,0,0,0,79,261,1,0,0,0,81,263,1,0,0,0,83,
		265,1,0,0,0,85,270,1,0,0,0,87,276,1,0,0,0,89,282,1,0,0,0,91,300,1,0,0,
		0,93,302,1,0,0,0,95,314,1,0,0,0,97,98,5,99,0,0,98,99,5,97,0,0,99,100,5,
		110,0,0,100,101,5,118,0,0,101,102,5,97,0,0,102,103,5,115,0,0,103,2,1,0,
		0,0,104,105,5,40,0,0,105,4,1,0,0,0,106,107,5,44,0,0,107,6,1,0,0,0,108,
		109,5,41,0,0,109,8,1,0,0,0,110,111,5,59,0,0,111,10,1,0,0,0,112,113,5,61,
		0,0,113,12,1,0,0,0,114,115,5,105,0,0,115,116,5,102,0,0,116,14,1,0,0,0,
		117,118,5,123,0,0,118,16,1,0,0,0,119,120,5,125,0,0,120,18,1,0,0,0,121,
		122,5,101,0,0,122,123,5,108,0,0,123,124,5,115,0,0,124,125,5,101,0,0,125,
		20,1,0,0,0,126,127,5,119,0,0,127,128,5,104,0,0,128,129,5,105,0,0,129,130,
		5,108,0,0,130,131,5,101,0,0,131,22,1,0,0,0,132,133,5,114,0,0,133,134,5,
		101,0,0,134,135,5,116,0,0,135,136,5,117,0,0,136,137,5,114,0,0,137,138,
		5,110,0,0,138,24,1,0,0,0,139,140,5,46,0,0,140,26,1,0,0,0,141,142,5,99,
		0,0,142,143,5,108,0,0,143,144,5,97,0,0,144,145,5,115,0,0,145,146,5,115,
		0,0,146,28,1,0,0,0,147,148,5,108,0,0,148,149,5,105,0,0,149,150,5,115,0,
		0,150,151,5,116,0,0,151,30,1,0,0,0,152,153,5,60,0,0,153,32,1,0,0,0,154,
		155,5,62,0,0,155,34,1,0,0,0,156,157,5,91,0,0,157,36,1,0,0,0,158,159,5,
		93,0,0,159,38,1,0,0,0,160,161,5,110,0,0,161,162,5,117,0,0,162,163,5,109,
		0,0,163,164,5,98,0,0,164,165,5,101,0,0,165,166,5,114,0,0,166,40,1,0,0,
		0,167,168,5,98,0,0,168,169,5,111,0,0,169,170,5,111,0,0,170,171,5,108,0,
		0,171,42,1,0,0,0,172,173,5,112,0,0,173,174,5,111,0,0,174,175,5,105,0,0,
		175,176,5,110,0,0,176,177,5,116,0,0,177,44,1,0,0,0,178,179,5,114,0,0,179,
		180,5,101,0,0,180,181,5,99,0,0,181,182,5,116,0,0,182,183,5,97,0,0,183,
		184,5,110,0,0,184,185,5,103,0,0,185,186,5,108,0,0,186,187,5,101,0,0,187,
		46,1,0,0,0,188,189,5,115,0,0,189,190,5,113,0,0,190,191,5,117,0,0,191,192,
		5,97,0,0,192,193,5,114,0,0,193,194,5,101,0,0,194,48,1,0,0,0,195,196,5,
		99,0,0,196,197,5,105,0,0,197,198,5,114,0,0,198,199,5,99,0,0,199,200,5,
		108,0,0,200,201,5,101,0,0,201,50,1,0,0,0,202,203,5,112,0,0,203,204,5,111,
		0,0,204,205,5,108,0,0,205,206,5,121,0,0,206,207,5,103,0,0,207,208,5,111,
		0,0,208,209,5,110,0,0,209,52,1,0,0,0,210,211,5,116,0,0,211,212,5,101,0,
		0,212,213,5,120,0,0,213,214,5,116,0,0,214,54,1,0,0,0,215,216,5,99,0,0,
		216,217,5,111,0,0,217,218,5,108,0,0,218,219,5,111,0,0,219,220,5,117,0,
		0,220,221,5,114,0,0,221,56,1,0,0,0,222,223,5,103,0,0,223,224,5,114,0,0,
		224,225,5,111,0,0,225,226,5,117,0,0,226,227,5,112,0,0,227,58,1,0,0,0,228,
		229,5,115,0,0,229,230,5,116,0,0,230,231,5,114,0,0,231,232,5,105,0,0,232,
		233,5,110,0,0,233,234,5,103,0,0,234,60,1,0,0,0,235,236,5,108,0,0,236,237,
		5,105,0,0,237,238,5,110,0,0,238,239,5,101,0,0,239,62,1,0,0,0,240,241,5,
		84,0,0,241,64,1,0,0,0,242,243,5,124,0,0,243,244,5,124,0,0,244,66,1,0,0,
		0,245,246,5,61,0,0,246,247,5,61,0,0,247,68,1,0,0,0,248,249,5,33,0,0,249,
		250,5,61,0,0,250,70,1,0,0,0,251,252,5,60,0,0,252,253,5,61,0,0,253,72,1,
		0,0,0,254,255,5,62,0,0,255,256,5,61,0,0,256,74,1,0,0,0,257,258,5,43,0,
		0,258,76,1,0,0,0,259,260,5,45,0,0,260,78,1,0,0,0,261,262,5,42,0,0,262,
		80,1,0,0,0,263,264,5,33,0,0,264,82,1,0,0,0,265,266,5,116,0,0,266,267,5,
		114,0,0,267,268,5,117,0,0,268,269,5,101,0,0,269,84,1,0,0,0,270,271,5,102,
		0,0,271,272,5,97,0,0,272,273,5,108,0,0,273,274,5,115,0,0,274,275,5,101,
		0,0,275,86,1,0,0,0,276,277,5,71,0,0,277,278,5,114,0,0,278,279,5,111,0,
		0,279,280,5,117,0,0,280,281,5,112,0,0,281,88,1,0,0,0,282,286,7,0,0,0,283,
		285,7,1,0,0,284,283,1,0,0,0,285,288,1,0,0,0,286,284,1,0,0,0,286,287,1,
		0,0,0,287,90,1,0,0,0,288,286,1,0,0,0,289,301,5,48,0,0,290,292,7,2,0,0,
		291,290,1,0,0,0,291,292,1,0,0,0,292,293,1,0,0,0,293,297,7,3,0,0,294,296,
		7,4,0,0,295,294,1,0,0,0,296,299,1,0,0,0,297,295,1,0,0,0,297,298,1,0,0,
		0,298,301,1,0,0,0,299,297,1,0,0,0,300,289,1,0,0,0,300,291,1,0,0,0,301,
		92,1,0,0,0,302,308,5,34,0,0,303,307,8,5,0,0,304,305,5,92,0,0,305,307,9,
		0,0,0,306,303,1,0,0,0,306,304,1,0,0,0,307,310,1,0,0,0,308,306,1,0,0,0,
		308,309,1,0,0,0,309,311,1,0,0,0,310,308,1,0,0,0,311,312,5,34,0,0,312,94,
		1,0,0,0,313,315,7,6,0,0,314,313,1,0,0,0,315,316,1,0,0,0,316,314,1,0,0,
		0,316,317,1,0,0,0,317,318,1,0,0,0,318,319,6,47,0,0,319,96,1,0,0,0,8,0,
		286,291,297,300,306,308,316,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
