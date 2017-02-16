using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cecs475.Generics {
	public class Program {
		public static void Main(string[] args) {
			GenericStack<int> s = new GenericStack<int>();
			s.Push(100);
			s.Push(50);
			s.Push(-12);
			s.Push(37);

			// s is IEnumerable, so I can use foreach:
			foreach (int x in s) {
				Console.WriteLine(x); // FILO order. Does not modify the stack.
			}

			// I can also use LINQ!
			var multiples =
				from v in s
				where v % 10 == 0
				select v;
		}
	}

}
