using System.Collections.Generic;
using System;

namespace abstractsyntaxtree {

	public class Round : Expression {
		
		private Expression exp1;
		
		public Round(Expression exp1) {
			this.exp1 = exp1;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.Round(exp1.evaluate(unknowns));
		}
	}
}