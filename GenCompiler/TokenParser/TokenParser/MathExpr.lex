
%using System.Collections;
%namespace MathParser
%visibility public

%option stack, classes, minimize, parser, verbose, persistbuffer, noembedbuffers, utf8default out:Scanner.cs

%{
        // User code is all now in ScanHelper.cs
%}
 
Ident                [a-zA-Z_][a-zA-Z0-9_]*
RealNum              ([1-9]\d*\.?\d*)|(-?0\.\d*[1-9])
 
//NUMBER SIN COS VIRTUAL  
//PLUS MINUS MUL DIVIDE
%x Ident       
%x RealNum     

// =============================================================
%%  // Start of rules
// =============================================================
 
[a-zA-Z_][a-zA-Z0-9_]*           {
                                        if ( yytext == "sin")
                                        {
                                            //System.Console.WriteLine(yytext);
                                            return (int)Tokens.SIN;
                                        }
                                        if ( yytext == "cos")
                                        {
                                            //System.Console.WriteLine(yytext);
                                            return (int)Tokens.COS;
                                        }
                                        if ( yytext == "virtual")
                                        {
                                            //System.Console.WriteLine(yytext);
                                            return (int)Tokens.VIRTUAL;
                                        }
                                        
                                        //System.Console.WriteLine(yytext);
                                        yylval =  MakeVariableNode(yytext);
                                        return (int)Tokens.WORLD;
                               }
([1-9][0-9]*\.[0-9]+)|(0\.[0-9]+)|([1-9][0-9]*) {
                                            //System.Console.WriteLine(yytext);
                                            yylval =  MakeConstNode(yytext);
                                            return (int)Tokens.NUMBER;
                                        } 
                                         
\,                                      {return (int)Tokens.COMMA;}
\+                                      {return (int)Tokens.PLUS;}
\-                                      {return (int)Tokens.MINUS;}
\*                                      {return (int)Tokens.MUL;}
\/                                      {return (int)Tokens.DIVIDE;}
\(                                      {return (int)Tokens.LEFT_PARENTHESES;}
\)                                      {return (int)Tokens.RIGHT_PARENTHESES;}

// =============================================================
%% // Start of user code //|([1-9]\d*[\.?\d+])|(0\.\d+) 
// =============================================================

  /*  User code is in ParseHelper.cs  */

// =============================================================
 

public static Node MakeSinNode(Node rhs ) {
 return new SinNode( rhs );
}
public static Node MakeCosNode(Node rhs ) {
  return new CosNode( rhs );
}
public static Node MakePlusNode(Node lfs,Node rhs ) {
 return new PlusNode( lfs,rhs );
}
public static Node MakeMinusNode(Node lfs,Node rhs ) {
  return new MinusNode( lfs,rhs );
}
public static Node MakeMulNode(Node lfs,Node rhs ) {
 return new MulNode( lfs,rhs );
}
public static Node MakeDivideNode(Node lfs,Node rhs ) {
  return new DivideNode( lfs,rhs );
}
public static Node MakeVariableNode(string rhs ) {
 return new VariableNode( rhs );
}
public static Node MakeConstNode(string rhs ) {
  return new ConstNode( rhs );
}