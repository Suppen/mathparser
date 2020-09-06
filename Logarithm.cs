using System;
using System.Collections.Generic;

namespace abstractsyntaxtree {
	
	public class Logarithm : Expression {
		
		private Expression exp1;
		private Expression logbase;
		
		public Logarithm(Expression exp1, Expression logbase) {
			this.exp1 = exp1;
			this.logbase = logbase;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.Log(exp1.evaluate(unknowns))
				// Divide by "logbase" to get the logarithm in the correct base
				/ Math.Log(logbase.evaluate(unknowns));
		}
		
	}
}