//
//  This CSharp output file generated by Gardens Point LEX
//  Gardens Point LEX (GPLEX) is Copyright (c) John Gough, QUT 2006-2014.
//  Output produced by GPLEX is the property of the user.
//  See accompanying file GPLEXcopyright.rtf.
//
//  GPLEX Version:  1.2.2
//  Machine:  DESKTOP-1F8U1G4
//  DateTime: 2022/6/7 22:37:36
//  UserName: zyxia
//  GPLEX input file <.\MathExpr.lex - 2022/6/7 22:37:12>
//  GPLEX frame file <embedded resource>
//
//  Option settings: verbose, parser, stack, minimize
//  Option settings: classes, noCompressMap, compressNext, persistBuffer, noEmbedBuffers
//

//
// Revised backup code
// Version 1.2.1 of 24-June-2013
//
//
#define BACKUP
#define STACK
#define PERSIST
#define BYTEMODE

using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

using QUT.GplexBuffers;
using System.Collections;

namespace MathParser
{   
    /// <summary>
    /// Summary Canonical example of GPLEX automaton
    /// </summary>
    
#if STANDALONE
    //
    // These are the dummy declarations for stand-alone GPLEX applications
    // normally these declarations would come from the parser.
    // If you declare /noparser, or %option noparser then you get this.
    //

     public enum Tokens
    { 
      EOF = 0, maxParseToken = int.MaxValue 
      // must have at least these two, values are almost arbitrary
    }

     public abstract class ScanBase
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yylex")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yylex")]
        public abstract int yylex();

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yywrap")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yywrap")]
        protected virtual bool yywrap() { return true; }

#if BABEL
        protected abstract int CurrentSc { get; set; }
        // EolState is the 32-bit of state data persisted at 
        // the end of each line for Visual Studio colorization.  
        // The default is to return CurrentSc.  You must override
        // this if you want more complicated behavior.
        public virtual int EolState { 
            get { return CurrentSc; }
            set { CurrentSc = value; } 
        }
    }
    
     public interface IColorScan
    {
        void SetSource(string source, int offset);
        int GetNext(ref int state, out int start, out int end);
#endif // BABEL
    }

#endif // STANDALONE
    
    // If the compiler can't find the scanner base class maybe you
    // need to run GPPG with the /gplex option, or GPLEX with /noparser
#if BABEL
     public sealed partial class Scanner : ScanBase, IColorScan
    {
        private ScanBuff buffer;
        int currentScOrd;  // start condition ordinal
        
        protected override int CurrentSc 
        {
             // The current start state is a property
             // to try to avoid the user error of setting
             // scState but forgetting to update the FSA
             // start state "currentStart"
             //
             get { return currentScOrd; }  // i.e. return YY_START;
             set { currentScOrd = value;   // i.e. BEGIN(value);
                   currentStart = startState[value]; }
        }
#else  // BABEL
     public sealed partial class Scanner : ScanBase
    {
        private ScanBuff buffer;
        int currentScOrd;  // start condition ordinal
#endif // BABEL
        
        /// <summary>
        /// The input buffer for this scanner.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public ScanBuff Buffer { get { return buffer; } }
        
        private static int GetMaxParseToken() {
     System.Reflection.FieldInfo f = typeof(Tokens).GetField("maxParseToken");
            return (f == null ? int.MaxValue : (int)f.GetValue(null));
        }
        
        static int parserMax = GetMaxParseToken();
        
        enum Result {accept, noMatch, contextFound};

        const int maxAccept = 10;
        const int initial = 11;
        const int eofNum = 0;
        const int goStart = -1;
        const int INITIAL = 0;
        const int Ident = 1;
        const int RealNum = 2;

#region user code
#endregion user code

        int state;
        int currentStart = startState[0];
        int code;      // last code read
        int cCol;      // column number of code
        int lNum;      // current line number
        //
        // The following instance variables are used, among other
        // things, for constructing the yylloc location objects.
        //
        int tokPos;        // buffer position at start of token
        int tokCol;        // zero-based column number at start of token
        int tokLin;        // line number at start of token
        int tokEPos;       // buffer position at end of token
        int tokECol;       // column number at end of token
        int tokELin;       // line number at end of token
        string tokTxt;     // lazily constructed text of token
#if STACK          
        private Stack<int> scStack = new Stack<int>();
#endif // STACK

#region ScannerTables
    struct Table {
        public int min; public int rng; public int dflt;
        public sbyte[] nxt;
        public Table(int m, int x, int d, sbyte[] n) {
            min = m; rng = x; dflt = d; nxt = n;
        }
    };

    static int[] startState = new int[] {11, 14, 14, 0};

#region CharacterMap
    static sbyte[] map = new sbyte[256] {
/*     '\0' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\x10' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\x20' */ 0, 0, 0, 0, 0, 0, 0, 0, 10, 11, 8, 6, 5, 7, 4, 9, 
/*      '0' */ 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 
/*      '@' */ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
/*      'P' */ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 
/*      '`' */ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
/*      'p' */ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 
/*   '\x80' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\x90' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\xA0' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\xB0' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\xC0' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\xD0' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\xE0' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
/*   '\xF0' */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
#endregion

    static Table[] NxS = new Table[15] {
/* NxS[   0] */ new Table(0, 0, 0, null), // Shortest string ""
/* NxS[   1] */ // Shortest string "A"
      new Table(1, 3, -1, new sbyte[] {1, 1, 1}),
/* NxS[   2] */ // Shortest string "1"
      new Table(2, 3, -1, new sbyte[] {2, 2, 13}),
/* NxS[   3] */ new Table(0, 0, -1, null), // Shortest string ","
/* NxS[   4] */ new Table(0, 0, -1, null), // Shortest string "+"
/* NxS[   5] */ new Table(0, 0, -1, null), // Shortest string "-"
/* NxS[   6] */ new Table(0, 0, -1, null), // Shortest string "*"
/* NxS[   7] */ new Table(0, 0, -1, null), // Shortest string "/"
/* NxS[   8] */ new Table(0, 0, -1, null), // Shortest string "("
/* NxS[   9] */ new Table(0, 0, -1, null), // Shortest string ")"
/* NxS[  10] */ // Shortest string "0.0"
      new Table(2, 2, -1, new sbyte[] {10, 10}),
/* NxS[  11] */ // Shortest string ""
      new Table(1, 11, -1, new sbyte[] {1, 12, 2, -1, 3, 4, 
          5, 6, 7, 8, 9}),
/* NxS[  12] */ // Shortest string "0"
      new Table(4, 1, -1, new sbyte[] {13}),
/* NxS[  13] */ // Shortest string "0."
      new Table(2, 2, -1, new sbyte[] {10, 10}),
/* NxS[  14] */ new Table(0, 0, -1, null), // Shortest string ""
    };

int NextState() {
    if (code == ScanBuff.EndOfFile)
        return eofNum;
    else
        unchecked {
            int rslt;
            int idx = map[code] - NxS[state].min;
            if (idx < 0) idx += 12;
            if ((uint)idx >= (uint)NxS[state].rng) rslt = NxS[state].dflt;
            else rslt = NxS[state].nxt[idx];
            return rslt;
        }
}

#endregion


#if BACKUP
        // ==============================================================
        // == Nested struct used for backup in automata that do backup ==
        // ==============================================================

        struct Context // class used for automaton backup.
        {
            public int bPos;
            public int rPos; // scanner.readPos saved value
            public int cCol;
            public int lNum; // Need this in case of backup over EOL.
            public int state;
            public int cChr;
        }
        
        private Context ctx = new Context();
#endif // BACKUP

        // ==============================================================
        // ==== Nested struct to support input switching in scanners ====
        // ==============================================================

		struct BufferContext {
            internal ScanBuff buffSv;
			internal int chrSv;
			internal int cColSv;
			internal int lNumSv;
		}

        // ==============================================================
        // ===== Private methods to save and restore buffer contexts ====
        // ==============================================================

        /// <summary>
        /// This method creates a buffer context record from
        /// the current buffer object, together with some
        /// scanner state values. 
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        BufferContext MkBuffCtx()
		{
			BufferContext rslt;
			rslt.buffSv = this.buffer;
			rslt.chrSv = this.code;
			rslt.cColSv = this.cCol;
			rslt.lNumSv = this.lNum;
			return rslt;
		}

        /// <summary>
        /// This method restores the buffer value and allied
        /// scanner state from the given context record value.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        void RestoreBuffCtx(BufferContext value)
		{
			this.buffer = value.buffSv;
			this.code = value.chrSv;
			this.cCol = value.cColSv;
			this.lNum = value.lNumSv;
        } 
        // =================== End Nested classes =======================

#if !NOFILES
     public Scanner(Stream file) {
            SetSource(file); // no unicode option
        }   
#endif // !NOFILES

     public Scanner() { }

        private int readPos;

        void GetCode()
        {
            if (code == '\n')  // This needs to be fixed for other conventions
                               // i.e. [\r\n\205\u2028\u2029]
            { 
                cCol = -1;
                lNum++;
            }
            readPos = buffer.Pos;

            // Now read new codepoint.
            code = buffer.Read();
            if (code > ScanBuff.EndOfFile)
            {
#if (!BYTEMODE)
                if (code >= 0xD800 && code <= 0xDBFF)
                {
                    int next = buffer.Read();
                    if (next < 0xDC00 || next > 0xDFFF)
                        code = ScanBuff.UnicodeReplacementChar;
                    else
                        code = (0x10000 + ((code & 0x3FF) << 10) + (next & 0x3FF));
                }
#endif
                cCol++;
            }
        }

        void MarkToken()
        {
#if (!PERSIST)
            buffer.Mark();
#endif
            tokPos = readPos;
            tokLin = lNum;
            tokCol = cCol;
        }
        
        void MarkEnd()
        {
            tokTxt = null;
            tokEPos = readPos;
            tokELin = lNum;
            tokECol = cCol;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        int Peek()
        {
            int rslt, codeSv = code, cColSv = cCol, lNumSv = lNum, bPosSv = buffer.Pos;
            GetCode(); rslt = code;
            lNum = lNumSv; cCol = cColSv; code = codeSv; buffer.Pos = bPosSv;
            return rslt;
        }

        // ==============================================================
        // =====    Initialization of string-based input buffers     ====
        // ==============================================================

        /// <summary>
        /// Create and initialize a StringBuff buffer object for this scanner
        /// </summary>
        /// <param name="source">the input string</param>
        /// <param name="offset">starting offset in the string</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public void SetSource(string source, int offset)
        {
            this.buffer = ScanBuff.GetBuffer(source);
            this.buffer.Pos = offset;
            this.lNum = 0;
            this.code = '\n'; // to initialize yyline, yycol and lineStart
            GetCode();
        }

        // ================ LineBuffer Initialization ===================
        /// <summary>
        /// Create and initialize a LineBuff buffer object for this scanner
        /// </summary>
        /// <param name="source">the list of input strings</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public void SetSource(IList<string> source)
        {
            this.buffer = ScanBuff.GetBuffer(source);
            this.code = '\n'; // to initialize yyline, yycol and lineStart
            this.lNum = 0;
            GetCode();
        }

#if !NOFILES        
        // =============== StreamBuffer Initialization ==================

        /// <summary>
        /// Create and initialize a StreamBuff buffer object for this scanner.
        /// StreamBuff is buffer for 8-bit byte files.
        /// </summary>
        /// <param name="source">the input byte stream</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public void SetSource(Stream source)
        {
            this.buffer = ScanBuff.GetBuffer(source);
            this.lNum = 0;
            this.code = '\n'; // to initialize yyline, yycol and lineStart
            GetCode();
        }
        
#if !BYTEMODE
        // ================ TextBuffer Initialization ===================

        /// <summary>
        /// Create and initialize a TextBuff buffer object for this scanner.
        /// TextBuff is a buffer for encoded unicode files.
        /// </summary>
        /// <param name="source">the input text file</param>
        /// <param name="fallbackCodePage">Code page to use if file has
        /// no BOM. For 0, use machine default; for -1, 8-bit binary</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public void SetSource(Stream source, int fallbackCodePage)
        {
            this.buffer = ScanBuff.GetBuffer(source, fallbackCodePage);
            this.lNum = 0;
            this.code = '\n'; // to initialize yyline, yycol and lineStart
            GetCode();
        }
#endif // !BYTEMODE
#endif // !NOFILES
        
        // ==============================================================

#if BABEL
        //
        //  Get the next token for Visual Studio
        //
        //  "state" is the inout mode variable that maintains scanner
        //  state between calls, using the EolState property. In principle,
        //  if the calls of EolState are costly set could be called once
        //  only per line, at the start; and get called only at the end
        //  of the line. This needs more infrastructure ...
        //
        public int GetNext(ref int state, out int start, out int end)
        {
                Tokens next;
            int s, e;
            s = state;        // state at start
            EolState = state;
                next = (Tokens)Scan();
            state = EolState;
            e = state;       // state at end;
            start = tokPos;
            end = tokEPos - 1; // end is the index of last char.
            return (int)next;
        }        
#endif // BABEL

        // ======== AbstractScanner<> Implementation =========

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yylex")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yylex")]
        public override int yylex()
        {
            // parserMax is set by reflecting on the Tokens
            // enumeration.  If maxParseToken is defined
            // that is used, otherwise int.MaxValue is used.
            int next;
            do { next = Scan(); } while (next >= parserMax);
            return next;
        }
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        int yypos { get { return tokPos; } }
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        int yyline { get { return tokLin; } }
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        int yycol { get { return tokCol; } }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yytext")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yytext")]
        public string yytext
        {
            get 
            {
                if (tokTxt == null) 
                    tokTxt = buffer.GetString(tokPos, tokEPos);
                return tokTxt;
            }
        }

        /// <summary>
        /// Discards all but the first "n" codepoints in the recognized pattern.
        /// Resets the buffer position so that only n codepoints have been consumed;
        /// yytext is also re-evaluated. 
        /// </summary>
        /// <param name="n">The number of codepoints to consume</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        void yyless(int n)
        {
            buffer.Pos = tokPos;
            // Must read at least one char, so set before start.
            cCol = tokCol - 1; 
            GetCode();
            // Now ensure that line counting is correct.
            lNum = tokLin;
            // And count the rest of the text.
            for (int i = 0; i < n; i++) GetCode();
            MarkEnd();
        }
       
        //
        //  It would be nice to count backward in the text
        //  but it does not seem possible to re-establish
        //  the correct column counts except by going forward.
        //
        /// <summary>
        /// Removes the last "n" code points from the pattern.
        /// </summary>
        /// <param name="n">The number to remove</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        void _yytrunc(int n) { yyless(yyleng - n); }
        
        //
        // This is painful, but we no longer count
        // codepoints.  For the overwhelming majority 
        // of cases the single line code is fast, for
        // the others, well, at least it is all in the
        // buffer so no files are touched. Note that we
        // can't use (tokEPos - tokPos) because of the
        // possibility of surrogate pairs in the token.
        //
        /// <summary>
        /// The length of the pattern in codepoints (not the same as 
        /// string-length if the pattern contains any surrogate pairs).
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yyleng")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yyleng")]
        public int yyleng
        {
            get {
                if (tokELin == tokLin)
                    return tokECol - tokCol;
                else
#if BYTEMODE
                    return tokEPos - tokPos;
#else
                {
                    int ch;
                    int count = 0;
                    int save = buffer.Pos;
                    buffer.Pos = tokPos;
                    do {
                        ch = buffer.Read();
                        if (!char.IsHighSurrogate((char)ch)) count++;
                    } while (buffer.Pos < tokEPos && ch != ScanBuff.EndOfFile);
                    buffer.Pos = save;
                    return count;
                }
#endif // BYTEMODE
            }
        }
        
        // ============ methods available in actions ==============

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal int YY_START {
            get { return currentScOrd; }
            set { currentScOrd = value; 
                  currentStart = startState[value]; 
            } 
        }
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal void BEGIN(int next) {
            currentScOrd = next;
            currentStart = startState[next];
        }

        // ============== The main tokenizer code =================

        int Scan() {
                for (; ; ) {
                    int next;              // next state to enter
#if LEFTANCHORS
                    for (;;) {
                        // Discard characters that do not start any pattern.
                        // Must check the left anchor condition after *every* GetCode!
                        state = ((cCol == 0) ? anchorState[currentScOrd] : currentStart);
                        if ((next = NextState()) != goStart) break; // LOOP EXIT HERE...
                        GetCode();
                    }
                    
#else // !LEFTANCHORS
                    state = currentStart;
                    while ((next = NextState()) == goStart) {
                        // At this point, the current character has no
                        // transition from the current state.  We discard 
                        // the "no-match" char.   In traditional LEX such 
                        // characters are echoed to the console.
                        GetCode();
                    }
#endif // LEFTANCHORS                    
                    // At last, a valid transition ...    
                    MarkToken();
                    state = next;
                    GetCode();                    
#if BACKUP
                    bool contextSaved = false;
                    while ((next = NextState()) > eofNum) { // Exit for goStart AND for eofNum
                        if (state <= maxAccept && next > maxAccept) { // need to prepare backup data
                            // Store data for the *latest* accept state that was found.
                            SaveStateAndPos( ref ctx );
                            contextSaved = true;
                        }
                        state = next;
                        GetCode();
                    }
                    if (state > maxAccept && contextSaved)
                        RestoreStateAndPos( ref ctx );
#else  // BACKUP
                    while ((next = NextState()) > eofNum) { // Exit for goStart AND for eofNum
                         state = next;
                         GetCode();
                    }
#endif // BACKUP
                    if (state <= maxAccept) {
                        MarkEnd();
#region ActionSwitch
#pragma warning disable 162, 1522
    switch (state)
    {
        case eofNum:
            if (yywrap())
                return (int)Tokens.EOF;
            break;
        case 1: // Recognized '[a-zA-Z_][a-zA-Z0-9_]*',	Shortest string "A"
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
            break;
        case 2: // Recognized '([1-9][0-9]*\.[0-9]+)|(0\.[0-9]+)|([1-9][0-9]*)',	Shortest string "1"
        case 10: // Recognized '([1-9][0-9]*\.[0-9]+)|(0\.[0-9]+)|([1-9][0-9]*)',	Shortest string "0.0"
yylval =  MakeConstNode(yytext);
                                            return (int)Tokens.NUMBER;
            break;
        case 3: // Recognized '\,',	Shortest string ","
return (int)Tokens.COMMA;
            break;
        case 4: // Recognized '\+',	Shortest string "+"
return (int)Tokens.PLUS;
            break;
        case 5: // Recognized '\-',	Shortest string "-"
return (int)Tokens.MINUS;
            break;
        case 6: // Recognized '\*',	Shortest string "*"
return (int)Tokens.MUL;
            break;
        case 7: // Recognized '\/',	Shortest string "/"
return (int)Tokens.DIVIDE;
            break;
        case 8: // Recognized '\(',	Shortest string "("
return (int)Tokens.LEFT_PARENTHESES;
            break;
        case 9: // Recognized '\)',	Shortest string ")"
return (int)Tokens.RIGHT_PARENTHESES;
            break;
        default:
            break;
    }
#pragma warning restore 162, 1522
#endregion
                    }
                }
        }

#if BACKUP
        void SaveStateAndPos(ref Context ctx) {
            ctx.bPos  = buffer.Pos;
            ctx.rPos  = readPos;
            ctx.cCol  = cCol;
            ctx.lNum  = lNum;
            ctx.state = state;
            ctx.cChr  = code;
        }

        void RestoreStateAndPos(ref Context ctx) {
            buffer.Pos = ctx.bPos;
            readPos = ctx.rPos;
            cCol  = ctx.cCol;
            lNum  = ctx.lNum;
            state = ctx.state;
            code  = ctx.cChr;
        }
#endif  // BACKUP

        // ============= End of the tokenizer code ================

#if STACK        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal void yy_clear_stack() { scStack.Clear(); }
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal int yy_top_state() { return scStack.Peek(); }
        
        internal void yy_push_state(int state)
        {
            scStack.Push(currentScOrd);
            BEGIN(state);
        }
        
        internal void yy_pop_state()
        {
            // Protect against input errors that pop too far ...
            if (scStack.Count > 0) {
				int newSc = scStack.Pop();
				BEGIN(newSc);
            } // Otherwise leave stack unchanged.
        }
 #endif // STACK

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal void ECHO() { Console.Out.Write(yytext); }
        
#region UserCodeSection

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

#endregion
    } // end class $Scanner


} // end namespace
