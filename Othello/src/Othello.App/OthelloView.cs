using System;
using System.IO;

namespace Othello.Game {
	/// <summary>
	/// Represents a console text-based view of an othello game.
	/// </summary>
	public class OthelloView {
		/// <summary>
		/// Parses a string representation of an OthelloMove in the format "(r, c)".
		/// </summary>
		public OthelloMove ParseMove(string move) {
			string[] split = move.Trim(new char[] { '(', ')' }).Split(',');
			return new OthelloMove(new BoardPosition(Convert.ToInt32(split[0]), Convert.ToInt32(split[1])));
		}

		/// <summary>
		/// Gets a string representing the given player.
		/// </summary>
		public string GetPlayerString(int player) {
			return player == 1 ? "Black" : "White";
		}

		/// <summary>
		/// Prints a text representation of an OthelloBoard to the given TextWriter.
		/// </summary>
		public void PrintView(TextWriter output, OthelloBoard board) {
			output.WriteLine("- 0 1 2 3 4 5 6 7");
			for (int i = 0; i < OthelloBoard.BOARD_SIZE; i++) {
				output.Write("{0} ", i);
				for (int j = 0; j < OthelloBoard.BOARD_SIZE; j++) {
					int space = board.GetPlayerAtPosition(new BoardPosition(i, j));
					output.Write("{0} ", space == 0 ? '.' : space == 1 ? 'B' : 'W');
				}
				output.WriteLine();
			}
		}


	}
}
