using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Operators {
	public class Program {
		public static void Main(string[] args) {
			Vector3 one = new Vector3(1, 2, 3);
			Vector3 two = one * 2; // operator*
			Vector3 three = one + two; // operator+
			if (three == one) { // operator==
				Console.WriteLine("This won't happen");
			}
			// etc.
		}
	}
}
