using System;

namespace Events {
	public class Program {
		public static void SayHello(string name) {
			Console.WriteLine($"Hello, {name}!");
		}
		public static void SayGoodbye(string name) {
			Console.WriteLine($"Goodbye, {name}!");
		}

		public static void Main(string[] args) {
			// Delegate in .NET are actually "multicast": they can point to multiple functions at the same time.
			// When the delegate is invoked, each of the functions it points to are called in turn. 
			Action<string> phrases = SayHello;
			// the += operator can append another function of the delegate type
			phrases += SayGoodbye;
			// When we invoke phrases, the two functions will be called synchronously.
			phrases("Neal");
			// Output?


			// What about nonvoid return types? How can we call two functions that both return something, if
			// function calls can only return one value.
			Func<double, double> f = Math.Abs;
			f += Math.Floor;

			// Func<double,double> returns a double when invoked...
			double result = f(-10.5);

			// ... but which function is actually returned?
			Console.WriteLine($"result: {result}");




			// Declare a Stack variable.
			IntStack s = new IntStack();
			// I want to be informed whenever an item is added to this stack, so I "subscribe" to the
			// ItemAdded event.
			s.ItemAdded += Stack_ItemAdded;
			// I also want to know when something is removed.
			s.ItemRemoved += Stack_ItemRemoved;

			// Now when I call s.Push, what happens?
			s.Push(100);
			s.Push(200);
			s.Pop();

			// How is this different than just calling Stack_ItemAdded from within the Stack.Push method?
			// Why bother with this approach?
		}

		public static void Stack_ItemAdded(IntStack stack, int item) {
			Console.WriteLine($"{item} was just added to the stack, which now has {stack.Count} elements");
		}
		public static void Stack_ItemRemoved(IntStack stack, int item) {
			Console.WriteLine($"{item} was just removed from the stack, which now has {stack.Count} elements");
		}
	}
}
