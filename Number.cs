using System.Collections.Generic;
using System.Globalization;
using System;

namespace abstractsyntaxtree {
	
	public class Number : Expression {
		
		private double exp1;
		
		public Number(string exp1) {
			// Set the right decimal separator
			NumberFormatInfo nfi = new NumberFormatInfo();
			nfi.NumberDecimalSeparator = ".";
			
			if (exp1.Equals("NaN"))
				exp1 = "0";
			else if (exp1.Equals("Infinity"))
				exp1 = Int32.MaxValue + "";
			
			this.exp1 = double.Parse(exp1, nfi);
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return exp1;
		}
		
	}
}
