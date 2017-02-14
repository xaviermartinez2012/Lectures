using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinqIntegrated {
	public class Program {
		// Half of LINQ is the .NET library for extending IEnumerable classes with LINQ functions to map, filter, and reduce.
		// The second half is a new sublanguage integrated into C#.

		public class Movie {
			private long mEarnings; // I'll explain this later...


			public string Title { get; set; }
			public int Year { get; set; }
			public long Budget { get; set; }
			public int RunningTime { get; set; }
			public string ProductionCompany { get; set; }

			public long Earnings {
				get {
					// This is only to demonstrate something very interesting...
					Console.WriteLine($"Reading Earnings of {Title}");
					return mEarnings;
				}
				set { mEarnings = value; }
			}
		}

		public static void Main(string[] args) {

			// Given a collection of values...
			Movie[] movies = new Movie[] {
				new Movie() {Title = "Wall-E", Year = 2008, ProductionCompany = "Pixar Animation Studios", Budget = 180000000,
					Earnings = 521300000, RunningTime = 98 }, // WARNING new syntax
				new Movie() {Title = "Finding Nemo", Year = 2003, ProductionCompany = "Pixar Animation Studios", Budget = 94000000,
					Earnings = 936700000, RunningTime = 100 },
				new Movie() {Title = "The Lion King", Year = 1994, ProductionCompany = "Walt Disney Feature Animation", Budget = 45000000,
					Earnings = 987500000, RunningTime = 98 },
				new Movie() {Title = "Finding Dory", Year = 2016, ProductionCompany = "Pixar Animation Studios", Budget = 200000000,
					Earnings = 1028000000, RunningTime = 97 },
				new Movie() {Title = "The Empire Strikes Back", Year = 1980, ProductionCompany = "Lucasfilm Ltd.", Budget = 33000000,
					Earnings = 538400000, RunningTime = 124 },
			};

			Console.WriteLine("Finding onlyPixarMovies \n");
			// We can use LINQ to "query" this data to create sequences only containing certain elements from the original...
			var onlyPixarMovies =
				from m in movies // introduce a variable to iterate over the given existing collection
				where m.ProductionCompany == "Pixar Animation Studios" // introduce an optional filter on the variable
				select m // use a mapping function to select the final data
			;
			// onlyPixarMovies is IEnumerable<Movie> containing only the elements of movies that passed the where clause.

			// The above compiles to this:
			var onlyPixarMovies2 = movies.Where(m => m.ProductionCompany == "Pixar Animation Studios").Select(m => m); // the last Select is unnecessary


			// Describe these sequences, and translate to their compiled form.
			Console.WriteLine("Finding nonPixarTitles \n");
			var nonPixarTitles =
				from m in movies
				where m.ProductionCompany != "Pixar Animation Studios"
				select m.Title;



			Console.WriteLine("Finding shortMovies  \n");
			var shortMovies =
				from m in movies
				where m.RunningTime < 100
				select m;



			Console.WriteLine("Finding profitMargins  \n");
			var profitMargins =
				from m in movies
				select (double)m.Earnings / m.Budget;


			// orderby: give a property to sort the resulting data by
			Console.WriteLine("Finding pixarEarnings  \n");
			var pixarEarnings =
				from m in movies
				where m.ProductionCompany == "Pixar Animation Studios"
				orderby m.Earnings descending // or leave this out for ascending
				select m;



			// We can also use other LINQ functions that don't have immediate support, by calling the method on the result
			// of the query.
			Console.WriteLine("Finding totalPixarEarnings  \n");
			var totalPixarEarnings = (
				from m in movies
				where m.ProductionCompany == "Pixar Animation Studios"
				select m.Earnings
			).Sum();
			Console.WriteLine();

			Console.WriteLine("Finding pixarAverageLengthExceptLongest  \n");
			var pixarAverageLengthExceptLongest = (
				from m in movies
				where m.ProductionCompany == "Pixar Animation Studios"
				orderby m.RunningTime descending
				select m.RunningTime
			).Skip(1).Average();



			Console.WriteLine("Finding pixarBestProfitMargin  \n");
			var pixarBestProfitMargin = (
				from margin in (
					from m in movies
					where m.ProductionCompany == "Pixar Animation Studios"
					select (double)m.Earnings / m.Budget
				)
				orderby margin
				select margin
			).First();
			

		}
	}
}
