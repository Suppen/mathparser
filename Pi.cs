using System.Collections.Generic;
using System;

namespace abstractsyntaxtree {
	
	public class Pi : Expression {
		
		public Pi() {
			// Intentionally left empty
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return Math.PI;
		}
		
	}
}