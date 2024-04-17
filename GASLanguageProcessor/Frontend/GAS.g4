grammar GAS;

//Program
program : canvas (statement)* ;
canvas : 'canvas' '(' NUM ',' NUM ( ',' colourTerm )? ')' ';';

//Statements
statement : declaration | pointDeclaration | squareDeclaration | rectangleDeclaration | circleDeclaration | assignment |
 print | ifStatement | whileStatement | collectionDeclaration | groupDeclaration | functionCall | functionDeclaration |
 polygonDeclaration | textDecleration | lineDeclaration | colourDeclaration | stringDecleration;
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
polygon: 'polygon';
text: 'text';
line: 'line';

//Shape declarations
pointDeclaration : point IDENTIFIER '=' pointTerm ';';
// Top left, width, height, stroke, strokeColour, fillColour
rectangleDeclaration : rectangle IDENTIFIER '=' '(' pointTerm ',' numTerm ',' numTerm ',' numTerm (',' colourTerm)? (',' colourTerm)? ')' ';';
// Top left, side length, stroke, strokeColour, fillColour
squareDeclaration : square IDENTIFIER '=' '(' pointTerm ',' numTerm ',' numTerm (',' colourTerm)? (',' colourTerm)? ')' ';';
//Center, radius  stroke, strokeColour, fillColour
circleDeclaration : circle IDENTIFIER '=' '(' pointTerm ',' numTerm ',' numTerm (',' colourTerm)? (',' colourTerm)? ')' ';';
//List of points
polygonDeclaration : polygon IDENTIFIER '=' '(' listTerm (',' colourTerm )?')' ';';
//Top left, text, Font-Size, Font, colour
textDecleration : text IDENTIFIER '=' '(' pointTerm ',' stringTerm ',' numTerm ',' stringTerm (',' colourTerm )? ')' ';';

lineDeclaration : line IDENTIFIER '=' lineTerm ';';

colourDeclaration : colour IDENTIFIER '=' colourTerm ';';

stringDecleration : string IDENTIFIER '=' stringTerm ';';


//Collection types
collectionDeclaration : list '<' allTypes '>' IDENTIFIER '=' '{' (expression (',' expression)*)? '}' ';';
list : 'list';
groupDeclaration : 'group' IDENTIFIER '=' '(' pointTerm ',' '{' (statement (',' statement)*)? '}' ')' ';';
listAccess : IDENTIFIER '[' expression ']';

//Standard data types
dataType : 'number' | 'bool';
allTypes : dataType | point | rectangle | square | circle | string | text | polygon | colour;
string : 'string';
colour : 'colour' | 'color';

// Expressions
expression : equalityExpression ('||' equalityExpression)* ;
equalityExpression : relationExpression (('==' | '!=') relationExpression)* ;
relationExpression : binaryExpression ('<' binaryExpression)* ;
binaryExpression : multExpression (('+' | '-') multExpression)* ;
multExpression : notExpression ('*' notExpression)* ;
notExpression : ('!' | '-')* term ;


//Terms
numTerm: NUM | IDENTIFIER | functionCall | listAccess;
term : IDENTIFIER | numTerm | 'true' | 'false' | '(' expression ')' | pointTerm | pointTerm | colourTerm | listTerm | functionCall | listAccess;
pointTerm : IDENTIFIER | '(' numTerm ',' numTerm ')' | functionCall | listAccess;
//Red Green Blue Alpha
colourTerm: IDENTIFIER | '(' numTerm ',' numTerm ',' numTerm ',' numTerm ')' | functionCall | listAccess;
listTerm : IDENTIFIER | '{' (expression (',' expression)*)? '}' | functionCall;
stringTerm : IDENTIFIER |  ALLSTRINGS  | functionCall | listAccess;
// Start and end points and colour
lineTerm : IDENTIFIER | '(' pointTerm ',' pointTerm ',' numTerm (',' colourTerm )? ')' | functionCall;

functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

IDENTIFIER : [a-zA-Z_][a-zA-Z0-9_]* ;
NUM : '0' | [-]?[1-9][0-9]* ;
ALLSTRINGS : '"' (~["\\] | '\\' .)* '"';
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
