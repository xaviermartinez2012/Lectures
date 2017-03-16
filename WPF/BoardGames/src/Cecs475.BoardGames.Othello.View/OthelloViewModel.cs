using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Cecs475.BoardGames;
using Cecs475.BoardGames.View;
using System;

namespace Cecs475.BoardGames.Othello.View {
	/// <summary>
	/// Represents one square on the Othello board grid.
	/// </summary>
	public class OthelloSquare : INotifyPropertyChanged {
		private int mPlayer;
		/// <summary>
		/// The player that has a piece in the given square, or 0 if empty.
		/// </summary>
		public int Player {
			get { return mPlayer; }
			set {
				if (value != mPlayer) {
					mPlayer = value;
					OnPropertyChanged(nameof(Player));
				}
			}
		}

		/// <summary>
		/// The position of the square.
		/// </summary>
		public BoardPosition Position {
			get; set;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string name) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

	/// <summary>
	/// Represents the game state of a single othello game.
	/// </summary>
	public class OthelloViewModel : INotifyPropertyChanged, IGameViewModel {
		private OthelloBoard mBoard;
		private ObservableCollection<OthelloSquare> mSquares;
		public event EventHandler GameFinished;

		public OthelloViewModel() {
			mBoard = new OthelloBoard();
			
			// Initialize the squares objects based on the board's initial state.
			mSquares = new ObservableCollection<OthelloSquare>(
				from pos in (
					from r in Enumerable.Range(0, 8)
					from c in Enumerable.Range(0, 8)
					select new BoardPosition(r, c)
				)
				select new OthelloSquare() {
					Position = pos,
					Player = mBoard.GetPieceAtPosition(pos)
				}
			);

			PossibleMoves = new HashSet<BoardPosition>(
				from OthelloMove m in mBoard.GetPossibleMoves()
				select m.Position
			);
		}

		/// <summary>
		/// Applies a move for the current player at the given position.
		/// </summary>
		public void ApplyMove(BoardPosition position) {
			var possMoves = mBoard.GetPossibleMoves() as IEnumerable<OthelloMove>;
			// Validate the move as possible.
			foreach (var move in possMoves) {
				if (move.Position.Equals(position)) {
					mBoard.ApplyMove(move);
					break;
				}
			}

			// Rebind the possible moves, now that the board has changed.
			PossibleMoves = new HashSet<BoardPosition>(
				from OthelloMove m in mBoard.GetPossibleMoves()
				select m.Position
			);

			// Update the collection of squares by examining the new board state.
			var newSquares =
				from r in Enumerable.Range(0, 8)
				from c in Enumerable.Range(0, 8)
				select new BoardPosition(r, c);
			int i = 0;
			foreach (var pos in newSquares) {
				mSquares[i].Player = mBoard.GetPieceAtPosition(pos);
				i++;
			}
			OnPropertyChanged(nameof(BoardValue));
			OnPropertyChanged(nameof(CurrentPlayer));
		}

		/// <summary>
		/// A collection of 64 OthelloSquare objects representing the state of the 
		/// game board.
		/// </summary>
		public ObservableCollection<OthelloSquare> Squares {
			get { return mSquares; }
		}

		/// <summary>
		/// A set of board positions where the current player can move.
		/// </summary>
		public HashSet<BoardPosition> PossibleMoves {
			get; private set;
		}

		/// <summary>
		/// The player whose turn it currently is.
		/// </summary>
		public int CurrentPlayer {
			get { return mBoard.CurrentPlayer; }
		}

		/// <summary>
		/// The value of the othello board.
		/// </summary>
		public int BoardValue { get { return mBoard.Value; } }

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string name) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}
}
