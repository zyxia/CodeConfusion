using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeConfusion3.Common;


class StringTokenState
{
    public string textTemp;
     
    public bool isStringEnd()
    {
        if(textTemp.Length>2 && textTemp[textTemp.Length-1] == '\"')
        {
            int count = StringUtil.rCharCount(ref textTemp, textTemp.Length - 2,'\\');
            int re = count % 2;
            if (re == 0)
                return true;
        }
        return false; 
    }

    public void push(char c)
    {
        textTemp += c;
    }


}
 
