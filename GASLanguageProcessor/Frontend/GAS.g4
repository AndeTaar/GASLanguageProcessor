grammar GAS;

program : (statement)+ ;

statement : declaration | assignment | print | ifStatement | whileStatement;

declaration : type IDENTIFIER ';' | type IDENTIFIER '=' expression ';';

assignment : IDENTIFIER '=' expression ';';

print : 'print' expression ';';

ifStatement : 'if' '(' expression ')' 'then' (statement)* ('else' (statement)*)? 'endif';

whileStatement : 'while' '(' expression ')' 'do' (statement)* 'endwhile';

type : 'int' | 'bool';

expression : equalityExpression ('||' equalityExpression)* ;

equalityExpression : relationExpression (('==' | '!=') relationExpression)* ;

relationExpression : binaryExpression ('<' binaryExpression)* ;

binaryExpression : multExpression (('+' | '-') multExpression)* ;

multExpression : notExpression ('*' notExpression)* ;

notExpression : ('!' | '-')* term ;

term : IDENTIFIER | NUM | 'true' | 'false' | '(' expression ')';

IDENTIFIER : [a-z_][a-z0-9_]* ;
NUM : '0' | [1-9][0-9]* ;
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
