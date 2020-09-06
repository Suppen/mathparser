using System.Collections.Generic;

namespace abstractsyntaxtree {

	public class Multiplication : Expression {
		
		private Expression exp1;
		private Expression exp2;
		
		public Multiplication(Expression exp1, Expression exp2) {
			this.exp1 = exp1;
			this.exp2 = exp2;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return exp1.evaluate(unknowns) * exp2.evaluate(unknowns);
		}
		
	}
}