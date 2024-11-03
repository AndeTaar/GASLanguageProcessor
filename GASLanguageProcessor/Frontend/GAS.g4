grammar GAS;

//Program
program : (statement)* EOF;

//Statements
statement : simpleStatement | complexStatement;

simpleStatement : (declaration | assignment | functionCallStatement | returnStatement | increment | listAssignment) ';';
complexStatement:  whileStatement | functionDeclaration | forStatement | ifStatement | recDefinition;

declaration : (type | collectionType) IDENTIFIER ('=' expression)?;
assignment : (IDENTIFIER | ATTRIBUTEIDENTIFIER) ('=' | '+=' | '-=' | '*=' | '/=') expression;
listAssignment : IDENTIFIER '[' expression ']' '=' expression;
increment : (IDENTIFIER | ATTRIBUTEIDENTIFIER) ('++' | '--');
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) ';' expression  ';' (assignment | increment) ')' '{' (statement)* '}';
returnStatement : 'return' expression;

functionDeclaration : allTypes IDENTIFIER '(' (allTypes IDENTIFIER  (',' allTypes IDENTIFIER)*)? ')' '{' (statement)* ? '}';

recDefinition : 'TypeDef' IDENTIFIER '{' (IDENTIFIER ':' allTypes (',' IDENTIFIER ':' allTypes)*)? '}';

//Standard data types

allTypes : type | collectionType ;
type: 'num' | 'bool' | 'string' | 'void' | IDENTIFIER;
collectionType : type'['']' | 'group';

// Expressions
expression : equalityExpression (('||' | '&&') equalityExpression)* ;
equalityExpression : relationExpression (('==' | '!=') relationExpression)* ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') binaryExpression)* ;
binaryExpression : multExpression (('+' | '-') multExpression)* ;
multExpression : unaryExpression (('*' | '/' | '%' ) unaryExpression)* ;
unaryExpression : ('!' | '-')* term;

//Terms
term : NUM | 'true' | 'false' | 'null'  | '(' expression ')' | arrayTerm | arrayAccessTerm | arrayNewTerm |
 functionCallTerm | ALLSTRINGS | groupTerm | IDENTIFIER | ATTRIBUTEIDENTIFIER | recordTerm | arraySizeTerm;

recordTerm: IDENTIFIER '{' (IDENTIFIER '=' expression (',' IDENTIFIER '=' expression)* )? '}';
arrayTerm : '<'type'>''[' (expression (',' expression)*)? ']';
arrayAccessTerm : (IDENTIFIER | ATTRIBUTEIDENTIFIER) '[' expression ']';
arraySizeTerm : (IDENTIFIER | ATTRIBUTEIDENTIFIER) '.' 'count';
arrayNewTerm: 'new' type '[' expression ']';
groupTerm : 'Group' '(' expression ',' '{' (statement)* '}' ')';

functionCallStatement : (IDENTIFIER | ATTRIBUTEIDENTIFIER) '(' (expression (',' expression)*)? ')';
functionCallTerm : (IDENTIFIER | ATTRIBUTEIDENTIFIER) '(' (expression (',' expression)*)? ')';

COMMENT: ('/*' .*? '*/' | '//' .*? '\n') -> skip;
ATTRIBUTEIDENTIFIER : [a-zA-Z_][a-zA-Z0-9_-]* '.' [a-zA-Z_][a-zA-Z0-9_-]* ;
IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_-]* ;
NUM : '0' | [0-9]* '.' [0-9]+ | [0-9]+ ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
