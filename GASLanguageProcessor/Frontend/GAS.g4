grammar GAS;

//Program
program : (statement)* canvas (statement)* ;
canvas : 'canvas' '(' expression ',' expression ( ',' expression )? ')' ';';

//Statements
statement : simpleStatement | complexStatement;
simpleStatement : (declaration | assignment | functionCall | returnStatement | collectionDeclaration) ';';
complexStatement:  whileStatement | functionDeclaration | forStatement | classDeclaration | ifStatement;

// (',' identifierTerm ('=' expression)?)* Could be added on this line to allow for multiple declarations on one line
declaration : type IDENTIFIER ('=' expression)?;
collectionDeclaration : collectionType IDENTIFIER ('=' expression)?;
assignment : IDENTIFIER ('.' IDENTIFIER)* '=' expression;
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) ';' expression  ';' assignment ')' '{' (statement)* '}';
returnStatement : 'return' expression;
classDeclaration : 'class' IDENTIFIER '{' (statement)* '}';
functionDeclaration : type IDENTIFIER '(' (type IDENTIFIER  (',' type IDENTIFIER)*)? ')' '{' (statement)* ? '}';

//Standard data types
type: 'number' | 'bool' | 'point' | 'rectangle' | 'square' | 'circle' | 'polygon' | 'text' | 'colour' | 'string' | 'line' | 'T' | 'void';
collectionType : 'list' '<' type '>' | 'segLine' | 'group' | 'T' | 'void' | 'ellipse';

// Expressions
expression : equalityExpression (('||' | '&&') (equalityExpression | expression))? ;
equalityExpression : relationExpression (('==' | '!=') (relationExpression | equalityExpression))? ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') (binaryExpression | relationExpression))? ;
binaryExpression : multExpression (('+' | '-') (multExpression | binaryExpression))? ;
multExpression : notExpression (('*' | '/' | '%' ) (notExpression | multExpression))? ;
notExpression : ('!' | '-')* listAccessExpression ;
listAccessExpression : term ('[' expression ']')?;

//Terms
term : IDENTIFIER ('.' IDENTIFIER)* | NUM | 'true' | 'false' | 'null'  | '(' expression ')' | listTerm |
 functionCall | ALLSTRINGS | groupTerm;

listTerm : 'List' '{' (expression (',' expression)*)? '}';
groupTerm : 'Group' '(' expression ',' '{' (statement)* '}' ')';

functionCall : IDENTIFIER ( '.' IDENTIFIER  )* '(' (expression (',' expression)*)? ')';

COMMENT: '/*' .*? '*/' -> skip;
IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | '-'? [0-9]* '.' [0-9]+ | '-'? [0-9]+ ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
