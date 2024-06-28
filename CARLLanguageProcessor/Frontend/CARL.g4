grammar CARL;

//Program
program : expression EOF;

// Expressions
expression : equalityExpression (('||' | '&&') (equalityExpression | expression))? ;
equalityExpression : relationExpression (('==' | '!=') (relationExpression | equalityExpression))? ;
relationExpression : binaryExpression (('<' | '>' | '<=' | '>=') (binaryExpression | relationExpression))? ;
binaryExpression : multExpression (('+' | '-') (multExpression | binaryExpression))? ;
multExpression : unaryExpression (('*' | '/' | '%' ) (unaryExpression | multExpression))? ;
unaryExpression : ('!' | '-')* term;

//Terms
term : NUM | 'true' | 'false' | '(' expression ')';


NUM : '0' | [0-9]* '.' [0-9]+ | [0-9]+ ;
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace

