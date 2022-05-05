
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
                                            System.Console.WriteLine(yytext);
                                            return (int)Tokens.SIN;
                                        }
                                        if ( yytext == "cos")
                                        {
                                            System.Console.WriteLine(yytext);
                                            return (int)Tokens.COS;
                                        }
                                        if ( yytext == "virtual")
                                        {
                                            System.Console.WriteLine(yytext);
                                            return (int)Tokens.VIRTUAL;
                                        }
                                        
                                        System.Console.WriteLine(yytext);
                                        yylval =  MakeVariableNode(yytext);
                                        return (int)Tokens.WORLD;
                               }
([1-9][0-9]*\.[0-9]+)|(0\.[0-9]+)|([1-9][0-9]*) {
                                            System.Console.WriteLine(yytext);
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

public abstract class Node
{

}
public class SinNode:Node
{
    Node _child;
    public SinNode(Node child){
        this._child = child;
    }
}
public class CosNode:Node
{
    Node _child;
    public CosNode(Node child){
        this._child = child;
    }
}
public class PlusNode:Node
{
    Node _left;
    Node _right;
    public PlusNode(Node lfs,Node right){
        this._left = lfs;
        this._right = right;
    }
}
public class MinusNode:Node
{
      Node _left;
        Node _right;
    public MinusNode(Node lfs,Node right){
         this._left = lfs;
         this._right = right;
    }
}

public class MulNode:Node
{
    Node _left;
      Node _right;
    public MulNode(Node lfs,Node right){
         this._left = lfs;
         this._right = right;
    }
}
public class DivideNode:Node
{
    Node _left;
      Node _right;
    public DivideNode(Node lfs,Node right){
         this._left = lfs;
         this._right = right;
    }
}
public class VariableNode:Node
{
    string _paramName;
    public VariableNode(string param){
        this._paramName = param;
    }
}
public class ConstNode:Node
{
    float _value;
    public ConstNode(string param){
        this._value = float.Parse(param);
    }
}

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