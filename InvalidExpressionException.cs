using System;

namespace abstractsyntaxtree {

	public class InvalidExpressionException : Exception {
		
		public InvalidExpressionException(string Message) : base(Message) {
			// Intentionally left empty
		}
		
	}
}