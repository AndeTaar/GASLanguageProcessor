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
		T__38=39, IDENTIFIER=40, NUM=41, WS=42, ALLSTRINGS=43;
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
		"T__33", "T__34", "T__35", "T__36", "T__37", "T__38", "IDENTIFIER", "NUM", 
		"WS", "ALLSTRINGS"
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
		"'else'", "'while'", "'print'", "'point'", "'rectangle'", "'square'", 
		"'circle'", "'polygon'", "'text'", "'line'", "'<'", "'>'", "'list'", "'group'", 
		"'['", "']'", "'number'", "'bool'", "'string'", "'colour'", "'color'", 
		"'||'", "'=='", "'!='", "'+'", "'-'", "'*'", "'!'", "'true'", "'false'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, "IDENTIFIER", "NUM", "WS", "ALLSTRINGS"
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
		4,0,43,287,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,2,1,2,1,3,1,3,1,4,1,4,1,5,1,
		5,1,6,1,6,1,6,1,7,1,7,1,8,1,8,1,9,1,9,1,9,1,9,1,9,1,10,1,10,1,10,1,10,
		1,10,1,10,1,11,1,11,1,11,1,11,1,11,1,11,1,12,1,12,1,12,1,12,1,12,1,12,
		1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,14,1,14,1,14,1,14,
		1,14,1,14,1,14,1,15,1,15,1,15,1,15,1,15,1,15,1,15,1,16,1,16,1,16,1,16,
		1,16,1,16,1,16,1,16,1,17,1,17,1,17,1,17,1,17,1,18,1,18,1,18,1,18,1,18,
		1,19,1,19,1,20,1,20,1,21,1,21,1,21,1,21,1,21,1,22,1,22,1,22,1,22,1,22,
		1,22,1,23,1,23,1,24,1,24,1,25,1,25,1,25,1,25,1,25,1,25,1,25,1,26,1,26,
		1,26,1,26,1,26,1,27,1,27,1,27,1,27,1,27,1,27,1,27,1,28,1,28,1,28,1,28,
		1,28,1,28,1,28,1,29,1,29,1,29,1,29,1,29,1,29,1,30,1,30,1,30,1,31,1,31,
		1,31,1,32,1,32,1,32,1,33,1,33,1,34,1,34,1,35,1,35,1,36,1,36,1,37,1,37,
		1,37,1,37,1,37,1,38,1,38,1,38,1,38,1,38,1,38,1,39,1,39,5,39,258,8,39,10,
		39,12,39,261,9,39,1,40,1,40,1,40,5,40,266,8,40,10,40,12,40,269,9,40,3,
		40,271,8,40,1,41,4,41,274,8,41,11,41,12,41,275,1,41,1,41,1,42,1,42,4,42,
		282,8,42,11,42,12,42,283,1,42,1,42,0,0,43,1,1,3,2,5,3,7,4,9,5,11,6,13,
		7,15,8,17,9,19,10,21,11,23,12,25,13,27,14,29,15,31,16,33,17,35,18,37,19,
		39,20,41,21,43,22,45,23,47,24,49,25,51,26,53,27,55,28,57,29,59,30,61,31,
		63,32,65,33,67,34,69,35,71,36,73,37,75,38,77,39,79,40,81,41,83,42,85,43,
		1,0,6,2,0,95,95,97,122,3,0,48,57,95,95,97,122,1,0,49,57,1,0,48,57,3,0,
		9,10,13,13,32,32,4,0,48,57,65,90,95,95,97,122,291,0,1,1,0,0,0,0,3,1,0,
		0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,
		1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,
		0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,
		1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,0,45,1,0,0,0,0,47,1,0,0,
		0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,0,0,59,
		1,0,0,0,0,61,1,0,0,0,0,63,1,0,0,0,0,65,1,0,0,0,0,67,1,0,0,0,0,69,1,0,0,
		0,0,71,1,0,0,0,0,73,1,0,0,0,0,75,1,0,0,0,0,77,1,0,0,0,0,79,1,0,0,0,0,81,
		1,0,0,0,0,83,1,0,0,0,0,85,1,0,0,0,1,87,1,0,0,0,3,94,1,0,0,0,5,96,1,0,0,
		0,7,98,1,0,0,0,9,100,1,0,0,0,11,102,1,0,0,0,13,104,1,0,0,0,15,107,1,0,
		0,0,17,109,1,0,0,0,19,111,1,0,0,0,21,116,1,0,0,0,23,122,1,0,0,0,25,128,
		1,0,0,0,27,134,1,0,0,0,29,144,1,0,0,0,31,151,1,0,0,0,33,158,1,0,0,0,35,
		166,1,0,0,0,37,171,1,0,0,0,39,176,1,0,0,0,41,178,1,0,0,0,43,180,1,0,0,
		0,45,185,1,0,0,0,47,191,1,0,0,0,49,193,1,0,0,0,51,195,1,0,0,0,53,202,1,
		0,0,0,55,207,1,0,0,0,57,214,1,0,0,0,59,221,1,0,0,0,61,227,1,0,0,0,63,230,
		1,0,0,0,65,233,1,0,0,0,67,236,1,0,0,0,69,238,1,0,0,0,71,240,1,0,0,0,73,
		242,1,0,0,0,75,244,1,0,0,0,77,249,1,0,0,0,79,255,1,0,0,0,81,270,1,0,0,
		0,83,273,1,0,0,0,85,279,1,0,0,0,87,88,5,99,0,0,88,89,5,97,0,0,89,90,5,
		110,0,0,90,91,5,118,0,0,91,92,5,97,0,0,92,93,5,115,0,0,93,2,1,0,0,0,94,
		95,5,40,0,0,95,4,1,0,0,0,96,97,5,44,0,0,97,6,1,0,0,0,98,99,5,41,0,0,99,
		8,1,0,0,0,100,101,5,59,0,0,101,10,1,0,0,0,102,103,5,61,0,0,103,12,1,0,
		0,0,104,105,5,105,0,0,105,106,5,102,0,0,106,14,1,0,0,0,107,108,5,123,0,
		0,108,16,1,0,0,0,109,110,5,125,0,0,110,18,1,0,0,0,111,112,5,101,0,0,112,
		113,5,108,0,0,113,114,5,115,0,0,114,115,5,101,0,0,115,20,1,0,0,0,116,117,
		5,119,0,0,117,118,5,104,0,0,118,119,5,105,0,0,119,120,5,108,0,0,120,121,
		5,101,0,0,121,22,1,0,0,0,122,123,5,112,0,0,123,124,5,114,0,0,124,125,5,
		105,0,0,125,126,5,110,0,0,126,127,5,116,0,0,127,24,1,0,0,0,128,129,5,112,
		0,0,129,130,5,111,0,0,130,131,5,105,0,0,131,132,5,110,0,0,132,133,5,116,
		0,0,133,26,1,0,0,0,134,135,5,114,0,0,135,136,5,101,0,0,136,137,5,99,0,
		0,137,138,5,116,0,0,138,139,5,97,0,0,139,140,5,110,0,0,140,141,5,103,0,
		0,141,142,5,108,0,0,142,143,5,101,0,0,143,28,1,0,0,0,144,145,5,115,0,0,
		145,146,5,113,0,0,146,147,5,117,0,0,147,148,5,97,0,0,148,149,5,114,0,0,
		149,150,5,101,0,0,150,30,1,0,0,0,151,152,5,99,0,0,152,153,5,105,0,0,153,
		154,5,114,0,0,154,155,5,99,0,0,155,156,5,108,0,0,156,157,5,101,0,0,157,
		32,1,0,0,0,158,159,5,112,0,0,159,160,5,111,0,0,160,161,5,108,0,0,161,162,
		5,121,0,0,162,163,5,103,0,0,163,164,5,111,0,0,164,165,5,110,0,0,165,34,
		1,0,0,0,166,167,5,116,0,0,167,168,5,101,0,0,168,169,5,120,0,0,169,170,
		5,116,0,0,170,36,1,0,0,0,171,172,5,108,0,0,172,173,5,105,0,0,173,174,5,
		110,0,0,174,175,5,101,0,0,175,38,1,0,0,0,176,177,5,60,0,0,177,40,1,0,0,
		0,178,179,5,62,0,0,179,42,1,0,0,0,180,181,5,108,0,0,181,182,5,105,0,0,
		182,183,5,115,0,0,183,184,5,116,0,0,184,44,1,0,0,0,185,186,5,103,0,0,186,
		187,5,114,0,0,187,188,5,111,0,0,188,189,5,117,0,0,189,190,5,112,0,0,190,
		46,1,0,0,0,191,192,5,91,0,0,192,48,1,0,0,0,193,194,5,93,0,0,194,50,1,0,
		0,0,195,196,5,110,0,0,196,197,5,117,0,0,197,198,5,109,0,0,198,199,5,98,
		0,0,199,200,5,101,0,0,200,201,5,114,0,0,201,52,1,0,0,0,202,203,5,98,0,
		0,203,204,5,111,0,0,204,205,5,111,0,0,205,206,5,108,0,0,206,54,1,0,0,0,
		207,208,5,115,0,0,208,209,5,116,0,0,209,210,5,114,0,0,210,211,5,105,0,
		0,211,212,5,110,0,0,212,213,5,103,0,0,213,56,1,0,0,0,214,215,5,99,0,0,
		215,216,5,111,0,0,216,217,5,108,0,0,217,218,5,111,0,0,218,219,5,117,0,
		0,219,220,5,114,0,0,220,58,1,0,0,0,221,222,5,99,0,0,222,223,5,111,0,0,
		223,224,5,108,0,0,224,225,5,111,0,0,225,226,5,114,0,0,226,60,1,0,0,0,227,
		228,5,124,0,0,228,229,5,124,0,0,229,62,1,0,0,0,230,231,5,61,0,0,231,232,
		5,61,0,0,232,64,1,0,0,0,233,234,5,33,0,0,234,235,5,61,0,0,235,66,1,0,0,
		0,236,237,5,43,0,0,237,68,1,0,0,0,238,239,5,45,0,0,239,70,1,0,0,0,240,
		241,5,42,0,0,241,72,1,0,0,0,242,243,5,33,0,0,243,74,1,0,0,0,244,245,5,
		116,0,0,245,246,5,114,0,0,246,247,5,117,0,0,247,248,5,101,0,0,248,76,1,
		0,0,0,249,250,5,102,0,0,250,251,5,97,0,0,251,252,5,108,0,0,252,253,5,115,
		0,0,253,254,5,101,0,0,254,78,1,0,0,0,255,259,7,0,0,0,256,258,7,1,0,0,257,
		256,1,0,0,0,258,261,1,0,0,0,259,257,1,0,0,0,259,260,1,0,0,0,260,80,1,0,
		0,0,261,259,1,0,0,0,262,271,5,48,0,0,263,267,7,2,0,0,264,266,7,3,0,0,265,
		264,1,0,0,0,266,269,1,0,0,0,267,265,1,0,0,0,267,268,1,0,0,0,268,271,1,
		0,0,0,269,267,1,0,0,0,270,262,1,0,0,0,270,263,1,0,0,0,271,82,1,0,0,0,272,
		274,7,4,0,0,273,272,1,0,0,0,274,275,1,0,0,0,275,273,1,0,0,0,275,276,1,
		0,0,0,276,277,1,0,0,0,277,278,6,41,0,0,278,84,1,0,0,0,279,281,5,34,0,0,
		280,282,7,5,0,0,281,280,1,0,0,0,282,283,1,0,0,0,283,281,1,0,0,0,283,284,
		1,0,0,0,284,285,1,0,0,0,285,286,5,34,0,0,286,86,1,0,0,0,6,0,259,267,270,
		275,283,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}