using abstractsyntaxtree;
using System;
using System.Collections.Generic;
	
public class Test {
	
	public static void Main(String[] args) {
		
		Expression ast = ExpressionParser.parse(Console.ReadLine());
		Dictionary<char, double> unknowns = new Dictionary<char, double>();
		unknowns.Add('x', double.Parse(Console.ReadLine()));
		Console.WriteLine(ast.evaluate(unknowns));
	}
	
}
