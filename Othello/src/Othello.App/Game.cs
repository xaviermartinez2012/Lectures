using System;
using System.Linq;
using Othello.Game;

namespace Othello.App
{
	/// <summary>
	/// This is the controller class of our simple MVC design. All user interaction is done here, driving the View and 
	/// Model classes in response to user input.
	/// </summary>
	public class Game {
		static void Main(string[] args) {
			// The model and view for the game.
			OthelloBoard board = new OthelloBoard();
			OthelloView view = new OthelloView();

			while (true) {
				// Print the view.
				Console.WriteLine();
				Console.WriteLine();
				view.PrintView(Console.Out, board);
				Console.WriteLine();
				Console.WriteLine();

				// Print the possible moves.
				var possMoves = board.GetPossibleMoves();
				Console.WriteLine("Possible moves:");
				Console.WriteLine(String.Join(", ", possMoves));

				// Print the turn indication.
				Console.WriteLine();
				Console.Write("{0}'s turn: ", view.GetPlayerString(board.CurrentPlayer));

				// Parse user input and apply their command.
				string input = Console.ReadLine();
				if (input.StartsWith("move ")) {
					// Parse the move and validate that it is one of the possible moves before applying it.
					OthelloMove move = view.ParseMove(input.Substring(5));
					bool foundMove = false;
					foreach (var poss in possMoves) {
						if (poss.Equals(move)) {
							board.ApplyMove(poss);
							foundMove = true;
							break;
						}
					}
					if (!foundMove) {
						Console.WriteLine("That is not a possible move, please try again.");
					}
				}
				else if (input.StartsWith("undo ")) {
					// Parse the number of moves to undo and repeatedly undo one move.
					int undoCount = Convert.ToInt32(input.Substring(5));
					while (undoCount > 0 && board.MoveHistory.Count > 0) {
						board.UndoLastMove();
						undoCount--;
					}
				}
				else if (input == "showHistory") {
					// Show the move history in reverse order.
					Console.WriteLine("History:");
					bool playerIsBlack = board.CurrentPlayer != 1; 
					// if board.CurrentPlayer == 1, then black is CURRENT player, not the most recent player.

					foreach (var move in board.MoveHistory.Reverse()) {
						Console.WriteLine("{0}: {1}", move.Player == 1 ? "Black" : "White", move);
					}
				}
				else if (input == "showAdvantage") {
					Console.WriteLine("Advantage: {0} in favor of {1}", 
						board.CurrentAdvantage.Advantage, 
						board.CurrentAdvantage.Player == 1 ? "Black" : "White");
				}

			}
		}
	}
}
