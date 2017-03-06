using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cecs475.War.Model;
namespace Cecs475.War.Application {
	/// <summary>
	/// A ViewModel represents the state of the application. It creates Model objects and mediates their
	/// behaviors so that a View can invoke commands on the application state without talking directly
	/// to the Model state.
	/// 
	/// This ViewModel tracks the state of "a game of War": it contains a deck of cards, and tracks
	/// the most recent card dealt to each player, which round number it is, and the scores for each player.
	/// </summary>
	public class WarViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string name) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private Deck mDeck1;
		private Deck mDeck2;
		private int mPlayer1Wins;
		private int mPlayer2Wins;
		private int mDraws;
		private int mRound;
		private Card mPlayer1Card;
		private Card mPlayer2Card;

		/// <summary>
		/// Constructs the ViewModel, with two shuffled decks. 
		/// </summary>
		public WarViewModel() {
			mDeck1 = new Deck();
			mDeck2 = new Deck();
			mDeck1.Shuffle();
			mDeck2.Shuffle();
		}

		/// <summary>
		/// The Card "showing" for player 1
		/// </summary>
		public Card Player1Card {
			get {
				return mPlayer1Card;
			}
			private set {
				mPlayer1Card = value;
				OnPropertyChanged(nameof(Player1Card));
			}
		}

		/// <summary>
		/// The Card "showing" for player 2
		/// </summary>
		public Card Player2Card {
			get {
				return mPlayer2Card;
			}
			private set {
				mPlayer2Card = value;
				OnPropertyChanged(nameof(Player2Card));
			}
		}

		/// <summary>
		/// The number of times player 1's card won the round.
		/// </summary>
		public int Player1Wins {
			get {
				return mPlayer1Wins;
			}
			private set {
				mPlayer1Wins = value;
				OnPropertyChanged(nameof(Player1Wins));
			}
		}

		/// <summary>
		/// The number of times player 2's card won the round.
		/// </summary>
		public int Player2Wins {
			get {
				return mPlayer2Wins;
			}
			private set {
				mPlayer2Wins = value;
				OnPropertyChanged(nameof(Player2Wins));
			}
		}

		/// <summary>
		/// The number of times the players tied.
		/// </summary>
		public int Draws{
			get {
				return mDraws;
			}
			private set {
				mDraws = value;
				OnPropertyChanged(nameof(Draws));
			}
		}

		/// <summary>
		/// How many cards have already been dealt.
		/// </summary>
		public int Round {
			get {
				return mRound;
			}
			private set {
				mRound = value;
				OnPropertyChanged(nameof(Round));
				OnPropertyChanged(nameof(CanDeal));
			}
		}

		/// <summary>
		/// True if the players have cards remaining in their decks.
		/// </summary>
		public bool CanDeal {
			get { return Round < 52; }
		}

		/// <summary>
		/// Deals one card to each player from their respective decks.
		/// </summary>
		public void DealOneCard() {
			Player1Card = mDeck1.DealOne();
			Player2Card = mDeck2.DealOne();
			int comp = Player1Card.CompareTo(Player2Card);
			if (comp == 0) {
				Draws++;
			}
			else if (comp < 0) {
				Player2Wins++;
			}
			else {
				Player1Wins++;
			}
			Round++;
		}
	}
}
