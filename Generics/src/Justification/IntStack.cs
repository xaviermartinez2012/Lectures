using System;

namespace Cecs475.Generics {
	/// <summary>
	/// Implements a FILO stack of integers without composing other collection types.
	/// </summary>
	public class IntStack {
		private int[] mData = new int[4]; // default size

		public int Count { get; private set; }

		public void Push(int element) {
			ResizeIfNecessary();
			mData[Count++] = element;
		}

		public int Pop() {
			if (Count == 0) {
				throw new InvalidOperationException("Cannot pop from an empty stack");
			}
			return mData[--Count];
		}

		public int Peek() {
			if (Count == 0) {
				throw new InvalidOperationException("Cannot peek an empty stack");
			}
			return mData[Count - 1];
		}

		private void ResizeIfNecessary() {
			if (mData.Length == Count) {
				int[] newData = new int[mData.Length * 2];
				Array.Copy(mData, newData, mData.Length);
				mData = newData;
			}
		}
	}

	/*
	 * A simple class, a simple goal, a simple exercise, and a simple question:
	 * What happens if I want a DoubleStack class?
	 * What about a StringStack?
	 * PokemonStack?
	 */
}
