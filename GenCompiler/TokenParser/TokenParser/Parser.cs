// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-1F8U1G4
// DateTime: 2022/6/7 22:34:37
// UserName: zyxia
// Input file <.\MathExpr.y - 2022/6/7 22:34:30>

// options: lines report gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace MathParser
{
public enum Tokens {error=2,EOF=3,WORLD=4,NUMBER=5,SIN=6,
    COS=7,VIRTUAL=8,PLUS=9,MINUS=10,MUL=11,DIVIDE=12,
    COMMA=13,LEFT_PARENTHESES=14,RIGHT_PARENTHESES=15,UMINUS=16};

// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<MathParser.Scanner.Node,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public MathParser.Scanner.Node yylval;
  public LexLocation yylloc;
  public ScanObj( int t, MathParser.Scanner.Node val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public partial class Parser: ShiftReduceParser<MathParser.Scanner.Node, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[13];
  private static State[] states = new State[27];
  private static string[] nonTerms = new string[] {
      "list", "expres_s", "$accept", };

  static Parser() {
    states[0] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-1,1,-2,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{9,4,10,6,11,8,12,10,3,-12});
    states[4] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-2,5});
    states[5] = new State(new int[]{9,-3,10,-3,11,8,12,10,3,-3,15,-3});
    states[6] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-2,7});
    states[7] = new State(new int[]{9,-4,10,-4,11,8,12,10,3,-4,15,-4});
    states[8] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-2,9});
    states[9] = new State(-5);
    states[10] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-2,11});
    states[11] = new State(-6);
    states[12] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-2,13});
    states[13] = new State(new int[]{15,14,9,4,10,6,11,8,12,10});
    states[14] = new State(-2);
    states[15] = new State(-7);
    states[16] = new State(-8);
    states[17] = new State(new int[]{14,18});
    states[18] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-2,19});
    states[19] = new State(new int[]{15,20,9,4,10,6,11,8,12,10});
    states[20] = new State(-9);
    states[21] = new State(new int[]{14,22});
    states[22] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-2,23});
    states[23] = new State(new int[]{15,24,9,4,10,6,11,8,12,10});
    states[24] = new State(-10);
    states[25] = new State(new int[]{14,12,4,15,5,16,6,17,7,21,10,25},new int[]{-2,26});
    states[26] = new State(-11);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-3, new int[]{-1,3});
    rules[2] = new Rule(-2, new int[]{14,-2,15});
    rules[3] = new Rule(-2, new int[]{-2,9,-2});
    rules[4] = new Rule(-2, new int[]{-2,10,-2});
    rules[5] = new Rule(-2, new int[]{-2,11,-2});
    rules[6] = new Rule(-2, new int[]{-2,12,-2});
    rules[7] = new Rule(-2, new int[]{4});
    rules[8] = new Rule(-2, new int[]{5});
    rules[9] = new Rule(-2, new int[]{6,14,-2,15});
    rules[10] = new Rule(-2, new int[]{7,14,-2,15});
    rules[11] = new Rule(-2, new int[]{10,-2});
    rules[12] = new Rule(-1, new int[]{-2});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // expres_s -> LEFT_PARENTHESES, expres_s, RIGHT_PARENTHESES
#line 24 ".\MathExpr.y"
        {
            CurrentSemanticValue = ValueStack[ValueStack.Depth-2];
            MathParser.Scanner.Node.Root = CurrentSemanticValue;
        }
#line default
        break;
      case 3: // expres_s -> expres_s, PLUS, expres_s
#line 29 ".\MathExpr.y"
        {
            CurrentSemanticValue = MathParser.Scanner.MakePlusNode(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1]);
           MathParser.Scanner.Node.Root = CurrentSemanticValue;
        }
#line default
        break;
      case 4: // expres_s -> expres_s, MINUS, expres_s
#line 34 ".\MathExpr.y"
        { 
            CurrentSemanticValue = MathParser.Scanner.MakeMinusNode(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1]);
            MathParser.Scanner.Node.Root = CurrentSemanticValue;
        }
#line default
        break;
      case 5: // expres_s -> expres_s, MUL, expres_s
#line 39 ".\MathExpr.y"
        {
            CurrentSemanticValue = MathParser.Scanner.MakeMulNode(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1]);
            MathParser.Scanner.Node.Root = CurrentSemanticValue;
        }
#line default
        break;
      case 6: // expres_s -> expres_s, DIVIDE, expres_s
#line 44 ".\MathExpr.y"
        { 
            CurrentSemanticValue = MathParser.Scanner.MakeDivideNode(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1]);
            MathParser.Scanner.Node.Root = CurrentSemanticValue;
        }
#line default
        break;
      case 9: // expres_s -> SIN, LEFT_PARENTHESES, expres_s, RIGHT_PARENTHESES
#line 51 ".\MathExpr.y"
        {
            CurrentSemanticValue = MathParser.Scanner.MakeSinNode(ValueStack[ValueStack.Depth-2]);
            MathParser.Scanner.Node.Root = CurrentSemanticValue;
        }
#line default
        break;
      case 10: // expres_s -> COS, LEFT_PARENTHESES, expres_s, RIGHT_PARENTHESES
#line 56 ".\MathExpr.y"
        {
            CurrentSemanticValue = MathParser.Scanner.MakeCosNode(ValueStack[ValueStack.Depth-2]);
            MathParser.Scanner.Node.Root = CurrentSemanticValue;
        }
#line default
        break;
      case 12: // list -> expres_s
#line 62 ".\MathExpr.y"
                {yyerrok(); }
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 66 ".\MathExpr.y"
/*
 * All the code is in the helper file RealTreeHelper.cs
 
     
 */ 
#line default
}
}
