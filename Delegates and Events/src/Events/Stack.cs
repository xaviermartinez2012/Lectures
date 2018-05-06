using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events {
	/// <summary>
	/// Implements a simple stack using an array list as the backing store. Provides events to listen
	/// for when items are added and removed.
	/// </summary>
	public class Stack<T> {
		private List<T> mData = new List<T>();

		// An "event" is a variable that allows other code (a "client") to "listen" for notable actions that take
		// place in our code. We define what "notable" means, and we decide when we "trigger" the event.
		// An event is declared as:
		// *accessmodifier* event *delegatetype* *name*;

		// Here, we want an event that triggers whenever an item is added to the stack. If you wanted to be informed
		// when that happened, what would you want to know? You'd want to know (at least) what stack was added to,
		// and perhaps too what item was added.

		// The standard pattern for events is to use the EventHandler delegate type. This delegate
		// defines a function that accepts two parameters: an object "sender" indicating the CLI object that
		// triggered the event, and any "parameters" to the event wrapped inside of an EventArgs-derived object.


		public event EventHandler<StackEventArgs<T>> ItemAdded;
		public event EventHandler<StackEventArgs<T>> ItemRemoved;
		// Jump back to Program.cs to see this in action...

		public void Push(T element) {
			mData.Add(element);

			// To notify the clients that are listening to ItemAdded, I invoke it like a function.
			if (ItemAdded != null) {
				ItemAdded(this, new StackEventArgs<T> { Value = element });
			}
		}

		public T Pop() {
			int lastIndex = mData.Count - 1;
			T element = mData[lastIndex];
			mData.RemoveAt(lastIndex);

			if (ItemRemoved != null) {
				ItemRemoved(this, new StackEventArgs<T> { Value = element });
			}
			return element;
		}

		public T Peek() {
			return mData[mData.Count - 1];
		}

		public int Count { get { return mData.Count; } }
	}


	public class StackEventArgs<T> : EventArgs {
		public T Value { get; set; }
	}
}
