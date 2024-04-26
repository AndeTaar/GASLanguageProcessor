grammar GAS;

//Program
program : canvas (statement)* ;
canvas : 'canvas' '(' expression ',' expression ( ',' expression )? ')' ';';

//Statements
statement : declaration | assignment | ifStatement | whileStatement | collectionDeclaration | functionCall |
functionDeclaration | groupDeclaration | forStatement;

// (',' identifierTerm ('=' expression)?)* Could be added on this line to allow for multiple declarations on one line
declaration : type IDENTIFIER ('=' expression)?';';
assignment : IDENTIFIER '=' expression ';';
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) expression  ';' assignment ')' '{' (statement)* '}';
returnStatement : 'return' expression ';';
classDeclaration : 'class' IDENTIFIER '{' (statement)* '}';
functionDeclaration : type IDENTIFIER '(' (type IDENTIFIER  (',' type IDENTIFIER)*)? ')' '{' (statement | returnStatement)* ? '}';

//Collection types
collectionDeclaration : 'list' '<' type '>' IDENTIFIER '=' '{' (expression (',' expression)*)? '}' ';';

//Standard data types
type: 'number' | 'bool' | 'point' | 'rectangle' | 'square' | 'circle' | 'polygon' | 'text' | 'colour' |
'list' | 'group' | 'string' | 'line' | 'group' | 'T' | 'void';

// Expressions
expression : equalityExpression (('||' | '&&') equalityExpression)* ;
equalityExpression : relationExpression (('==' | '!=') relationExpression)* ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') binaryExpression)* ;
binaryExpression : multExpression (('+' | '-') multExpression)* ;
multExpression : notExpression (('*' | '/') notExpression)* ;
notExpression : ('!' | '-')* listAccessExpression ;
listAccessExpression : term ('[' expression ']')?;


//Terms
term : IDENTIFIER | NUM | 'true' | 'false' | 'null'  | '(' expression ')' | listTerm |
 functionCall | ALLSTRINGS;
parameterAccess : IDENTIFIER '.' IDENTIFIER;

listTerm : '{' (expression (',' expression)*)? '}';
groupDeclaration : 'group' IDENTIFIER '=' 'Group' '(' expression ',' '{' (statement (',' statement)*)? '}' ')' ';';

functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [-]?[1-9][0-9]* ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
