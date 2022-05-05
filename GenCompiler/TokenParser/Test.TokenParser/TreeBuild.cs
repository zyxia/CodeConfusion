using System;
using System.IO;
using MathParser;
using NUnit.Framework;

namespace Test.TokenParser
{
    public class TreeBuild
    {
        [Test]
        public  void BuildTest()
        {
            string value=  "(x+z)";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine(value);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            var scanner = new Scanner(stream);
            var parser = new Parser(scanner);
            Assert.IsTrue(parser.Parse());
            var root = parser.GetRootNode();
            Console.WriteLine( Newtonsoft.Json.JsonConvert.SerializeObject(root));;
            writer.Dispose();
            stream.Dispose();
        }
    }
}