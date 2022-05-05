using NUnit.Framework;

namespace Test.TokenParser
{
    public class OperatorTest
    {
        [Test]
        public void Test()
        { 
            Common.ParseTest( "x+y+z");
            Common.ParseTest( "x-y*z/x-z");
        }
        [Test]
        public void Test2()
        { 
            Common.ParseTest( "x-2*z");
            Common.ParseTest( "-2.02*(x-2)*z");
            Common.ParseTest( "-2.7*sin(x-2*z)"); 
        }
        [Test]
        public void Test3()
        {  
            Common.ParseTest( "sin(sin(x)+y-cos(t))");
        }
    }
}