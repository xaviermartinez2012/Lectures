using System.Collections.Generic;

namespace Cecs475.BoardGames {
	/// <summary>
	/// Represents the board model for a particular board game. Can apply moves, get all possible moves, undo moves,
	/// and report other state information. 
	/// </summary>
	public interface IGameBoard {
		/// <summary>
		/// Gets a list of all possible moves for the current game state.
		/// </summary>
		IEnumerable<IGameMove> GetPossibleMoves();

		/// <summary>
		/// Applies a valid move to the current game state.
		/// </summary>
		/// <param name="move">assumed to be a valid move from the possible moves list</param>
		void ApplyMove(IGameMove move);

		/// <summary>
		/// Undoes the most recent move, restoring the game state to the moment when the move was applied.
		/// </summary>
		void UndoLastMove();

		/// <summary>
		/// The player whose turn it currently is, counting from 1 for the first player.
		/// </summary>
		int CurrentPlayer { get; }

		/// <summary>
		/// A list of all moves applied to the game, in the order they were applied.
		/// </summary>
		IList<IGameMove> MoveHistory { get; }

		/// <summary>
		/// A value indicating which player is winning the game, in a game-specific way. Positive is player 1.
		/// </summary>
		int Value { get; }
	}
}
