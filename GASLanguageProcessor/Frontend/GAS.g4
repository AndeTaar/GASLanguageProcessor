grammar GAS;

//Program
program : (statement)* canvas (statement)* ;
canvas : 'canvas' '(' expression ',' expression ',' expression ')' ';';

//Statements
statement : simpleStatement | complexStatement;
simpleStatement : (declaration | assignment | functionCall | returnStatement | collectionDeclaration) ';';
complexStatement:  whileStatement | functionDeclaration | forStatement | ifStatement;

declaration : type IDENTIFIER ('=' expression)?;
collectionDeclaration : collectionType IDENTIFIER ('=' expression)?;
assignment : IDENTIFIER ('.' IDENTIFIER)* ('=' | '+=' | '-=' | '*=' | '/=') expression;
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) ';' expression  ';' assignment ')' '{' (statement)* '}';
returnStatement : 'return' expression;
functionDeclaration : type IDENTIFIER '(' (type IDENTIFIER  (',' type IDENTIFIER)*)? ')' '{' (statement)* ? '}';

//Standard data types
type: 'num' | 'bool' | 'point' | 'rectangle' | 'square' | 'circle' | 'polygon' | 'text' | 'color' | 'string' | 'line' |
 'T' | 'void' | 'segLine' | 'ellipse'  | 'polygon';
collectionType : 'list' '<' type '>' | 'group';

// Expressions
expression : equalityExpression (('||' | '&&') (equalityExpression | expression))? ;
equalityExpression : relationExpression (('==' | '!=') (relationExpression | equalityExpression))? ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') (binaryExpression | relationExpression))? ;
binaryExpression : multExpression (('+' | '-') (multExpression | binaryExpression))? ;
multExpression : unaryExpression (('*' | '/' | '%' ) (unaryExpression | multExpression))? ;
unaryExpression : ('!' | '-')* listAccessExpression ('++' | '--')*;
listAccessExpression : term ('[' expression ']')?;

//Terms
term : IDENTIFIER ('.' IDENTIFIER)* | NUM | 'true' | 'false' | 'null'  | '(' expression ')' | listTerm |
 functionCall | ALLSTRINGS | groupTerm;

listTerm : 'List' '{' (expression (',' expression)*)? '}';
groupTerm : 'Group' '(' expression ',' '{' (statement)* '}' ')';

functionCall : IDENTIFIER ( '.' IDENTIFIER  )* '(' (expression (',' expression)*)? ')';

COMMENT: '/*' .*? '*/' -> skip;
IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [0-9]* '.' [0-9]+ | [0-9]+ ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
