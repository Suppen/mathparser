using System.Collections.Generic;

namespace abstractsyntaxtree {

	public class Modulo : Expression {
		
		private Expression exp1;
		private Expression exp2;
		
		public Modulo(Expression exp1, Expression exp2) {
			this.exp1 = exp1;
			this.exp2 = exp2;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			double divisor = exp2.evaluate(unknowns);
			double value = exp1.evaluate(unknowns) % divisor;

			if (value < 0)
				value += divisor;

			return value;
		}
		
	}
}
