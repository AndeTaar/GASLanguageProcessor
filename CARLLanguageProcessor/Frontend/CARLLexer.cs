//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/thomas/Documents/CARLLanguageProcessor/CARLLanguageProcessor/Frontend/CARL.g4 by ANTLR 4.13.1

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
public partial class CARLLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, NUM=19, WS=20;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "NUM", "WS"
	};


	public CARLLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public CARLLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'||'", "'&&'", "'=='", "'!='", "'<'", "'>'", "'<='", "'>='", "'+'", 
		"'-'", "'*'", "'/'", "'%'", "'!'", "'true'", "'false'", "'('", "')'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, "NUM", "WS"
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

	public override string GrammarFileName { get { return "CARL.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static CARLLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,20,117,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,1,0,1,0,1,0,1,1,
		1,1,1,1,1,2,1,2,1,2,1,3,1,3,1,3,1,4,1,4,1,5,1,5,1,6,1,6,1,6,1,7,1,7,1,
		7,1,8,1,8,1,9,1,9,1,10,1,10,1,11,1,11,1,12,1,12,1,13,1,13,1,14,1,14,1,
		14,1,14,1,14,1,15,1,15,1,15,1,15,1,15,1,15,1,16,1,16,1,17,1,17,1,18,1,
		18,5,18,93,8,18,10,18,12,18,96,9,18,1,18,1,18,4,18,100,8,18,11,18,12,18,
		101,1,18,4,18,105,8,18,11,18,12,18,106,3,18,109,8,18,1,19,4,19,112,8,19,
		11,19,12,19,113,1,19,1,19,0,0,20,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,
		9,19,10,21,11,23,12,25,13,27,14,29,15,31,16,33,17,35,18,37,19,39,20,1,
		0,2,1,0,48,57,3,0,9,10,13,13,32,32,122,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,
		0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,
		1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,
		0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,
		1,0,0,0,1,41,1,0,0,0,3,44,1,0,0,0,5,47,1,0,0,0,7,50,1,0,0,0,9,53,1,0,0,
		0,11,55,1,0,0,0,13,57,1,0,0,0,15,60,1,0,0,0,17,63,1,0,0,0,19,65,1,0,0,
		0,21,67,1,0,0,0,23,69,1,0,0,0,25,71,1,0,0,0,27,73,1,0,0,0,29,75,1,0,0,
		0,31,80,1,0,0,0,33,86,1,0,0,0,35,88,1,0,0,0,37,108,1,0,0,0,39,111,1,0,
		0,0,41,42,5,124,0,0,42,43,5,124,0,0,43,2,1,0,0,0,44,45,5,38,0,0,45,46,
		5,38,0,0,46,4,1,0,0,0,47,48,5,61,0,0,48,49,5,61,0,0,49,6,1,0,0,0,50,51,
		5,33,0,0,51,52,5,61,0,0,52,8,1,0,0,0,53,54,5,60,0,0,54,10,1,0,0,0,55,56,
		5,62,0,0,56,12,1,0,0,0,57,58,5,60,0,0,58,59,5,61,0,0,59,14,1,0,0,0,60,
		61,5,62,0,0,61,62,5,61,0,0,62,16,1,0,0,0,63,64,5,43,0,0,64,18,1,0,0,0,
		65,66,5,45,0,0,66,20,1,0,0,0,67,68,5,42,0,0,68,22,1,0,0,0,69,70,5,47,0,
		0,70,24,1,0,0,0,71,72,5,37,0,0,72,26,1,0,0,0,73,74,5,33,0,0,74,28,1,0,
		0,0,75,76,5,116,0,0,76,77,5,114,0,0,77,78,5,117,0,0,78,79,5,101,0,0,79,
		30,1,0,0,0,80,81,5,102,0,0,81,82,5,97,0,0,82,83,5,108,0,0,83,84,5,115,
		0,0,84,85,5,101,0,0,85,32,1,0,0,0,86,87,5,40,0,0,87,34,1,0,0,0,88,89,5,
		41,0,0,89,36,1,0,0,0,90,109,5,48,0,0,91,93,7,0,0,0,92,91,1,0,0,0,93,96,
		1,0,0,0,94,92,1,0,0,0,94,95,1,0,0,0,95,97,1,0,0,0,96,94,1,0,0,0,97,99,
		5,46,0,0,98,100,7,0,0,0,99,98,1,0,0,0,100,101,1,0,0,0,101,99,1,0,0,0,101,
		102,1,0,0,0,102,109,1,0,0,0,103,105,7,0,0,0,104,103,1,0,0,0,105,106,1,
		0,0,0,106,104,1,0,0,0,106,107,1,0,0,0,107,109,1,0,0,0,108,90,1,0,0,0,108,
		94,1,0,0,0,108,104,1,0,0,0,109,38,1,0,0,0,110,112,7,1,0,0,111,110,1,0,
		0,0,112,113,1,0,0,0,113,111,1,0,0,0,113,114,1,0,0,0,114,115,1,0,0,0,115,
		116,6,19,0,0,116,40,1,0,0,0,6,0,94,101,106,108,113,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
