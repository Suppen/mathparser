using System;
using System.Collections.Generic;

namespace abstractsyntaxtree {
	
	public class Power : Expression {
		
		private Expression exp1;
		private Expression exponent;
		
		public Power(Expression exp1, Expression exponent) {
			this.exp1 = exp1;
			this.exponent = exponent;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.Pow(exp1.evaluate(unknowns), exponent.evaluate(unknowns));
		}
		
	}
}