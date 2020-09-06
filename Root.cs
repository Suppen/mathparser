using System;
using System.Collections.Generic;

namespace abstractsyntaxtree {
	
	public class Root : Expression {
		
		private Expression exp1;
		private Expression nroot;
		
		public Root(Expression exp1, Expression nroot) {
			this.exp1 = exp1;
			this.nroot = nroot;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.Pow(exp1.evaluate(unknowns), 1/nroot.evaluate(unknowns));
		}
		
	}
}