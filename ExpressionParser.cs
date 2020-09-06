using abstractsyntaxtree;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public static class ExpressionParser {
	
	// Reference to the game manager
#if !TESTING
	private static NewAndBetterGameManager gm = NewAndBetterGameManager.getGameManager();
#endif
	
	// List of all static tokens
	private static readonly string[] possibleTokens = {	// NOT MULTIPLE-UNKOWN COMPATIBLE
		"floor",
		"round",
		"sqrt",
		"ceil",
		"asin",
		"acos",
		"atan",
		"log",
		"sin",
		"cos",
		"tan",
		"mod",
		"abs",
		"pi",
		"e",
		"x",
		"y",
		"+",
		"-",
		"*",
		"/",
		"^"
	};
	
	private static readonly List<string> unaryOperators = new List<string>() {
		"-",
		"sin",
		"cos",
		"tan",
		"log",
		"abs",
		"asin",
		"acos",
		"atan",
		"sqrt",
		"ceil",
		"floor",
		"round"
	};

	// Makes an AST from an expression
	public static Expression parse(string expression) {
#if TESTING
	Console.WriteLine("Pre : " + expression);
#endif
		// Make the string ready for parsing
		expression = expression.ToLower();							// Make it all lower case
		expression = Regex.Replace(expression, "\\s", "");					// Remove whitespace
		expression = Regex.Replace(expression, ".*=", "");					// Remove everything in front of =
		expression = Regex.Replace(expression, ",", ".");					// , -> .
		expression = Regex.Replace(expression, "ln", "log");					// ln -> log
		expression = Regex.Replace(expression, "(^|[^0-9])\\.([0-9]+)", "${1}0.$2");		// .a -> (0.a)
		expression = Regex.Replace(expression, "((?:\\d)+(?:\\.)?(?:\\d)*)(x|y)", "$1*$2");	// ax -> a*x
		expression = Regex.Replace(expression, "(x|y)((?:\\d)+(?:\\.)?(?:\\d))", "$1*$2");	// xa -> x*a
		expression = Regex.Replace(expression, "(\\d|x|y)([a-z])([^(od)])", "$1*$2$3");		// ab -> a*b
		expression = Regex.Replace(expression, "(x|y)(x|y)", "$1*$2");				// xy -> x*y	NOT MULTIPLE-UNKOWN COMPATIBLE
		expression = Regex.Replace(expression, "(x|y)(x|y)", "$1*$2");				// xy -> x*y	Twice to fix a bug
		expression = Regex.Replace(expression, "([exy0-9]|pi)\\(", "$1*(");			// a( -> a*(	NOT MULTIPLE-UNKOWN COMPATIBLE
		expression = Regex.Replace(expression, "\\)([a-z0-9](?!od))", ")*$1");			// )a -> )*a	NOT MULTIPLE-UNKOWN COMPATIBLE
		expression = Regex.Replace(expression, "\\)\\(", ")*(");				// )( -> )*(
		expression = Regex.Replace(expression, "([^^(*+/])-", "$1+-");				// - -> +-
		expression = Regex.Replace(expression, "-", "(-1)*");					// - -> (-1)*
#if TESTING
	Console.WriteLine("Post: " + expression);
#endif
	
		// Don't crash if there is no expression
		if (expression.Length == 0) {
			expression = "0";
		}
	
		List<List<string>> tokenlists = tokenize(expression);
#if TESTING
		for (int i = 0; i < tokenlists.Count; i++) {//TESTING
			Console.Write(i + ": ");//TESTING
			printList(tokenlists[i]);//TESTING
		}//TESTING
#endif
		
		return makeAST(ref tokenlists, tokenlists[0]);
	}
	
	// Tokenizes an expression
	private static List<List<string>> tokenize(string expression) {
		List<List<string>> MicrosoftSucks = new List<List<string>>();
		return tokenize(expression, ref MicrosoftSucks);
	}
	
	// Tokenizes an expression
	private static List<List<string>> tokenize(string expression, ref List<List<string>> tokenlists) {	// NOT MULTIPLE-UNKOWN COMPATIBLE
		List<string> tokenlist = new List<string>();			// Create the current tokenlist
		tokenlists.Add(tokenlist);								// Add it to the list of tokenlists
		
		// Make the tokens
		int i = 0;
		while (i < expression.Length) {
		
			bool tokenFound = false;
			
			// Loop through all possible tokens
			for (int j = 0; j < possibleTokens.Length && !tokenFound; j++) {
				
				// Check if this is an operator
				try {
					if (expression.Substring(i, possibleTokens[j].Length).Equals(possibleTokens[j])) {
						tokenlist.Add(possibleTokens[j]);
						
						i += possibleTokens[j].Length;
						tokenFound = true;
					}
				} catch (ArgumentOutOfRangeException) {
					// Intentionally left empty
				}
			}
			
			// Check if this is a number
			if (!tokenFound && (Char.IsDigit(expression[i]) || expression[i] == '.')) {
				Match possibleNumber = Regex.Match(expression.Substring(i), "^(\\d)+(\\.(\\d)+)?(?!\\.)");
				
				// Check if it matched something
				if (possibleNumber.Success) {
					string number = possibleNumber.Value;
					tokenlist.Add(number);
					
					i += number.Length;
					tokenFound = true;
				}
			}
			
			// Check if this is a parenthesis
			if (!tokenFound && expression[i] == '(') {
				tokenlist.Add("ref" + tokenlists.Count);
				
				// Tokenize the parenthesis
				int closeParen = findMatchingCloseParen(expression.Substring(i));
				tokenize(expression.Substring(i+1, closeParen-1), ref tokenlists);
				
				i += closeParen+1;
				tokenFound = true;
			}
			
			// Throw an exception if a suitable token was not found
			if (!tokenFound) {
				throw new InvalidExpressionException("Error near " + expression[i]);
			}
		}
	
		// sin(x)^2 and other unary operators becomes sin((x)^2). Should be (sin(x))^2. Does not apply to minus
		//try {
			for (int j = 2; j < tokenlist.Count; j++) {
				if (tokenlist[j].Equals("^") && unaryOperators.Contains(tokenlist[j-2])) {
					// Move back a bit
					j -= 2;
					// Create the reference table
					List<string> powerlist = new List<string>();
					
					// Fill it, and delete the tokens from this list
					powerlist.Add(tokenlist[j]);
					tokenlist.RemoveAt(j);
					powerlist.Add(tokenlist[j]);
					tokenlist.RemoveAt(j);
					
					// Insert new token
					tokenlist.Insert(j++, "ref" + tokenlists.Count);
					
					// Add the new list of tokens
					tokenlists.Add(powerlist);
				}
			}
		//} catch (ArgumentOutOfRangeException) {
		//	throw new InvalidExpressionException("Error near " + expression[i]);
		//}
		
		return tokenlists;
	}
	
	// Send the opening parenthesis in the expression
	private static int findMatchingCloseParen(string expression) {
		// Keep track of how deep we are in the expression
		int depth = 1;
		
		for (int i = 1; i < expression.Length; i++) {
			
			if (expression[i] == '(') {
				depth++;
			} else if (expression[i] == ')') {
				depth--;
			}
			
			// The close-paren is found when depth is 0
			if (depth == 0) {
				return i;
			}
		}
		
		throw new InvalidExpressionException("No matching close parenthesis!");
	}
	
	
	// Builds an AST recursively
	private static Expression makeAST(ref List<List<string>> tokenlists, List<string> currentList) {
		
		// Settings
#if TESTING
		bool radians = true;
		float logbase = (float) Math.E;
#else
		bool radians = gm.usingRadians();
		float logbase = gm.getLogbase();
#endif

		string token = "";
		
		// Find a +
		for (int i = 0; i < currentList.Count; i++) {
			token = currentList[i];
			
			if (token == "+") {
				List<string> left = currentList.GetRange(0, i);
				List<string> right = currentList.GetRange(i+1, currentList.Count-i-1);
				
				return new Addition(makeAST(ref tokenlists, left), makeAST(ref tokenlists, right));
			}
		}
		
		// Find a *
		for (int i = 0; i < currentList.Count; i++) {
			token = currentList[i];
			
			if (token == "*") {
				List<string> left = currentList.GetRange(0, i);
				List<string> right = currentList.GetRange(i+1, currentList.Count-i-1);
				
				return new Multiplication(makeAST(ref tokenlists, left), makeAST(ref tokenlists, right));
			}
		}
		
		// Find a modulo or /
		for (int i = 0; i < currentList.Count; i++) {
			token = currentList[i];
			
			if (token.Equals("mod") || token.Equals("/")) {
				List<string> left = currentList.GetRange(0, i);
				List<string> right = currentList.GetRange(i+1, currentList.Count-i-1);
				
				if (token.Equals("mod")) {
					return new Modulo(makeAST(ref tokenlists, left), makeAST(ref tokenlists, right));
				} else if (token.Equals("/")) {
					return new Division(makeAST(ref tokenlists, left), makeAST(ref tokenlists, right));
				}
			}
		}
		
		// Find a ^
		for (int i = 0; i < currentList.Count; i++) {
			token = currentList[i];
			
			if (token == "^") {
				List<string> left = currentList.GetRange(0, i);
				List<string> right = currentList.GetRange(i+1, currentList.Count-i-1);
				
				return new Power(makeAST(ref tokenlists, left), makeAST(ref tokenlists, right));
			}
		}

		try {
			token = currentList[0];
		} catch (Exception) {
			throw new InvalidExpressionException("Missing a value!");
		}
		
		// Unary operators
		if (unaryOperators.Contains(token)) {
			List<string> rest = currentList.GetRange(1, currentList.Count-1);
			
			if (token.Equals("sin")) {
				return new Sinus(makeAST(ref tokenlists, rest), radians);
			} else if (token.Equals("cos")) {
				return new Cosinus(makeAST(ref tokenlists, rest), radians);
			} else if (token.Equals("log")) {
				return new Logarithm(makeAST(ref tokenlists, rest), new Number(logbase + ""));
			} else if (token.Equals("tan")) {
				return new Tangent(makeAST(ref tokenlists, rest), radians);
			} else if (token.Equals("sqrt")) {
				return new Root(makeAST(ref tokenlists, rest), new Number("2"));
			} else if (token.Equals("-")) {
				return new Subtraction(makeAST(ref tokenlists, rest));
			} else if (token.Equals("asin")) {
				return new ASinus(makeAST(ref tokenlists, rest), radians);
			} else if (token.Equals("acos")) {
				return new ACosinus(makeAST(ref tokenlists, rest), radians);
			} else if (token.Equals("atan")) {
				return new ATangent(makeAST(ref tokenlists, rest), radians);
			} else if (token.Equals("abs")) {
				return new Absolute(makeAST(ref tokenlists, rest));
			} else if (token.Equals("ceil")) {
				return new Ceil(makeAST(ref tokenlists, rest));
			} else if (token.Equals("floor")) {
				return new Floor(makeAST(ref tokenlists, rest));
			} else if (token.Equals("round")) {
				return new Round(makeAST(ref tokenlists, rest));
			}
		}
		
		// Numbers
		if (Char.IsDigit(token[0])) {
			return new Number(token);
		}
		
		// Constants
		if (token.Equals("e")) {
			return new E();	
		}
		if (token.Equals("pi")) {
			return new Pi();
		}
		
		// Unknowns		NOT MULTIPLE-UNKOWN COMPATIBLE
		if (token.Equals("x") || token.Equals("y")) {
			return new Unknown(token);
		}
		
		// References
		if (token.Substring(0, 3).Equals("ref")) {
			int reference = Int32.Parse(token.Substring(3));
			return makeAST(ref tokenlists, tokenlists[reference]);
		}
		
		// If it ends up here, it is not a valid expression
		throw new InvalidExpressionException("Could not parse expression");
	}
	
	private static void printList(List<string> list) {
		foreach (string a in list) {
			Console.Write(a + " ");
		}
		Console.WriteLine();
	}

}
