using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cecs475.War.Model {
	/// <summary>
	/// Represents a deck of cards that can be shuffled and drawn from.
	/// </summary>
	public class Deck {
		private const int NEW_DECK_SIZE = 52;
		private static Random mRng = new Random();
		private Card[] mCards;

		public int Count {
			get;
			private set;
		}


		/// <summary>
		/// Construct a new unshuffled deck.
		/// </summary>
		public Deck() {
			mCards = new Card[NEW_DECK_SIZE];
			Count = NEW_DECK_SIZE; // this sets the hidden "Count" member to 52, using the private set property.

			int i = 0;
			// For simplicity, we will abuse the fact that we know that CardSuit and CardKind are really just integers.
			for (int suit = 0; suit < 4; suit++) {
				for (int kind = 2; kind <= 14; kind++) {
					mCards[i] = new Card((Card.CardKind)kind, (Card.CardSuit)suit); // the cast satisfies the type system.
					i++;
				}
			}
		}

		/// <summary>
		/// Performs a randomized shuffle of whichever cards are still in the deck.
		/// </summary>
		public void Shuffle() {
			// Perform a Fisher-Yates shuffle.
			for (int i = Count - 1; i > 0; i--) {
				int j = mRng.Next(i + 1);
				Card temp = mCards[j];
				mCards[j] = mCards[i];
				mCards[i] = temp;
			}
		}
		
		/// <summary>
		/// Deals one card, removing it from the top of the deck.
		/// </summary>
		/// <returns>the top card of the deck</returns>
		public Card DealOne() {
			return mCards[--Count];
		}

		// Return a string of all the cards in the deck, from top to bottom.
		public override string ToString() {
			return string.Join(", ", mCards.Take(Count).Reverse());
		}
	}
}
