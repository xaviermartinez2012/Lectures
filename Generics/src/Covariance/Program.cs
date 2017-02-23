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
		}
	}
}
