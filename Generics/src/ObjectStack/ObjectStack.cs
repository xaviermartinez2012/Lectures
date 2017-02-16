using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cecs475.Generics {
	/// <summary>
	/// Represents a stack of any type of object.
	/// </summary>
	public class ObjectStack {
		private object[] mData = new object[4];

		public int Count { get; private set; }

		public void Push(object element) {
			ResizeIfNecessary();
			mData[Count++] = element;
		}

		public object Pop() {
			if (Count == 0) {
				throw new InvalidOperationException("Cannot pop from an empty stack");
			}
			return mData[--Count];
		}

		public object  Peek() {
			if (Count == 0) {
				throw new InvalidOperationException("Cannot peek an empty stack");
			}
			return mData[Count - 1];
		}

		private void ResizeIfNecessary() {
			if (mData.Length == Count) {
				object [] newData = new object [mData.Length * 2];
				Array.Copy(mData, newData, mData.Length);
				mData = newData;
			}
		}

		public static void Main(string[] args) {
			// This stack can hold any type of data.
			ObjectStack s1 = new ObjectStack();
			s1.Push("Strings");
			s1.Push("Can");
			s1.Push("Be");
			s1.Push("Pushed");
			// If I know the stack contains strings, I can downcast the return of Peek or Pop.
			string top = s1.Peek() as string;
			Console.WriteLine($"{top} is on top");

			// Since even value types like int derive from object, I can also make stacks of value types.
			ObjectStack s2 = new ObjectStack();
			s2.Push(5);
			s2.Push(6);
			s2.Push(7);
			// Remind me how this works...


			// Otherwise, looks great! What's the catch?
		}
	}
}
