using System.Collections.Generic;
using System;

namespace abstractsyntaxtree {
	
	public class E : Expression {
		
		public E() {
			// Intentionally left empty
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.E;
		}
		
	}
}