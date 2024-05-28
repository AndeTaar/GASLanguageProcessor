grammar GAS;

//Program
program : (statement)* EOF;
canvas : 'canvas' '(' expression ',' expression ',' expression ')';

//Statements
statement : simpleStatement | complexStatement;
simpleStatement : (declaration | assignment | functionCall | returnStatement | increment | canvas) ';';
complexStatement:  whileStatement | functionDeclaration | forStatement | ifStatement;

declaration : (type | collectionType) IDENTIFIER ('=' expression)?;
assignment : IDENTIFIER ('=' | '+=' | '-=' | '*=' | '/=') expression;
increment : IDENTIFIER ('++' | '--');
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) ';' expression  ';' (assignment | increment) ')' '{' (statement)* '}';
returnStatement : 'return' expression;
functionDeclaration : allTypes IDENTIFIER '(' (allTypes IDENTIFIER  (',' allTypes IDENTIFIER)*)? ')' '{' (statement)* ? '}';
//Standard data types

allTypes : type | collectionType;
type: 'num' | 'bool' | 'point' | 'rectangle' | 'square' | 'circle' | 'polygon' | 'text' | 'color' | 'string' | 'line'
| 'void' | 'segLine' | 'ellipse'  | 'polygon' | 'arrow';
collectionType : 'list' '<' type '>' | 'group';

// Expressions
expression : equalityExpression (('||' | '&&') (equalityExpression | expression))? ;
equalityExpression : relationExpression (('==' | '!=') (relationExpression | equalityExpression))? ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') (binaryExpression | relationExpression))? ;
binaryExpression : multExpression (('+' | '-') (multExpression | binaryExpression))? ;
multExpression : unaryExpression (('*' | '/' | '%' ) (unaryExpression | multExpression))? ;
unaryExpression : ('!' | '-')* term;

//Terms
term : IDENTIFIER | NUM | 'true' | 'false' | 'null'  | '(' expression ')' | listTerm |
 functionCall | ALLSTRINGS | groupTerm;

listTerm : 'List' '<' type '>' '{' (expression (',' expression)*)? '}';
groupTerm : 'Group' '(' expression ',' '{' (statement)* '}' ')';

functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

COMMENT: '/*' .*? '*/' -> skip;
IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [0-9]* '.' [0-9]+ | [0-9]+ ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
