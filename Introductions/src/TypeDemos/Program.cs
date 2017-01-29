using System;

namespace TypeDemos {
	public class Program {
		public static void PrintObject(object obj) {
			// The C# keyword "object" is an alias for the CLI type Object.
			Console.WriteLine("{0} has HashCode {1}", obj.ToString(), obj.GetHashCode());
		}

		public static void BoxInt(object obj) {
			int value = (int)obj;
			Console.WriteLine("Boxed object: {0}; unboxed int: {1}", obj, value);
		}

		public static void Main(string[] args) {
			// PrintObject's parameter is compatible with any argument type.
			PrintObject(5); // a ValueType of Int32
			PrintObject("Hello"); // a ReferenceType of String

			// demonstrate boxing an int
			BoxInt(10);

			// which of these array assignments are boxed?
			object[] arr = new object[5];
			arr[0] = "Hello";
			arr[1] = 10;
			arr[2] = 'a';
			arr[3] = true;
			arr[4] = new int[1];
		}
	}
}
