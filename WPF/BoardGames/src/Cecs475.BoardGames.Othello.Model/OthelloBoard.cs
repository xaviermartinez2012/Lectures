using System.Collections.Generic;

namespace Cecs475.BoardGames.Othello {
	/// <summary>
	/// Implements the board model for a game of othello. Tracks which squares of the 8x8 grid are occupied
	/// by which player, as well as state for the current player and move history.
	/// </summary>
	public class OthelloBoard : IGameBoard {
		public const int BOARD_SIZE = 8;

		// The board is represented by an 8x8 matrix of signed bytes. Each entry represents one square on the board.
		private sbyte[,] mBoard = new sbyte[BOARD_SIZE, BOARD_SIZE];

		// Internally, we will represent pieces for each player as 1 or -1 (for player 2), which makes certain game 
		// operations easier to code. Those values don't make sense to the public, however, so we will expose them in a 
		// public property by mapping -1 to a value of 2. This will reduce coupling between other components and the 
		// private model logic.
		private int mCurrentPlayer;

		/// <summary>
		/// Constructs an othello board in the starting game state.
		/// </summary>
		public OthelloBoard() {
			mCurrentPlayer = 1;
			MoveHistory = new List<IGameMove>();
			mBoard[BOARD_SIZE / 2 - 1, BOARD_SIZE / 2 - 1] = -1;
			mBoard[BOARD_SIZE / 2, BOARD_SIZE / 2] = -1;
			mBoard[BOARD_SIZE / 2, BOARD_SIZE / 2 - 1] = 1;
			mBoard[BOARD_SIZE / 2 - 1, BOARD_SIZE / 2] = 1;
		}

		/// <summary>
		/// The player whose move it is.
		/// </summary>
		public int CurrentPlayer {
			get {
				return mCurrentPlayer == 1 ? 1 : 2;
			}
		}

		/// <summary>
		/// How many "pass" moves have been applied in a row.
		/// </summary>
		public int PassCount {
			get; private set;
		}

		/// <summary>
		/// Gets the current value of the board, as the difference between the number of Black pieces and White pieces.
		/// </summary>
		public int Value {
			get; private set;
		}

		/// <summary>
		/// Gets a list 
		/// </summary>
		public IList<IGameMove> MoveHistory {
			get; private set;
		}

		// This is how we will expose the state of the gameboard in a way that reduces coupling.
		// No one needs to know HOW the data is represented; they simply need to know which player is
		// at which position.
		/// <summary>
		/// Returns an integer representing which player has a piece at the given position, or 0 if the position
		/// is empty.
		/// </summary>
		public int GetPieceAtPosition(BoardPosition boardPosition) {
			sbyte pos = mBoard[boardPosition.Row, boardPosition.Col];
			if (pos == -1) { // -1 maps to player 2.
				return 2;
			}
			return pos; // otherwise the value is correct
		}

		/// <summary>
		/// Applies the given move to the board state.
		/// </summary>
		/// <param name="m">a move that is assumed to be valid</param>
		public void ApplyMove(IGameMove move) {
			OthelloMove m = move as OthelloMove;

			// If the move is a pass, then we do very little.
			if (m.IsPass) {
				PassCount++;
			}
			else {
				PassCount = 0;
				// Otherwise update the board at the move's position with the current player.
				mBoard[m.Position.Row, m.Position.Col] = (sbyte)mCurrentPlayer;
				Value += mCurrentPlayer;

				// Iterate through all 8 directions radially from the move's position.
				for (int rDelta = -1; rDelta <= 1; rDelta++) {
					for (int cDelta = -1; cDelta <= 1; cDelta++) {
						if (rDelta == 0 && cDelta == 0)
							continue;

						// Repeatedly move in the selected direction, as long as we find "enemy" squares.
						BoardPosition newPos = m.Position;
						int steps = 0;
						do {
							newPos = newPos.Translate(rDelta, cDelta);
							steps++;
						} while (PositionInBounds(newPos) && PositionIsEnemy(newPos, mCurrentPlayer));

						// This is a valid direction of flips if we moved at least 2 squares, and ended in bounds and on a
						// "friendly" square.
						if (steps > 1 && PositionInBounds(newPos) && mBoard[newPos.Row, newPos.Col] == mCurrentPlayer) {
							// Record this direction in the move's flipsets so the move can be undone.
							m.AddFlipSet(
								new OthelloMove.FlipSet() {
									RowDelta = rDelta,
									ColDelta = cDelta,
									Count = steps - 1
								});
							// The FlipSet constructor takes no parameters; this syntax allows us to construct a FlipSet
							// and initialize many of its properties in one expression.

							// Repeatedly walk back the way we came, updating the board with the current player's piece.
							newPos = newPos.Translate(-rDelta, -cDelta);
							while (steps > 1) {
								mBoard[newPos.Row, newPos.Col] = (sbyte)mCurrentPlayer;
								Value += 2 * mCurrentPlayer;

								newPos = newPos.Translate(-rDelta, -cDelta);
								steps--;
							}
						}
					}
				}
			}
			// Update the rest of the board state.
			mCurrentPlayer = -mCurrentPlayer;
			MoveHistory.Add(m);
		}

		/// <summary>
		/// Returns true if the given position is in bounds of the board.
		/// </summary>
		private static bool PositionInBounds(BoardPosition pos) {
			return pos.Row >= 0 && pos.Row < BOARD_SIZE && pos.Col >= 0 && pos.Col < BOARD_SIZE;
		}

		/// <summary>
		/// Returns true if the given in-bounds position is an enemy of the given player.
		/// </summary>
		/// <param name="pos">assumed to be in bounds</param>
		private bool PositionIsEnemy(BoardPosition pos, int player) {
			return mBoard[pos.Row, pos.Col] == -player;
		}

		/// <summary>
		/// Returns an enumeration of moves that would be valid to apply to the current board state.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IGameMove> GetPossibleMoves() {
			List<OthelloMove> moves = new List<OthelloMove>();

			// Iterate through all 64 squares on the board, looking for an empty position.
			for (int row = 0; row < BOARD_SIZE; row++) {
				for (int col = 0; col < BOARD_SIZE; col++) {
					if (mBoard[row, col] != 0)
						continue;

					bool validSquare = false;

					// Iterate through all 8 directions radially from the current position.
					for (int rDelta = -1; rDelta <= 1 && !validSquare; rDelta++) {
						for (int cDelta = -1; cDelta <= 1 && !validSquare; cDelta++) {
							if (rDelta == 0 && cDelta == 0)
								continue;

							// Repeatedly move in the selected direction, as long as we find "enemy" squares.
							BoardPosition newPos = new BoardPosition(row, col);
							int steps = 0;
							do {
								newPos = newPos.Translate(rDelta, cDelta);
								steps++;
							} while (PositionInBounds(newPos) && PositionIsEnemy(newPos, mCurrentPlayer));

							// This is a valid direction of flips if we moved at least 2 squares, and ended in bounds and on a
							// "friendly" square.
							if (steps > 1 && PositionInBounds(newPos) && mBoard[newPos.Row, newPos.Col] == mCurrentPlayer) {
								validSquare = true;
							}
						}
					}
					// If the current position is valid, yield a move at the position.
					if (validSquare) {
						moves.Add(new OthelloMove(new BoardPosition(row, col)));
					}
				}
			}
			// If no positions were valid, yield a "pass" move.
			if (moves.Count == 0) {
				moves.Add(new OthelloMove(new BoardPosition(-1, -1)));
			}

			return moves;
		}


		/// <summary>
		/// Undoes the last move, restoring the game to its state before the move was applied.
		/// </summary>
		public void UndoLastMove() {
			OthelloMove m = MoveHistory[MoveHistory.Count - 1] as OthelloMove;

			// Note: there is a bug in this code.
			if (!m.IsPass) {
				// Reset the board at the move's position.
				mBoard[m.Position.Row, m.Position.Col] = 0;

				// Iterate through the move's recorded flipsets.
				foreach (var flipSet in m.FlipSets) {
					BoardPosition pos = m.Position;
					// For each flipset, walk along the flipset's direction resetting pieces.
					for (int i = 1; i <= flipSet.Count; i++) {
						pos = pos.Translate(flipSet.RowDelta, flipSet.ColDelta);
						mBoard[pos.Row, pos.Col] = (sbyte)mCurrentPlayer;
					}
				}

				// Check to see if the second-to-last move was a pass; if so, set PassCount.
				if (MoveHistory.Count > 1 && (MoveHistory[MoveHistory.Count - 2] as OthelloMove).IsPass) {
					PassCount = 1;
				}
			}
			else {
				PassCount--;
			}
			// Reset the remaining game state.
			mCurrentPlayer = -mCurrentPlayer;
			MoveHistory.RemoveAt(MoveHistory.Count - 1);
		}
	}
}
