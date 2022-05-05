using System;
using System.IO;
using MathParser;

namespace TokenParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = args[0];
            Stream file = new FileStream(filename,FileMode.Open);
            var scanner = new Scanner(file);
            var parser = new Parser(scanner);
            var r = parser.Parse(); 
        }
    }
}