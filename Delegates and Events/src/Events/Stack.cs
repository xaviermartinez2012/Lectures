using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events {
	/// <summary>
	/// Implements a simple stack using an array list as the backing store. Provides events to listen
	/// for when items are added and removed.
	/// </summary>
	public class IntStack {
		private List<int> mData = new List<int>();

		// An "event" is a variable that allows other code (a "client") to "listen" for notable actions that take
		// place in our code. We define what "notable means", and we decide when we "trigger" the event.
		// An event is declared as:
		// *accessmodifier* event *delegatetype* *name*;

		// Here, we want an event that triggers whenever an item is added to the stack. If you wanted to be informed
		// when that happened, what would you want to know? You'd want to know (at least) what stack was added to,
		// and perhaps too what item was added.
		public event Action<IntStack, int> ItemAdded;
		// Jump back to Program.cs to see this in action...

		public event Action<IntStack, int> ItemRemoved;

		public void Push(int element) {
			mData.Add(element);

			// To notify the clients that are listening to ItemAdded, I invoke it like a function.
			if (ItemAdded != null) {
				ItemAdded(this, element);
			}
		}

		public int Pop() {
			int lastIndex = mData.Count - 1;
			int element = mData[lastIndex];
			mData.RemoveAt(lastIndex);

			if (ItemRemoved != null) {
				ItemRemoved(this, element);
			}
			return element;
		}

		public int Peek() {
			return mData[mData.Count - 1];
		}

		public int Count { get { return mData.Count; } }
	}
}
