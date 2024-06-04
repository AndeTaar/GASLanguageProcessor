grammar GAS;

//Program
program : (statement)* EOF;
canvas : 'canvas' '(' expression ',' expression ',' expression ')';

//Statements
statement : simpleStatement | complexStatement;
simpleStatement : (declaration | assignment | functionCall | returnStatement | increment | canvas) ';';
complexStatement:  whileStatement | functionDeclaration | forStatement | ifStatement | recDefinition;

recDefinition : 'TypeDef' recordTypeIdentifier '=' '{' (allTypes identifier ';')* '}';

declaration : (type | collectionType) (attributeIdentifier | identifier) ('=' expression)?;
assignment : (attributeIdentifier | identifier) ('=' | '+=' | '-=' | '*=' | '/=') expression;
increment : (attributeIdentifier | identifier) ('++' | '--');
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) ';' expression  ';' (assignment | increment) ')' '{' (statement)* '}';
returnStatement : 'return' expression;
functionDeclaration : allTypes identifier '(' (allTypes identifier  (',' allTypes identifier)*)? ')' '{' (statement)* ? '}';
//Standard data types

allTypes : type | collectionType ;
type: 'num' | 'bool' | 'string' | 'void' | identifier;
collectionType : 'list' '<' (type) '>' | 'group';

// Expressions
expression : equalityExpression (('||' | '&&') (equalityExpression | expression))? ;
equalityExpression : relationExpression (('==' | '!=') (relationExpression | equalityExpression))? ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') (binaryExpression | relationExpression))? ;
binaryExpression : multExpression (('+' | '-') (multExpression | binaryExpression))? ;
multExpression : unaryExpression (('*' | '/' | '%' ) (unaryExpression | multExpression))? ;
unaryExpression : ('!' | '-')* term;

//Terms
term : NUM | 'true' | 'false' | 'null'  | '(' expression ')' | listTerm |
 functionCall | ALLSTRINGS | groupTerm | attributeIdentifier | identifier | recordTerm;

recordTerm: recordTypeIdentifier '{' ( identifier '=' expression ';')* '}';
listTerm : 'List' '<' type '>' '{' (expression (',' expression)*)? '}';
groupTerm : 'Group' '(' expression ',' '{' (statement)* '}' ')';

functionCall : identifier '(' (expression (',' expression)*)? ')';

recordTypeIdentifier : IDENTIFIER;
identifier : IDENTIFIER;
attributeIdentifier : identifier '.' identifier;

COMMENT: '/*' .*? '*/' -> skip;
IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [0-9]* '.' [0-9]+ | [0-9]+ ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
