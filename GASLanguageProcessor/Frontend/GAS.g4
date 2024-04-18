grammar GAS;

//Program
program : canvas (statement)* ;
canvas : 'canvas' '(' NUM ',' NUM ( ',' colourTerm )? ')' ';';

//Statements
statement : declaration | assignment | print | ifStatement | whileStatement | collectionDeclaration | groupDeclaration |
 functionCall | functionDeclaration;

declaration : allTypes IDENTIFIER ';' | allTypes IDENTIFIER '=' expression ';';
assignment : IDENTIFIER '=' expression ';';
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' ('else' '{' (statement)* '}')?;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
print : 'print' expression ';';
functionDeclaration : allTypes IDENTIFIER '(' (allTypes IDENTIFIER (',' allTypes IDENTIFIER)*)? ')' '{' (statement)* '}' ;

//Collection types
collectionDeclaration : list '<' allTypes '>' IDENTIFIER '=' '{' (expression (',' expression)*)? '}' ';';
list : 'list';
groupDeclaration : 'group' IDENTIFIER '=' '(' pointTerm ',' '{' (statement (',' statement)*)? '}' ')' ';';
listAccess : IDENTIFIER '[' expression ']';

//Standard data types
allTypes: 'number' | 'bool' | 'point' | 'rectangle' | 'square' | 'circle' | 'polygon' | 'text' | 'colour' | 'list' | 'group' | 'string' | 'line';

// Expressions
expression : equalityExpression ('||' equalityExpression)* ;
equalityExpression : relationExpression (('==' | '!=') relationExpression)* ;
relationExpression : binaryExpression ('<' binaryExpression)* ;
binaryExpression : multExpression (('+' | '-') multExpression)* ;
multExpression : notExpression ('*' notExpression)* ;
notExpression : ('!' | '-')* term ;


//Terms
numTerm: NUM | IDENTIFIER | functionCall | listAccess;
boolTerm : 'true' | 'false' | IDENTIFIER | functionCall | listAccess;
term : IDENTIFIER | numTerm | boolTerm | '(' expression ')' | pointTerm | pointTerm | colourTerm | listTerm |
 functionCall | listAccess | stringTerm | lineTerm | squareTerm | polygonTerm | circleTerm | rectangleTerm | textTerm;

pointTerm : IDENTIFIER | 'point(' numTerm ',' numTerm ')' | listAccess | functionCall;
//Red Green Blue Alpha
colourTerm: IDENTIFIER | 'colour(' numTerm ',' numTerm ',' numTerm ',' numTerm ')' | functionCall | listAccess;
listTerm : IDENTIFIER | '{' (expression (',' expression)*)? '}' | functionCall;
stringTerm : IDENTIFIER |  ALLSTRINGS  | functionCall | listAccess;
// Start and end points and colour
lineTerm : IDENTIFIER | 'line(' pointTerm ',' pointTerm ',' numTerm (',' colourTerm )? ')' | functionCall;
// Top left, width, height, stroke, strokeColour, fillColour
squareTerm: IDENTIFIER | 'square(' pointTerm ',' numTerm ',' numTerm (',' colourTerm )? (',' colourTerm)? ')' | functionCall;
// Colour, list of points
polygonTerm : IDENTIFIER | 'polygon(' colourTerm ',' listTerm ')' | functionCall;
// Centre, radius, stroke, strokeColour, fillColour
circleTerm : IDENTIFIER | 'circle(' pointTerm ',' numTerm ',' numTerm (',' colourTerm)? (',' colourTerm)? ')' | functionCall;
// Top left, width, height, stroke, strokeColour, fillColour
rectangleTerm : IDENTIFIER | 'rectangle(' pointTerm ',' numTerm ',' numTerm ',' numTerm (',' colourTerm)? (',' colourTerm)? ')' | functionCall;
// Text, point, size, font, colour
textTerm : IDENTIFIER | 'text(' stringTerm ',' pointTerm ',' numTerm ',' stringTerm (',' colourTerm)? ')' | functionCall;


functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [-]?[1-9][0-9]* ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
