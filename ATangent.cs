using System;
using System.Collections.Generic;

namespace abstractsyntaxtree {

	public class ATangent : Expression {
		
		private Expression exp1;
		private bool useRadians;
		
		public ATangent(Expression exp1, bool useRadians = true) {
			this.exp1 = exp1;
			this.useRadians = useRadians;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
		if (useRadians)
			return Math.Atan(exp1.evaluate(unknowns));
		else
			return Math.Atan(exp1.evaluate(unknowns))*180/Math.PI;
		}
		
	}
}