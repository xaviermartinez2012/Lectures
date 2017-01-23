using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cecs475.War {
	/// <summary>
	/// Represents a deck of cards that can be shuffled and drawn from.
	/// </summary>
	public class Deck {
		// const values are automatically static, and cannot be changed.
		private const int NEW_DECK_SIZE = 52;

		// I will explain this later.
		private static Random mRng = new Random();

		// We have several choices of how to represent a deck of cards. We'll go with the simplest: an array of Card
		// objects, and a count of how many cards are still in the deck.
		private Card[] mCards;

		// We're going to need a public property to get the number of cards in the deck (but not set!). We could do this
		// the long way:
		// private int mCount;
		// public int Count {
		//		get { return mCount; }
		// }
		// But that's silly. When a property only directly returns/sets a member variable, we can use a C# feature called
		// auto properties:
		/// <summary>
		/// A count of the number of cards still in the deck.
		/// </summary>
		public int Count {
			get;
			private set;
		}
		// When neither the get nor set have a body, C# will automatically create a hidden member variable to represent
		// the property, then will create get and set methods to manipulate that value. If we add "private" to the set,
		// then only this class can set the Count value.

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
			// We use a static Random generator because the Random constructor uses a time-based seed, and if two decks are
			// shuffled within some small time frame, they will end up the same if they both construct a new Random 
			// instance. Using a shared static instance means this won't happen.

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
			// String.Join: creates a string by inserting the given delimiter between every element of a given collection.
			// Reverse(): reverses a sequence.
			// Take(n): returns only the first n elements of a sequence.
			return string.Join(", ", mCards.Take(Count).Reverse());
		}
	}
}
