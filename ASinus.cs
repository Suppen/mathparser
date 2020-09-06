using System;
using System.Collections.Generic;

namespace abstractsyntaxtree {

	public class ASinus : Expression {
		
		private Expression exp1;
		private bool useRadians;
		
		public ASinus(Expression exp1, bool useRadians = true) {
			this.exp1 = exp1;
			this.useRadians = useRadians;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
		if (useRadians)
			return Math.Asin(exp1.evaluate(unknowns));
		else
			return Math.Asin(exp1.evaluate(unknowns))*180/Math.PI;
		}
		
	}
}