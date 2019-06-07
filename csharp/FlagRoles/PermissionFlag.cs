using System;
using System.Diagnostics;
using System.Numerics;

namespace FlagRoles
{
    [DebuggerDisplay("{Value}")]
    public class PermissionFlag : IEquatable<PermissionFlag>
    {
        public PermissionFlag() : this(0)
        {

        }

        public PermissionFlag(BigInteger value)
        {
            Value = value;
        }

        public PermissionFlag(byte[] value)
        {
            Value = new BigInteger(value);
        }

        public BigInteger Value { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as PermissionFlag);
        }

        public bool Equals(PermissionFlag other)
        {
            return other != null && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static PermissionFlag operator &(PermissionFlag left, PermissionFlag right)
        {
            return left.Value & right.Value;
        }

        public static PermissionFlag operator |(PermissionFlag left, PermissionFlag right)
        {
            return left.Value | right.Value;
        }

        public static bool operator ==(PermissionFlag left, PermissionFlag right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(PermissionFlag left, PermissionFlag right)
        {
            return left.Value != right.Value;
        }

        public static implicit operator BigInteger(PermissionFlag flag)
        {
            return flag.Value;
        }

        public static implicit operator PermissionFlag(BigInteger value)
        {
            return new PermissionFlag(value);
        }

        public static implicit operator PermissionFlag(byte[] value)
        {
            return new PermissionFlag(value);
        }

        public static implicit operator PermissionFlag(int value)
        {
            return new PermissionFlag(value);
        }

        public static implicit operator PermissionFlag(long value)
        {
            return new PermissionFlag(value);
        }

        public static implicit operator PermissionFlag(uint value)
        {
            return new PermissionFlag(value);
        }

        public static implicit operator PermissionFlag(ulong value)
        {
            return new PermissionFlag(value);
        }

        /// <summary>
        /// Create a flag representing a permission's ID. This is the same as (2 ^ permissionId)
        /// </summary>
        /// <param name="permissionId">The unique identifier for this permission</param>
        public static PermissionFlag FromId(int permissionId)
        {
            if (permissionId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(permissionId));
            }

            var permissionFlagValue = new BigInteger(1) << permissionId;
            return new PermissionFlag(permissionFlagValue);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
