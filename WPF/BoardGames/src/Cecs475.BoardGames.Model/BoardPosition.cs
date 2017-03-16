using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cecs475.BoardGames
{
	/// <summary>
	/// Represents a row/column position on a 2D grid board.
	/// </summary>
	public struct BoardPosition : IEquatable<BoardPosition> {
		/// <summary>
		/// The row of the position.
		/// </summary>
		public int Row { get; set; }
		/// <summary>
		/// The column of the position.
		/// </summary>
		public int Col { get; set; }

		public BoardPosition(int row, int col) {
			Row = row;
			Col = col;
		}

		/// <summary>
		/// Translates the BoardPosition by the given amount in the row and column directions, returning a new
		/// position.
		/// </summary>
		/// <param name="rDelta">the amount to change the new position's row by</param>
		/// <param name="cDelta">the amount to change the new position's column by</param>
		/// <returns>a new BoardPosition object that has been translated from the source</returns>
		public BoardPosition Translate(int rDelta, int cDelta) {
			return new BoardPosition(Row + rDelta, Col + cDelta);
		}

		// An overridden ToString makes debugging easier.
		public override string ToString() {
			return "(" + Row + ", " + Col + ")";
		}

		/// <summary>
		/// Two board positions are equal if they have the same row and column.
		/// </summary>
		/// <param name="other"></param>
		public bool Equals(BoardPosition other) {
			return Row == other.Row && Col == other.Col;
		}
	}
}
