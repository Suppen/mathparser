using System.Collections.Generic;
using System;

namespace abstractsyntaxtree {

	public class Cosinus : Expression {
		
		private Expression exp1;
		bool useRadians;
		
		public Cosinus(Expression exp1, bool useRadians = true) {
			this.exp1 = exp1;
			this.useRadians = useRadians;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
		if (useRadians)
			return Math.Cos(exp1.evaluate(unknowns));
		else
			return Math.Cos(exp1.evaluate(unknowns)*Math.PI/180);
		}
		
	}
}