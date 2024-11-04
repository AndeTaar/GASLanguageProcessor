grammar GAS;

//Program
program : (statement)* EOF;

//Statements
statement : constantDeclaration | functionDeclaration | classDeclaration ;

classDeclaration : 'class' IDENTIFIER '{' (declaration | functionDeclaration | constantDeclaration)* '}';
propertyDeclaration : propertyVisibility type IDENTIFIER ('=' expression)? ';';


constantDeclaration : 'const' type IDENTIFIER '=' expression;
declaration : (type | collectionType) IDENTIFIER ('=' expression)?;
assignment : (IDENTIFIER | ATTRIBUTEIDENTIFIER) ('=' | '+=' | '-=' | '*=' | '/=') expression;
listAssignment : IDENTIFIER '[' expression ']' '=' expression;
increment : (IDENTIFIER | ATTRIBUTEIDENTIFIER) ('++' | '--');
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) ';' expression  ';' (assignment | increment) ')' '{' (statement)* '}';
returnStatement : 'return' expression;

functionDeclaration : (type | collectionType) IDENTIFIER '(' ((type | collectionType) IDENTIFIER  (',' (type | collectionType) IDENTIFIER)*)? ')' '{' (statement)* ? '}';

//Standard data types

type: 'num' | 'bool' | 'string' | 'void' | 'var' | IDENTIFIER;
collectionType : type'['']' | 'group';

propertyVisibility : 'public' | 'private' | 'protected';

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
