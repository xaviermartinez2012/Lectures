using Cecs475.BoardGames;
using Cecs475.BoardGames.Othello;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.TypeDemo {
	public struct DemoStruct {
		public int X { get; set; }
		public int Y { get; set; }
	}
	public class DemoProgram {
		public static void Main(string[] args) {
			// The Assembly class is a logical representation of a physical assembly file.
			// It contains information on the types defined in the assembly, among other things.
			Assembly current = Assembly.GetExecutingAssembly();

			Console.WriteLine($"Assembly: {current.FullName}");
			// the DefinedTypes property is a list of all Types defined in the assembly.
			foreach (Type t in current.DefinedTypes) {
				Console.WriteLine("{0} {1}", t.IsClass ? "class" : "struct", t.FullName);
				var prop = t.GetProperties();
				if (prop.Length > 0) {
					Console.WriteLine("Properties:");
					foreach (PropertyInfo p in prop) {
						Console.WriteLine($"\t{p.PropertyType.Name} {p.Name}");
					}
				}
				Console.WriteLine();
			}

			// We can get a specific Type object with typeof, or by calling .GetType() on
			// an actual object instance.
			Type othelloType = typeof(OthelloBoard);
			Type strType = "Hello".GetType();

			// This gives me an idea... can we find all types that match some criteria, as in,
			// all types that implement IGameBoard?
			

			Type iGameBoard = typeof(IGameBoard);
			var boardTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(t => iGameBoard.IsAssignableFrom(t) && t.IsClass);

			Console.WriteLine("Found these IGameBoard types:");
			Console.WriteLine(string.Join(",\n", boardTypes));

			// Huh... OthelloBoard shows, but not TicTacToeBoard. What gives?
			
			
			// Assembly tttAssembly = Assembly.LoadFrom("lib/Cecs475.BoardGames.TicTacToe.Model.dll");
		}
	}
}
