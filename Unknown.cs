using System.Collections.Generic;

namespace abstractsyntaxtree {
	
	public class Unknown : Expression {
	
		char unknown;
		
		public Unknown(string unknown) {
			this.unknown = char.Parse(unknown);
		}
		
		public override double evaluate(Dictionary<char, double> unknowns) {
			return unknowns[unknown];
		}
		
	}

}