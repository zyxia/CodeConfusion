using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CodeConfusion.CSharpGrammar;

namespace CodeConfusion
{
    namespace a
    {

    }
    enum EnumTest
    {
        a,
        b,
        c
    }
    public  class Program2
    { 
        public static int i = 0;
        public static int i2;
        //public static int i3 = testFunc();
        float test = 0.0f;


        //float a = i * i2 * i3;
        int b = 1 & 2;
        bool c = true && false;
        bool d = true || false;
        int e = 1 | 2;
        int e1 = 1 + 2;
        int e2 = 1 * 2;
        int e3 = 1 / 2;
        int e4 = 1 - 2;

        public static int testFunc(int p1,int p2)
        {
            if (p1 > p2)
            {
                return 1;
            }
            else
                return 0;
            return 0;
        }

    }

}
