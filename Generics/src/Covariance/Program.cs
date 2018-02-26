using System;
using System.Collections.Generic;

namespace Cecs475.Generics {
	public interface Animal {
		string Speak();
	}
	public class Iguana : Animal {
		public string Speak() {
			return "Hiss!";
		}
	}
	public class Rat : Animal {
		public string Speak() {
			return "Squeak!";
		}
	}

	public class Program {
		public static void PrintAnimal(Animal a) {
			Console.WriteLine(a.Speak());

		}

		public static void PrintIguana(Iguana a) {
			Console.WriteLine(a.Speak());
		}

		public static void PrintRat(Rat a) {
			Console.WriteLine(a.Speak());
		}

		public static void PrintAnimals(IEnumerable<Animal> a) {
			foreach (Animal x in a) {
				Console.WriteLine(x.Speak());
			}
		}

		public static void PrintRats(IEnumerable<Rat> a) {
			foreach (Rat x in a) {
				Console.WriteLine(x.Speak());
			}
		}

		public static void PrintIguanas(IEnumerable<Iguana> a) {
			foreach (Iguana x in a) {
				Console.WriteLine(x.Speak());
			}
		}

		public static Animal CreateAnimal() => new Iguana();

		public static Iguana CreateIguana() => new Iguana();

		public static Rat CreateRat() => new Rat();

		public static void Main(string[] args) {
			// Suppose I have a few collections of animals.
			Iguana[] igs = new Iguana[] { new Iguana() };
			Rat[] rats = new Rat[] { new Rat() };
			Animal[] animals = new Animal[] { new Iguana(), new Rat() };

			// Which of these function calls is valid?
			/*
			 * PrintAnimal(igs[0]);
			 * PrintAnimal(animals[0]);
			 * PrintIguana(animals[0]);
			 * PrintRat(igs[0]);
			 * PrintRat(rats[0]);
			 * 
			 * Why??
			 */

			// How about these?
			/*
			 * PrintAnimals(animals);
			 * PrintAnimals(igs);
			 * PrintAnimals(rats);
			 * PrintRats(rats);
			 * PrintRats(animals);
			 * 
			 * Why??
			 */


			/* And these???
			 * Action<Animal> a2 = PrintAnimal;
			 * Action<Rat> a2 = PrintRat;
			
			 * Action<Iguana> a1 = PrintAnimal;
			 * Action<Rat> a1 = PrintAnimal;
			 * Action<Animal> a2 = PrintRat;
			*/


			/* Or THESE???
			 * Func<Rat> = CreateRat;
			 * Func<Iguana> = CreateRat;
			 * Func<Animal> = CreateRat;
			 * 
			 */

			// CONCLUSIONS:

			// A collection can be covariant if it is ____________________.
			// A collection can be contravariant if it is ____________________.


			// A function pointer/delegate can be covariant in its ________________.
			// A function pointer/delegate can be contravariant in its ________________.
		}
	}
}
