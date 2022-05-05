using System.IO;
using NUnit.Framework;

namespace Test.TokenParser
{
    public class BasicFunctionTest
    {
    
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Basic1()
        {
            Common.ParseTest("x"); 
        }
        [Test]
        public void Basic2()
        {
            Common.ParseTest("x + x"); 
        }
        [Test]
        public void Linear()
        {
            Common.ParseTest("2*x");  
        }
        [Test]
        public void Number()
        {
            Common.ParseTest("23.0"); 
            Common.ParseTest("23.01"); 
            Common.ParseTest("-23.01");
            Common.ParseTest("0.01");
        }
        [Test]
        public void SinTest()
        {
            Common.ParseTest("sin(x)"); 
        }
        
        [Test]
        public void CosTest()
        {
            Common.ParseTest("cos(x)");  
        }
       
        [Test]
        public void ScaleSinTest()
        {
           Common. ParseTest("2.7*sin(-2.03*x)");  
        }
    }
}