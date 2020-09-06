using System.Collections.Generic;

namespace abstractsyntaxtree {

	public class Subtraction : Expression {
		
		private Expression exp1;
		
		public Subtraction(Expression exp1) {
			this.exp1 = exp1;
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return -exp1.evaluate(unknowns);	
		}
		
	}
}