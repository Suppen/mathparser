using System.Collections.Generic;
using System;

namespace abstractsyntaxtree {

	public class Ceil : Expression {
		
		private Expression exp1;
		
		public Ceil(Expression exp1) {
			this.exp1 = exp1;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.Ceiling(exp1.evaluate(unknowns));
		}
	}
}