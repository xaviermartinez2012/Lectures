using System;
using System.Collections.Generic;

// A namespace is a logical grouping of types under one name. A namespace is added to a type's name to create its
// Fully Qualified Name. Namespaces are similar to Java packages, except Java packages define a physical on-disk
// grouping in addition to a logical grouping.
namespace Cecs475.War {
	// A class defines a new reference type. Like Java, all classes inherit from a base Object type. In .NET, a
	// reference type uses reference semantics for assignment and copying. All classes in Java are reference types,
	// but .NET (and thus C#) give us the option of declaring non-reference types, which we will see later.

	// A comment like the one below is a documentation comment.
	/// <summary>
	/// Represents a single card in a 52-card deck of playing cards.
	/// </summary>
	public class Card : IComparable {
		// An enum is a new type whose values can only be taken from the names in the enum declaration. Each value
		// in the enum is secretly an integer counting up from 0.
		// Because this type is declared inside Card, other types will have to use the name "Card.Suit"
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

		// A Card consists of a suit and a kind.
		private CardSuit mSuit;
		private CardKind mKind;

		// Constructor
		public Card(CardKind kind, CardSuit suit) {
			mSuit = suit;
			mKind = kind;
		}

		// Since mSuit is private, we need a way for other classes to access that value. In Java we would do this with a
		// "get" accessor method. You could do that in C# too, but the preference in .NET languages is to use a property.
		public CardSuit Suit {
			// The name of this property is Suit, and its type is CardSuit. Note that this is not a method because there 
			// are no parentheses.
			// Inside a property block we can have two other blocks: a get block and a set block.
			get {
				// A get block is like an accessor, and is called when someone wants to retrieve a value from the property.
				// In this case, we just return our private variable.
				return mSuit;
			}
			// If we want others to be able to mutate the property, we can provide a set block as well. It doesn't really
			// make sense for Suit to need to be mutated after construction, but we can still demo the idea.
			set {
				// Someone wants to change the card's suit to a new value. Inside of a set block, we have a special variable
				// named "value", representing the new value to set the property to.
				// value can be thought of as the parameter to a Java mutator method.
				mSuit = value;
			}
		}

		// Without the verbose comments, we get something like this:
		public CardKind Kind {
			get { return mKind; }
			set { mKind = value; }
		}
		// which is a nicer way of defining an accessor/mutator pair than Java's slightly longer getX/setX standard.


		// As in Java, the Object class defines a method ToString for creating a string representation of an object.
		// The override keyword is mandatory and indicates we are changing the behavior of a method defined in a base
		// class.
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

		// Compare this card to another, to decide which wins the War game. This is inherited from the IComparable 
		// interface.
		public int CompareTo(object obj) {
			Card c = obj as Card;
			// compare the cards based on the integer value of their Kind.
			return this.Kind.CompareTo(c.Kind);
		}
	}
}
