namespace ImportantTypes {
	// A recreation of important types that C# programmers should know.

	interface IComparable {
		int CompareTo(object other);
	}
	interface IComparable<T> {
		int CompareTo(T other);
	}
	// What's the difference?

	interface IEquatable<T> {
		bool Equals(T other);
	}
	// How is that different than Object.Equals?

	// IEnumerable is a read-only sequence of values.
	interface IEnumerable<T> {
		IEnumerator<T> GetEnumerator();
	}

	// IEnumerator gives access to individual elements of an IEnumerable.
	interface IEnumerator<T> {
		T Current { get; }
		bool MoveNext();
	}

	// ICollection is a container that can add and remove elements. Collection<T> in Java.
	interface ICollection<T> : IEnumerable<T> { 
		void Add(T item);
		void Remove(T item);
		bool Contains(T item);
		int Count { get; }
	}

	// IList is a list container (items indexed by integer position). List<T> in Java.
	interface IList<T> : ICollection<T> { // thus, also IEnumerable<T>
		T this[int index] { get; set; } // syntax for overloading operator []
		int IndexOf(T item);
		void Insert(int index, T item);
		void RemoveAt(int index);
	}

	// List<T> implements IList<T> using an array list. ArrayList<T> in Java.
	

	// A key/value pair for use in maps. (Map.Entry in Java.)
	struct KeyValuePair<TKey, TValue> {
		public TKey Key { get; }
		public TValue Value { get; }
	}

	// IDictionary is a map (dictionary) structure from keys to values. Map<TKey, TValue> in Java.
	interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>> {
		void Add(TKey key, TValue value);
		bool ContainsKey(TKey key);
		void Remove(TKey key);
	}

	// Dictionary<TKey, TValue> implements IDictionary<TKey, TValue> using a hashtable with open addressing.
	// HashMap<TKey, TValue> in Java.
}
