


%namespace MathParser

%YYSTYPE MathParser.Scanner.Node
%start list
%output=Parser.cs 
%partial 


%token WORLD NUMBER SIN COS VIRTUAL 
%token PLUS MINUS MUL DIVIDE
%token COMMA LEFT_PARENTHESES RIGHT_PARENTHESES

%left PLUS MINUS
%left MUL DIVIDE  
%left UMINUS

%%

 
expres_s  :    LEFT_PARENTHESES expres_s RIGHT_PARENTHESES  
        {
            $$ = $2;
            MathParser.Scanner.Node.Root = $$;
        }
        |   expres_s PLUS expres_s                          
        {
            $$ = MathParser.Scanner.MakePlusNode($1,$3);
           MathParser.Scanner.Node.Root = $$;
        }
        |   expres_s MINUS expres_s                         
        { 
            $$ = MathParser.Scanner.MakeMinusNode($1,$3);
            MathParser.Scanner.Node.Root = $$;
        }
        |   expres_s MUL expres_s                           
        {
            $$ = MathParser.Scanner.MakeMulNode($1,$3);
            MathParser.Scanner.Node.Root = $$;
        }
        |   expres_s DIVIDE expres_s                        
        { 
            $$ = MathParser.Scanner.MakeDivideNode($1,$3);
            MathParser.Scanner.Node.Root = $$;
        }
        |   WORLD                                            //from scanner
        |   NUMBER                                           //from scanner
        |   SIN LEFT_PARENTHESES expres_s RIGHT_PARENTHESES 
        {
            $$ = MathParser.Scanner.MakeSinNode($3);
            MathParser.Scanner.Node.Root = $$;
        } 
        |   COS LEFT_PARENTHESES expres_s RIGHT_PARENTHESES
        {
            $$ = MathParser.Scanner.MakeCosNode($3);
            MathParser.Scanner.Node.Root = $$;
        }
        |   MINUS expres_s %prec UMINUS  
;
list: expres_s  {yyerrok(); };


%%
/*
 * All the code is in the helper file RealTreeHelper.cs
 
     
 */ 
