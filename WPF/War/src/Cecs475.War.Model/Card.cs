using System;

// This is the same class as our intoductory example.
namespace Cecs475.War.Model {
	public class Card : IComparable<Card> {
		public enum CardSuit {
			Spades, // 0
			Clubs,  // 1, etc.
			Diamonds,
			Hearts
		}

		public enum CardKind {
			Two = 2, // a value can be supplied explicitly, and other values will count up from there.
			Three,
			Four,
			Five,
			Six,
			Seven,
			Eight,
			Nine,
			Ten,
			Jack,
			Queen,
			King,
			Ace // == 14
		}

		private CardSuit mSuit;
		private CardKind mKind;

		public Card(CardKind kind, CardSuit suit) {
			mSuit = suit;
			mKind = kind;
		}

		public CardSuit Suit {
			get {return mSuit;}
			set {mSuit = value;}
		}

		public CardKind Kind {
			get { return mKind; }
			set { mKind = value; }
		}

		public override string ToString() {
			int kindValue = (int)mKind;
			string r = null;
			if (kindValue >= 2 && kindValue <= 10) {
				r = kindValue.ToString();
			}
			else {
				r = mKind.ToString(); // ToString on an enum returns the name given in code, e.g., "Jack", "Two", etc.
			}
			return r + " of " + mSuit.ToString();
		}

		public int CompareTo(Card other) {
			return Kind.CompareTo(other.Kind);
		}
	}
}
