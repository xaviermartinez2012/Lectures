using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinqIntro {
	/*
	 * LINQ (Language Integrated Query) is two things:
	 *		- a sublanguage designed by Microsoft to enable SQL-like "querying" of data 
	 *			structures in a non-database language
	 *		- a CLI library for enabling this sublanguage.
	 *		
	 *	LINQ uses the power of extension methods, delegates, and "lazy evaluation" to 
	 *	provide a ton of utility to C# programmers working with data structures.
	 *	
	 *	In this demo we will explore the CLI library for enabling LINQ via the 
	 *	flexible IEnumerable<T> class.
	 */
	public class Program {
		public static void Main(string[] args) {
			/* Every class that inherits from IEnumerable gets a set of LINQ extension
			 * methods as long as you are using System.Linq. We'll look at some now.
			 */

			int[] numbers = new int[] { 8, -6, -7, 5, 3, 0, -9 };
			Console.WriteLine(string.Join(", ", numbers));
			// LINQ has two key functions: Select and Where

			/* 
			 * IEnumerable<T2> Select(Func<T, T2> selector): transforms a sequence of
			 * type T into a sequence of type T2, by applying the selector function to
			 * each element of the original sequence in turn.
			 */
			var abs = numbers.Select(Math.Abs); // what are T and T2?
			Console.WriteLine(string.Join(", ", abs));

			/* Because Select returns another IEnumerable, we can chain a Select call
			 * onto that if we wish.
			 */
			var sqrts = numbers.Select(Math.Abs).Select(Convert.ToDouble)
				.Select(Math.Sqrt);
			// What are T and T2 for each of these Select calls?


			/*
			 * IEnumerable<T> Where(Func<T, bool> predicate): given a predicate function,
			 * filters the IEnumerable to only return elements that satisfy the predicate.
			 */
			var positives = numbers.Where(IsPositive);
			var posRoots = numbers.Where(IsPositive).Select(Convert.ToDouble)
				.Select(Math.Sqrt);

			/*
			 * Writing IsPositive was stupid and tedious. It would be great to be able to
			 * declare an inline anonymous function to do what I want. C# gives us syntax
			 * for that.
			 */
			positives = numbers.Where(v => v > 0);
			/*
			 * => declares a funtion whose argument preceeds the =>, and whose body follows.
			 * If the body of the function is a single expression, you don't need a return
			 * statement. If the body takes multiple statements, you use curly braces as
			 * in a normal method.
			 */
			// Now I can simplify some of these
			posRoots = numbers.Where(v => v > 0).Select(v => Math.Sqrt((double)v));

			// These two functions are the core of LINQ, but there's SO MUCH MORE:
			/* 
			 * T Aggregate(Func<T, T, T>): apply a function to first two elements; then
			 * to the result of the first function and the third element; then
			 * to the result of the second function and the fourth element; etc.
			 */
			var product = numbers.Aggregate((x1, x2) => x1 * x2);
			// With two params, the () are necessary

			var sumOfNegatievs = numbers.Where(x => x < 0).Aggregate((x1, x2) => x1 + x2);

			/* 
			 * bool Any(Func<T, bool> pred): returns true if any of the elements satisfy
			 *		the predicate.
			 */
			bool anyMultOf5 = numbers.Any(x => x % 5 == 0);
			// similarly for All.


			/* 
			 * IEnumerable<T> Skip(int n): returns a new sequence, only using the elements
			 * found after skipping the first n elements of the original sequence.
			 */
			var lastFour = numbers.Skip(4);
			// likewise for Take 

			/*
			 * IEnumerable<T> SkipWhile(Func<T, bool> pred): as Skip, but only skips elements
			 * until finding the first element that fails the predicate.
			 */
			var answer = numbers.SkipWhile(x => x % 2 == 0);

			// These functions, and more, work on any IEnumerable type. Including string!
			string greet = "   Hello, world!";
			var letters = greet.Where(c => char.IsLetter(c));
			var trimmed = greet.SkipWhile(c => c == ' ');
			var firstWord = greet.SkipWhile(c => c == ' ').TakeWhile(c => char.IsLetter(c));
		}

		public static bool IsPositive(int val) {
			return val > 0;
		}
	}
}
