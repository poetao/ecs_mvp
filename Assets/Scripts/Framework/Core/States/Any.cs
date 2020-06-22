using System;
using UnityEngine.Serialization;

namespace MVP.Framework.Core.States
{
    [Serializable]
    public struct Any : IEquatable<Any>
    {
        public object Object;
        public Type Type;
        public static Any Empty = new Any(new object());

        public Any(object obj)
        {
            this.Type = obj?.GetType();
            this.Object = obj;
        }

        public Any(object obj, Type type)
        {
            this.Type = type;
            this.Object = obj;
        }

        public Any(Type type)
        {
            this.Type = type;
            if (type.IsValueType)
                this.Object = Reflection.CreateInstance<object>(type);
            else
                this.Object = null;
        }

        public TValue Value<TValue>()
        {
            if (this.Object is TValue)
            {
                return (TValue) this.Object;
            }

            return default(TValue);
        }

        public bool Is<TValue>()
        {
            return typeof(TValue).IsAssignableFrom(this.Type);
        }

        public override string ToString()
        {
            return $"Any(Type:{Type}, Object:{Object})";
        }

        public bool Equals(Any other)
        {
            return this.Type.Equals(other.Type) && this.Object.Equals(other.Object);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Any && Equals((Any)obj);
        }

        public override int GetHashCode()
        {
            int h0;
            h0 = this.Type.GetHashCode();
            h0 = (h0 << 5) + h0 ^ this.Object.GetHashCode();
            return h0;
        }

        public static bool operator ==(Any left, Any right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Any left, Any right)
        {
            return !left.Equals(right);
        }
    }
}