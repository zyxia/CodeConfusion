using System;
using System.Collections.Generic;
using System.IO;
using MathParser;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Test.TokenParser
{
    public class TreeBuild
    {
        [Test]
        public  void BuildTest()
        {
            string value=  "(sin(x)-cos(y))";
            TestMathFormat(value);
            value=  "(sin(x)-cos(y-x))";
            TestMathFormat(value);
            value=  "(sin(x)-cos(y*x))";
            TestMathFormat(value);
            value=  "(sin(x)-cos(x*x))";
            TestMathFormat(value);
        }

        public void TestMathFormat(string value)
        { 
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine(value);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            var scanner = new Scanner(stream);
            var parser = new Parser(scanner);
            Assert.IsTrue(parser.Parse());
            var root = parser.GetRootNode();
            
            /*var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };
            var paramList = new List<string>();
            root.GetParamList( paramList);
            int pCount = paramList.Count;
            Assert.True(pCount == 2);
            Console.WriteLine( Newtonsoft.Json.JsonConvert.SerializeObject(root,settings));;
            writer.Dispose();
            stream.Dispose();*/
            
            root.ToFunction(out var function);
            var functionD = function.GetDerivative("x");
            functionD = functionD.Reduce();
            Console.WriteLine("the x partial  derivative of "+value+" is \n"+functionD.ToString());
        }
    }
}