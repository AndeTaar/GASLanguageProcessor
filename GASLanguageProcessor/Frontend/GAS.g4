grammar GAS;

//Program
program : (statement)* EOF;
canvas : 'canvas' '(' expression ',' expression ',' expression ')';

//Statements
statement : simpleStatement | complexStatement;
simpleStatement : (declaration | assignment | functionCall | returnStatement | increment | canvas | recDeclaration | recAssignment) ';';
complexStatement:  whileStatement | functionDeclaration | forStatement | ifStatement | recDefinition;

recDefinition : 'TypeDef' recTypeIdent '=' '{' (allTypes ident ';')* '}';
recDeclaration : (recTypeIdent | recCollectionType) recIdent '=' recExpression;
recAssignment : recIdent '=' recExpression;

declaration : (type | collectionType) varIdent ('=' expression)?;
assignment : varIdent ('=' | '+=' | '-=' | '*=' | '/=') expression;
increment : varIdent ('++' | '--');
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' elseStatement?;
elseStatement : 'else' ('{' (statement)* '}') | 'else'  ifStatement;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
forStatement : 'for' '(' (declaration | assignment) ';' expression  ';' (assignment | increment) ')' '{' (statement)* '}';
returnStatement : 'return' expression;
functionDeclaration : allTypes ident '(' (allTypes ident  (',' allTypes ident)*)? ')' '{' (statement)* ? '}';
//Standard data types

allTypes : type | collectionType | recTypeIdent;
type: 'num' | 'bool' | 'string' | 'void';
collectionType : 'list' '<' (type) '>' | 'group';

recCollectionType : 'list' '<' (recTypeIdent) '>';

// Expressions
expression : equalityExpression (('||' | '&&') (equalityExpression | expression))? ;
equalityExpression : relationExpression (('==' | '!=') (relationExpression | equalityExpression))? ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') (binaryExpression | relationExpression))? ;
binaryExpression : multExpression (('+' | '-') (multExpression | binaryExpression))? ;
multExpression : unaryExpression (('*' | '/' | '%' ) (unaryExpression | multExpression))? ;
unaryExpression : ('!' | '-')* term;

recExpression : recIdent | recTypeIdent '{' (ident '=' allExpression ';')* '}' | recListTerm;

recListTerm : 'List' '<' recTypeIdent '>' '{' (recExpression (',' recExpression)*)? '}';
allExpression : expression | recExpression;

//Terms
term : NUM | 'true' | 'false' | 'null'  | '(' expression ')' | listTerm |
 functionCall | ALLSTRINGS | groupTerm | varIdent;

listTerm : 'List' '<' type '>' '{' (expression (',' expression)*)? '}';
groupTerm : 'Group' '(' expression ',' '{' (statement)* '}' ')';

functionCall : ident '(' (allExpression (',' allExpression)*)? ')';

recTypeIdent : IDENTIFIER;
recIdent : IDENTIFIER;
varIdent : IDENTIFIER | recIdent '.' IDENTIFIER;
ident : IDENTIFIER;

COMMENT: '/*' .*? '*/' -> skip;
IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [0-9]* '.' [0-9]+ | [0-9]+ ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
