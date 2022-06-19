using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeConfusion3.Common;

namespace CodeConfusion
{
    public static class Token
    {
        
        public static bool isComment(string strToken)
        {
            if(strToken.Length>2)
            {
                string sub =strToken.Substring(0, 2);
                if (sub == "//" || sub == "/*")
                    return true;
            }
            return false;
        }
        
    }

    class TonkenParser
    {
        static string tempText = "";

        static string invalideStr = " \n\r";
         
        static void push(char c)
        {
            if (!invalideStr.Contains(c))
            {
                tempText += c;
            }
        }

        static bool TempTextSelfIsSeparator()
        {
            if(tempText.Length == 1)
            {
                string srcSeparator = "-+*/%;,()<>=.[]{}!~";
                return srcSeparator.Contains(tempText.Last<char>());
            } 
            return false;
        }

        
        static bool canNextToken(char c)
        {
            string srcSeparator = " \n\r-+*/%;,()<>=[]{}!~";
            return  TempTextSelfIsSeparator() || (tempText.Length>0 && srcSeparator.Contains(c))||(!StringUtil.isInterger(in tempText)&&c=='.');
        }
         

        public static LinkedList<string> Parse(string src)
        {
            LinkedList<string> result = new LinkedList<string>();
            StringTokenState gStringState = null;
            ComentTokenState gCommentState = null;
            int i = 0;
            while (i < src.Length)
            {
                if( gStringState == null && gCommentState == null)
                { 
                    char c = src.ElementAt(i);
                    bool isPreEnd = (i >= src.Length -2);
                    char test = ' ';
                    if (!isPreEnd)
                        test = src.ElementAt(i + 1);
                    if (c == '\"' )
                    {
                        gStringState = new StringTokenState();
                        gStringState.push(c); 
                    }else if(c == '/' && !isPreEnd && (test=='/'||test=='*'))
                    {
                        gCommentState = new ComentTokenState();
                        gCommentState.push(c);
                    }
                    else
                    {
                        if (canNextToken(c))
                        {
                            result.AddLast(tempText);
                            tempText = "";
                            push(c);
                        }
                        else
                        {
                            push(c);
                        }
                    } 
                }
                else if( gStringState != null )
                {
                    char c = src.ElementAt(i);
                    gStringState.push(c);
                    if(gStringState.isStringEnd())
                    {  
                        result.AddLast(gStringState.textTemp);
                        gStringState = null;
                    }
                }else if( gCommentState != null )
                {
                    char c = src.ElementAt(i);
                    gCommentState.push(c);
                    if (gCommentState.isComentEnd())
                    { 
                        result.AddLast(gCommentState.textTemp);
                        gCommentState = null;
                    }
                }
                i += 1;
            }
            return result;
        }
    }
}
