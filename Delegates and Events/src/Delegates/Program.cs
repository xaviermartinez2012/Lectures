using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delegates {
	/* A delegate is a type-safe function pointer.
	 * Using other languages as a reference, we can declare a pointer to a function
	 * that takes two int parameters and returns a double as:
	 * 
	 * in C:     double (*ptr)(int, int) = MyFunction;
	 * in C++:   std::function<double(int,int)> ptr = MyFunction;
	 * in F#:    let (ptr : int->int->double) = MyFunction
	 * 
	 * In each language, we can then "call" the variable ptr to execute the function
	 * it points to.
	 * 
	 * In the CLI, function pointers are instances of a class called Delegate.
	 * A Delegate is a type that specifies the return type and parameter types of
	 * a function. You can then declare a variable of the delegate type, and point it
	 * at any function that matches the delegate signature.
	 */
	public delegate double IntIntToDoubleFunction(int first, int second);
	// This declares a type called IntIntToDoubleFunction, allowing me to declare
	// variables of that type. The only valid assignment to such a variable would be
	// to a function that matches the delegate's signature.


	public class Program {
		public static double NthPower(int first, int second) {
			return Math.Pow(first, second);
		}

		public static double NthRoot(int first, int second) {
			return Math.Pow(first, 1.0 / second);
		}

		public static double Product(int first, int second) {
			return first * second;
		}

		public static void Main(string[] args) {
			IntIntToDoubleFunction ptr = NthPower;
			int x = 10, y = 3;
			Console.WriteLine($"{x}^{y} = {ptr(x, y)}"); // whoa.
			ptr = NthRoot;
			Console.WriteLine($"{y}th root of {x} = {ptr(x, y)}");

			// delegates are first class citizens: they can be assigned to variables,
			// passed as parameters, returned from functions.
			double res = ApplyFunction(
				new IntIntToDoubleFunction(Product), // this is the OLD C# syntax
				x, y);

			// Newer versions of C# create the "new IntIntToDoubleFunction" implicitly:
			res = ApplyFunction(Product, x, y);
		}

		public static double ApplyFunction(IntIntToDoubleFunction fn, int x, int y) {
			return fn(x, y);
		}
	}
}
