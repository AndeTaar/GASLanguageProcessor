grammar GAS;

//Program
program : canvas (statement)* ;
canvas : 'canvas' '(' NUM ',' NUM ')' ';';

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
// Top left, width and height
rectangleDeclaration : rectangle IDENTIFIER '=' '(' pointTerm ',' NUM ',' NUM ')' ';';
// Top left and side length
squareDeclaration : square IDENTIFIER '=' '(' pointTerm ',' NUM ')' ';';
//Center and radius
circleDeclaration : circle IDENTIFIER '=' '(' pointTerm ',' NUM ')' ';';
//List of points
polygonDeclaration : polygon IDENTIFIER '=' '(' listTerm (',' colourTerm )?')' ';';
//Top left, text, colour and Font-Size
textDecleration : text IDENTIFIER '=' '(' pointTerm ',' stringTerm ',' NUM (',' colourTerm )? ')' ';';

lineDeclaration : line IDENTIFIER '=' lineTerm ';';

colourDeclaration : colour IDENTIFIER '=' colourTerm ';';

stringDecleration : string IDENTIFIER '=' stringTerm ';';


//Collection types
collectionDeclaration : list '<' allTypes '>' IDENTIFIER '=' '{' (expression (',' expression)*)? '}' ';';
list : 'list';
groupDeclaration : 'group' IDENTIFIER '=' '(' pointTerm ',' '{' (expression (',' expression)*)? '}' ')' ';';
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
term : IDENTIFIER | NUM | 'true' | 'false' | '(' expression ')' | pointTerm | pointTerm | colourTerm | listTerm | functionCall | listAccess;
pointTerm : IDENTIFIER | '(' NUM ',' NUM ')' | functionCall;
//Red Green Blue Alpha
colourTerm: IDENTIFIER | '(' NUM ',' NUM ',' NUM ',' NUM ')' | functionCall;
listTerm : IDENTIFIER | '{' (expression (',' expression)*)? '}' | functionCall;
stringTerm : IDENTIFIER |  ALLSTRINGS  | functionCall;
// Start and end points and colour
lineTerm : IDENTIFIER | '(' pointTerm ',' pointTerm (',' colourTerm )? | functionCall;

functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

IDENTIFIER : [a-z_][a-z0-9_]* ;
NUM : '0' | [1-9][0-9]* ;
WS : [ \t\r\n]+ -> skip ; // Ignore/skip whitespace
ALLSTRINGS : '"'[a-zA-Z0-9_]+'"';