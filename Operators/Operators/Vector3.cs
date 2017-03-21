using System;

namespace Operators {
	public struct Vector3 : IComparable<Vector3>, IEquatable<Vector3> {
		public double X { get; private set; }
		public double Y { get; private set; }
		public double Z { get; private set; }

		public Vector3(double x, double y, double z) {
			X = x;
			Y = y;
			Z = z;
		}

		public double Magnitude() => Math.Sqrt(X * X + Y * Y + Z * Z);

		public Vector3 Unit() {
			double m = Magnitude();
			return new Operators.Vector3(X / m, Y / m, Z / m);
		}
		
		public Vector3 Scale(double s) {
			return new Vector3(X * s, Y * s, Z * s);
		}

		public Vector3 Add(Vector3 other) {
			return new Vector3(X + other.X, Y + other.Y, Z + other.Z);
		}

		public Vector3 Cross(Vector3 other) {
			return new Vector3(Y * other.Z - Z * other.Y, other.X * Z - X * other.Z,
				X * other.Y - Y * other.X);
		}

		public int CompareTo(Vector3 other) {
			return Magnitude().CompareTo(other.Magnitude());
		}

		public override bool Equals(object obj) {
			return this.Equals((Vector3)obj);
		}

		public override int GetHashCode() {
			return (new { X, Y, Z }.GetHashCode());
		}

		public bool Equals(Vector3 other) {
			return X == other.X && Y == other.Y && Z == other.Z;
		}

		// C# classes can overload certain specific operators.
		public static bool operator ==(Vector3 lhs, Vector3 rhs) {
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Vector3 lhs, Vector3 rhs) {
			return !lhs.Equals(rhs);
		}

		public static Vector3 operator+(Vector3 lhs, Vector3 rhs) {
			return lhs.Add(rhs);
		}

		public static Vector3 operator*(Vector3 lhs, double rhs) {
			return lhs.Scale(rhs);
		}

		public static Vector3 operator *(Vector3 lhs, Vector3 rhs) {
			return lhs.Cross(rhs);
		}

		public static Vector3 operator/(Vector3 lhs, double rhs) {
			return lhs.Scale(1 / rhs);
		}

		public static bool operator<(Vector3 lhs, Vector3 rhs) {
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator >(Vector3 lhs, Vector3 rhs) {
			return lhs.CompareTo(rhs) > 0;
		}

		// General rules:
		// The operator is always static.
		// It always takes one parameter per operand.
		// At least one of the parameters must be of the containing class' type.
		// Certain operators must be paired: operator== and operator!=, operator< and operator>, etc.
		// All operators should be mirrored with public methods doing the same logic.
		// If you override operator==, you SHOULD override Object.Equals and Object.GetHashCode
	}

}
