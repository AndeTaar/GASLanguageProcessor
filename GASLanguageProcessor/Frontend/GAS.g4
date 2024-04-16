grammar GAS;

//Program
program : canvas (statement)* functionDeclaration*;
canvas : 'canvas' '(' NUM ',' NUM ')';

//Statements
statement : declaration | pointDeclaration | squareDeclaration | rectangleDeclaration | circleDeclaration | assignment |
 print | ifStatement | whileStatement | collectionDeclaration | groupDeclaration;
declaration : dataType IDENTIFIER ';' | dataType IDENTIFIER '=' expression ';';
assignment : IDENTIFIER '=' expression ';';
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' ('else' '{' (statement)* '}')?;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
print : 'print' expression ';';
functionDeclaration : allTypes IDENTIFIER '(' (allTypes IDENTIFIER (',' allTypes IDENTIFIER)*)? ')' '{' (statement)* '}' ;


//Shapes
point: 'point';
rectangle: 'rectangle';
square: 'square';
circle: 'circle';


//Shape declarations
pointDeclaration : point IDENTIFIER '=' '(' NUM ',' NUM ')' ';';
// Top left, width and height
rectangleDeclaration : rectangle IDENTIFIER '=' '( point,' NUM ',' NUM ')' ';';
// Top left and side length
squareDeclaration : square IDENTIFIER '=' '( point,' NUM ')' ';';
//Center and radius
circleDeclaration : circle IDENTIFIER '=' '( point, ' NUM ')' ';';


//Collection types
collectionDeclaration : list '<' allTypes '>' IDENTIFIER '=' '{' (expression (',' expression)*)? '}' ';';
list : 'list';
groupDeclaration : 'group' IDENTIFIER '=' '(' point ',' '{' (statement (',' statement)*)? '}' ')' ';';
listAccess : IDENTIFIER '[' expression ']';

//Standard data types
dataType : 'number' | 'bool' ;
allTypes : dataType | point | rectangle | square | circle;


// Expressions
expression : equalityExpression ('||' equalityExpression)* ;
equalityExpression : relationExpression (('==' | '!=') relationExpression)* ;
relationExpression : binaryExpression ('<' binaryExpression)* ;
binaryExpression : multExpression (('+' | '-') multExpression)* ;
multExpression : notExpression ('*' notExpression)* ;
notExpression : ('!' | '-')* term ;


//Terms
term : IDENTIFIER | NUM | 'true' | 'false' | '(' expression ')' | functionCall | listAccess;

functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

IDENTIFIER : [a-z_][a-z0-9_]* ;
NUM : '0' | [1-9][0-9]* ;
WS : [\t\r\n]+ -> skip ; // Ignore/skip whitespace