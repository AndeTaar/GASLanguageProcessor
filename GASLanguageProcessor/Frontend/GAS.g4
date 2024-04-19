grammar GAS;

//Program
program : canvas (statement)* ;
canvas : 'canvas' '(' NUM ',' NUM ( ',' expression )? ')' ';';

//Statements
statement : declaration | assignment | ifStatement | whileStatement | collectionDeclaration | functionCall |
functionDeclaration | returnStatement | groupDeclaration;

// (',' identifierTerm ('=' expression)?)* Could be added on this line to allow for multiple declarations on one line
declaration : allTypes IDENTIFIER ('=' expression)?';';
assignment : IDENTIFIER '=' expression ';';
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' ('else' '{' (statement)* '}')?;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
returnStatement : 'return' expression ';';
functionDeclaration : allTypes IDENTIFIER '(' (allTypes   (',' allTypes IDENTIFIER)*)? ')' '{' (statement)* '}' ;

//Collection types
collectionDeclaration : 'list' '<' allTypes '>' IDENTIFIER '=' '{' (expression (',' expression)*)? '}' ';';
listAccess : term ('[' expression ']')?;

//Standard data types
allTypes: 'number' | 'bool' | 'point' | 'rectangle' | 'square' | 'circle' | 'polygon' | 'text' | 'colour' |
'list' | 'group' | 'string' | 'line' | 'group' | 'T';

// Expressions
expression : equalityExpression ('||' equalityExpression)* ;
equalityExpression : relationExpression (('==' | '!=') relationExpression)* ;
relationExpression : binaryExpression ('<' binaryExpression)* ;
binaryExpression : multExpression (('+' | '-') multExpression)* ;
multExpression : notExpression ('*' notExpression)* ;
notExpression : ('!' | '-')* listAccessExpression ;
listAccessExpression : term ('[' expression ']')?;


//Terms
term : IDENTIFIER | NUM | 'true' | 'false'  | '(' expression ')' | listTerm |
 functionCall | ALLSTRINGS;

listTerm : '{' (expression (',' expression)*)? '}';
compoundStatements : '{' (statement (',' statement)*)? '}';
groupDeclaration : 'group' IDENTIFIER '=' 'Group' '(' expression ',' compoundStatements ')' ';';

functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [-]?[1-9][0-9]* ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
