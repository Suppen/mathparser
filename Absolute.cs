using System.Collections.Generic;
using System;

namespace abstractsyntaxtree {

	public class Absolute : Expression {
		
		private Expression exp1;
		
		public Absolute(Expression exp1) {
			this.exp1 = exp1;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.Abs(exp1.evaluate(unknowns));
		}
	}
}