using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action_Func {
	/* Delegates are great, but their implementation in C# 1.0 was annoying.
	 * Every time you wanted to take a function as a parameter, you had to 
	 * declare a new delegate type.
	 */
	public delegate void PrintAnIntFunction(int param);

	public class Program {
		public static void PrintInt(int p) {
			Console.WriteLine(p);
		}

		public static void Main(string[] args) {
			PrintAnIntFunction p = PrintInt;
			p(10);

			/* But that's tedious and creates way too many names. C# 3.5 introduced the
			 * Action<T> and Func<T> delegate types.
			 * Action<T> is a pointer to a void function taking a parameter of type T.
			 * Action<T1, T2, ...Tn> is a void function taking params of types T1, T2, etc.
			 * 
			 * Func<T, TResult> is a function taking a param of type T, returning a value
			 *			of type TResult. 
			 *	Func<T1, T2, ..., TResult> likewise.
			 */

			Action<int> p2 = PrintInt;
			p2(100);

			// Why is that helpful?
		}
	}
}
