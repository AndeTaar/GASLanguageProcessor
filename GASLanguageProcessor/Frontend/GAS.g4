grammar GAS;

//Program
program : (statement)* EOF;
canvas : 'canvas' '(' expression ',' expression ',' expression ')';

//Statements
statement : simpleStatement | complexStatement;
simpleStatement : (declaration | assignment | functionCall | returnStatement | increment | canvas) ';';
complexStatement:  whileStatement | functionDeclaration | forStatement | ifStatement | structCreation;

declaration : (type | collectionType) IDENTIFIER ('=' expression)?;
assignment : IDENTIFIER ('=' | '+=' | '-=' | '*=' | '/=') expression;
structCreation : IDENTIFIER ':' 'Struct''{' (declaration ';')* '}';
increment : IDENTIFIER ('++' | '--');
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) ';' expression  ';' (assignment | increment) ')' '{' (statement)* '}';
returnStatement : 'return' expression;
functionDeclaration : allTypes IDENTIFIER '(' (allTypes IDENTIFIER  (',' allTypes IDENTIFIER)*)? ')' '{' (statement)* ? '}';
//Standard data types

allTypes : type | collectionType;
type: 'num' | 'bool' | 'point' | 'rectangle' | 'square' | 'circle' | 'polygon' | 'text' | 'col' | 'string' | 'line'
| 'void' | 'segLine' | 'ellipse'  | 'polygon' | 'arrow' | 'struct';
collectionType : 'list' '<' type '>' | 'group';

// Expressions
expression : equalityExpression (('||' | '&&') (equalityExpression | expression))? ;
equalityExpression : relationExpression (('==' | '!=') (relationExpression | equalityExpression))? ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') (binaryExpression | relationExpression))? ;
binaryExpression : multExpression (('+' | '-') (multExpression | binaryExpression))? ;
multExpression : unaryExpression (('*' | '/' | '%' ) (unaryExpression | multExpression))? ;
unaryExpression : ('!' | '-')* term;

//Terms
term : IDENTIFIER ('.' IDENTIFIER)? | NUM | 'true' | 'false' | 'null'  | '(' expression ')' | listTerm |
 functionCall | ALLSTRINGS | groupTerm | structTerm;

listTerm : 'List' '<' type '>' '{' (expression (',' expression)*)? '}';
groupTerm : 'Group' '(' expression ',' '{' (statement)* '}' ')';
structTerm : IDENTIFIER '{' (assignment (',' assignment)*) '}';

functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

COMMENT: '/*' .*? '*/' -> skip;
IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [0-9]* '.' [0-9]+ | [0-9]+ ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
