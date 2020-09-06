using System.Collections.Generic;
using System;

namespace abstractsyntaxtree {

	public class Floor : Expression {
		
		private Expression exp1;
		
		public Floor(Expression exp1) {
			this.exp1 = exp1;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.Floor(exp1.evaluate(unknowns));
		}
	}
}