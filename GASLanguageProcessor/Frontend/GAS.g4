grammar GAS;

//Program
program : canvas (statement)* ;
canvas : 'canvas' '(' NUM ',' NUM ( ',' colourTerm )? ')' ';';

//Statements
statement : declaration | assignment | print | ifStatement | whileStatement | collectionDeclaration | groupDeclaration |
 functionCall | functionDeclaration;

// (',' identifierTerm ('=' expression)?)* Could be added on this line to allow for multiple declarations on one line
declaration : allTypes identifierTerm ('=' expression)?';';
assignment : identifierTerm '=' expression ';';
ifStatement : 'if' '(' expression ')' '{' (statement)* '}' ('else' '{' (statement)* '}')?;
whileStatement : 'while' '(' expression ')' '{' (statement)* '}';
print : 'print' expression ';';
functionDeclaration : allTypes identifierTerm '(' (allTypes identifierTerm (',' allTypes identifierTerm)*)? ')' '{' (statement)* '}' ;

//Collection types
collectionDeclaration : list '<' allTypes '>' identifierTerm '=' '{' (expression (',' expression)*)? '}' ';';
list : 'list';
groupDeclaration : 'group' identifierTerm '=' '(' pointTerm ',' '{' (statement (',' statement)*)? '}' ')' ';';
listAccess : identifierTerm '[' expression ']';

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
identifierTerm: IDENTIFIER;
numTerm: NUM | identifierTerm | functionCall | listAccess;
boolTerm : 'true' | 'false' | identifierTerm | functionCall | listAccess;
term : identifierTerm | numTerm | boolTerm | '(' expression ')' | pointTerm | pointTerm | colourTerm | listTerm |
 functionCall | listAccess | stringTerm | lineTerm | squareTerm | polygonTerm | circleTerm | rectangleTerm | textTerm;

pointTerm : identifierTerm | 'point(' numTerm ',' numTerm ')' | listAccess | functionCall;
//Red Green Blue Alpha
colourTerm: identifierTerm | 'colour(' numTerm ',' numTerm ',' numTerm ',' numTerm ')' | functionCall | listAccess;
listTerm : identifierTerm | '{' (expression (',' expression)*)? '}' | functionCall;
stringTerm : identifierTerm |  ALLSTRINGS  | functionCall | listAccess;
// Start and end points and colour
lineTerm : identifierTerm | 'line(' pointTerm ',' pointTerm ',' numTerm (',' colourTerm )? ')' | functionCall;
// Top left, width, height, stroke, strokeColour, fillColour
squareTerm: identifierTerm | 'square(' pointTerm ',' numTerm ',' numTerm (',' colourTerm )? (',' colourTerm)? ')' | functionCall;
// Colour, list of points
polygonTerm : identifierTerm | 'polygon(' colourTerm ',' listTerm ')' | functionCall;
// Centre, radius, stroke, strokeColour, fillColour
circleTerm : identifierTerm | 'circle(' pointTerm ',' numTerm ',' numTerm (',' colourTerm)? (',' colourTerm)? ')' | functionCall;
// Top left, width, height, stroke, strokeColour, fillColour
rectangleTerm : identifierTerm | 'rectangle(' pointTerm ',' numTerm ',' numTerm ',' numTerm (',' colourTerm)? (',' colourTerm)? ')' | functionCall;
// Text, point, size, font, colour
textTerm : identifierTerm | 'text(' stringTerm ',' pointTerm ',' numTerm ',' stringTerm (',' colourTerm)? ')' | functionCall;

functionCall : identifierTerm '(' (expression (',' expression)*)? ')';

IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [-]?[1-9][0-9]* ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
