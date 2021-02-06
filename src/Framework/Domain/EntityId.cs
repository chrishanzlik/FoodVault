using System;
using System.Collections.Generic;

namespace FoodVault.Framework.Domain
{
    /// <summary>
    /// Represents an identifier for an entity.
    /// </summary>
    public class EntityId :
            IEquatable<EntityId>,
            IComparable<EntityId>,
            IComparable<Guid>,
            IComparable
    {
        /// <summary>
        /// Gets the internal ID value.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityId" /> class.
        /// </summary>
        /// <param name="value">Idnetifier</param>
        protected EntityId(Guid value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is EntityId other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc />
        public bool Equals(EntityId other)
        {
            if (other == null)
            {
                return Value == Guid.Empty;
            }

            return this.Value == other?.Value;
        }

        /// <inheritdoc />
        public int CompareTo(EntityId other)
        {
            if (other == null) return 1;

            return Comparer<Guid>.Default.Compare(this.Value, other.Value);
        }

        /// <inheritdoc />
        public int CompareTo(Guid other)
        {
            return Comparer<Guid>.Default.Compare(this.Value, other);
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is EntityId id)
            {
                return CompareTo(id);
            }

            if (obj is Guid g)
            {
                return CompareTo(g);
            }

            throw new ArgumentException($"Parameter is not of type {nameof(EntityId)}");
        }

        /// <inheritdoc />
        public static bool operator ==(EntityId obj1, EntityId obj2)
        {
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }

        /// <inheritdoc />
        public static bool operator !=(EntityId x, EntityId y)
        {
            return !(x == y);
        }

        /// <inheritdoc />
        public static implicit operator Guid(EntityId typedId)
        {
            return typedId.Value;
        }
    }
}