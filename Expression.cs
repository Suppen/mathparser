using System.Collections.Generic;

namespace abstractsyntaxtree {
	
	public abstract class Expression {
		
		public abstract double evaluate(Dictionary<char, double> unknowns);
		
	}
}