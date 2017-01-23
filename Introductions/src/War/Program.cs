using System;

namespace Cecs475.War {
	public class Program {
		// The entry point for the application.
		public static void Main(string[] args) {
			// This class has access to any class in the same project, or in an assembly that has been referenced. (Later.)


			// Build a deck for player 1.
			Deck d1 = new Deck();
			// .NET uses formatted strings for output. {0} refers to the first parameter to the method, {1} to the next,
			// etc. If a value is not a string, its ToString is called.
			Console.WriteLine("Player 1's deck (debug): {0}", d1);
			d1.Shuffle();
			Console.WriteLine();
			Console.WriteLine("Player 1's deck shuffled: {0}", d1);

			Deck d2 = new Deck();
			d2.Shuffle();

			Console.WriteLine();
			Console.WriteLine("Let's play WAR!");
			while (d1.Count > 0) { // calls the Count property accessor
				// Deal one card from each deck, compare, and print result.
				Card c1 = d1.DealOne();
				Card c2 = d2.DealOne();

				int compare = c1.CompareTo(c2);
				Console.WriteLine("{0} vs. {1} ... {2}", c1, c2,
					compare == 0 ? "Tie!" :
					compare < 0 ? "Player 2 wins!" :
					"Player 1 wins");

				// Ask to go to next deal.
				string again;
				Console.WriteLine("Continue? y/n:");
				again = Console.ReadLine();
				if (again != "y") { // == and != work for strings! 
					break;
				}
				Console.WriteLine();
			}
		}
	}
}
