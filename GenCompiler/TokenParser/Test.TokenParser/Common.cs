using System.IO;
using MathParser;
using NUnit.Framework;

namespace Test.TokenParser
{
    public static class Common
    {
        public static void ParseTest(string value)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine(value);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            var scanner = new Scanner(stream);
            var parser = new Parser(scanner);
            Assert.IsTrue(parser.Parse());
            writer.Dispose();
            stream.Dispose();
        }
    }
}