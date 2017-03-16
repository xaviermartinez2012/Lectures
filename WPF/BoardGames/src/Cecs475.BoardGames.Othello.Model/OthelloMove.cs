using System;
using System.Collections.Generic;

namespace Cecs475.BoardGames.Othello {
	/// <summary>
	/// Represents a single move that can be or has been applied to an OthelloBoard object.
	/// </summary>
	public class OthelloMove : IGameMove, IEquatable<OthelloMove> {
		/// <summary>
		/// A record of pieces that were flipped in a particular direction when an OthelloMove was applied.
		/// </summary>
		public struct FlipSet {
			/// <summary>
			/// The row direction that pieces were flipped in.
			/// </summary>
			public int RowDelta { get; set; }
			/// <summary>
			/// The column direction that pieces were flipped in.
			/// </summary>
			public int ColDelta { get; set; }
			/// <summary>
			/// The number of pieces that were flipped in the recorded direction.
			/// </summary>
			public int Count { get; set; }
		}

		/// <summary>
		/// True if the move represents a "pass".
		/// </summary>
		public bool IsPass {
			get { return Position.Row == -1 && Position.Col == -1; }
		}

		/// <summary>
		/// The position of the move.
		/// </summary>
		public BoardPosition Position { get; private set; }
		
		// Our list of flips that were recorded when this move was applied.
		private List<FlipSet> mFlipSets;

		/// <summary>
		/// Initializes a new OthelloMove instance representing the given board position.
		/// </summary>
		public OthelloMove(BoardPosition pos) {
			Position = pos;
			mFlipSets = new List<FlipSet>();
		}

		/// <summary>
		/// Returns true if the two objects have the same position.
		/// </summary>
		public bool Equals(IGameMove obj) {
			OthelloMove other = obj as OthelloMove;
			return other.Position.Row == this.Position.Row && other.Position.Col == this.Position.Col;
		}

		public bool Equals(OthelloMove other) {
			return other.Position.Row == this.Position.Row && other.Position.Col == this.Position.Col;
		}

		// Any time you override Equals you should also override GetHashCode, which is used in hashing data structures
		// to find hash buckets for an object.
		public override int GetHashCode() {
			return Position.GetHashCode();
		}

		public override string ToString() {
			return Position.ToString();
		}

		/// <summary>
		/// Records a FlipSet for the move.
		/// </summary>
		public void AddFlipSet(FlipSet f) {
			mFlipSets.Add(f);
		}

		/// <summary>
		/// A sequence of FlipSet objects representing the different directions of flips made when this move was applied.
		/// </summary>
		public IEnumerable<FlipSet> FlipSets { get { return mFlipSets; } }

		

	}
}
