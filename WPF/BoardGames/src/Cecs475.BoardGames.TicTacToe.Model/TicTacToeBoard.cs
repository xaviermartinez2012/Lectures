using Cecs475.BoardGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.BoardGames.TicTacToe.Model {
	/// <summary>
	/// Represents the board state for a game of tic tac toe.
	/// </summary>
	public class TicTacToeBoard : IGameBoard {
		private bool mGameOver;
		private int mPlayer;
		private sbyte[,] mBoard = new sbyte[3, 3];

		public TicTacToeBoard() {
			mPlayer = 1;
			MoveHistory = new List<IGameMove>();
		}

		public int CurrentPlayer {
			get {
				return mPlayer == -1 ? 2 : 1;
			}
		}

		public IList<IGameMove> MoveHistory { get; private set; }

		/// <summary>
		/// Value is either 0, or 1 if Player 1 has won, or -1 if Player 2 has won.
		/// </summary>
		public int Value {
			get; private set;
		}

		public void ApplyMove(IGameMove move) {
			TicTacToeMove m = move as TicTacToeMove;
			SetPosition(m.Position, CurrentPlayer);
			MoveHistory.Add(m);
			mPlayer = -mPlayer;
			mGameOver = GameIsOver();
		}

		/// <summary>
		/// True if one player has 3 squares in a row. Sets Value 1 if Player1 has won, 
		/// or -1 if Player2 has won.
		/// </summary>
		/// <returns></returns>
		private bool GameIsOver() {
			Value = 0;
			for (int r = 0; r < 3; r++) {
				if (mBoard[r, 0] == mBoard[r, 1] && mBoard[r, 0] == mBoard[r, 2] && mBoard[r, 0] != 0) {
					Value = mBoard[r, 0];
				}
				if (mBoard[0, r] == mBoard[1, r] && mBoard[0, r] == mBoard[2, r] && mBoard[0, r] != 0) {
					Value = mBoard[r, 0];
				}
			}
			if (mBoard[0, 0] == mBoard[1, 1] && mBoard[0, 0] == mBoard[2, 2] && mBoard[0, 0] != 0) {
				Value = mBoard[0, 0];
			}
			if (mBoard[0, 2] == mBoard[1, 1] && mBoard[0, 2] == mBoard[2, 0] && mBoard[0, 2] != 0) {
				Value = mBoard[0, 2];
			}
			return Value != 0;
		}

		private void SetPosition(BoardPosition position, int player) {
			mBoard[position.Row, position.Col] = (sbyte)(player == 2 ? -1 : player);
		}

		public IEnumerable<IGameMove> GetPossibleMoves() {
			if (mGameOver) {
				return new IGameMove[0];
			}
			return
				from pos in (
					from r in Enumerable.Range(0, 3)
					from c in Enumerable.Range(0, 3)
					select new BoardPosition(r, c)
				)
				where GetPieceAtPosition(pos) == 0
				select new TicTacToeMove(pos);
		}

		public int GetPieceAtPosition(BoardPosition pos) {
			int player = mBoard[pos.Row, pos.Col];
			return player == -1 ? 2 : player;
		}

		public void UndoLastMove() {
			TicTacToeMove m = MoveHistory.Last() as TicTacToeMove;
			SetPosition(m.Position, 0);
			MoveHistory.RemoveAt(MoveHistory.Count - 1);
		}
	}
}
