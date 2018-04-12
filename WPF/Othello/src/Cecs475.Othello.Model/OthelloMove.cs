using System;
using System.Collections.Generic;

namespace Cecs475.Othello.Model {
	/// <summary>
	/// Represents a single move that can be or has been applied to an OthelloBoard object.
	/// </summary>
	public class OthelloMove : IEquatable<OthelloMove> {
		public int Player { get; set; }

		/// <summary>
		/// The position of the move.
		/// </summary>
		public BoardPosition Position { get; }

		/// <summary>
		/// Initializes a new OthelloMove instance representing the given board position.
		/// </summary>
		public OthelloMove(BoardPosition pos) {
			Position = pos;
		}

		public OthelloMove(int player, BoardPosition pos) {
			Player = player;
			Position = pos;
		}

		public override bool Equals(object obj) {
			return Equals(obj as OthelloMove);
		}

		/// <summary>
		/// Returns true if the two objects have the same position.
		/// </summary>
		public bool Equals(OthelloMove other) {
			return other != null && Position.Equals(other.Position);
		}
		
		/// <summary>
		/// True if the move represents a "pass".
		/// </summary>
		public bool IsPass =>
			Position.Row == -1 && Position.Col == -1;


		// For debugging.
		public override string ToString() {
			return Position.ToString();
		}

		public override int GetHashCode() {
			var hashCode = 1257125194;
			hashCode = hashCode * -1521134295 + Player.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<BoardPosition>.Default.GetHashCode(Position);
			return hashCode;
		}
	}
}
