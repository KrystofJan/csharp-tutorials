using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ConsoleApp1 // Note: actual namespace depends on the project name.
{
	internal class Program
	{
		static void Main(string[] args)
		{
			String input = File.ReadAllText("input.txt");
			AntlrInputStream stream = new AntlrInputStream(input);
			PLC_Lab7_exprLexer lexer = new PLC_Lab7_exprLexer(input);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			PLC_Lab7_exprParser parser = new PLC_Lab7_exprParser(tokens);
			IParseTree tree = parser.StartRule();
		}
	}
}