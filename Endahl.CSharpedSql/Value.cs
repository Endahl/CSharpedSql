namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System;

    public class Value : ColumnItem
    {
        protected Value(object value, string asName)
        {
            Name = asName;
            Object = value;
        }

        public Value(bool value, string asName) : this(value as object, asName) { }
        public Value(byte value, string asName) : this(value as object, asName) { }
        public Value(byte[] value, string asName) : this(value as object, asName) { }
        public Value(char value, string asName) : this(value as object, asName) { }
        public Value(DateTime value, string asName) : this(value as object, asName) { }
        public Value(DateTimeOffset value, string asName) : this(value as object, asName) { }
        public Value(decimal value, string asName) : this(value as object, asName) { }
        public Value(double value, string asName) : this(value as object, asName) { }
        public Value(float value, string asName) : this(value as object, asName) { }
        public Value(Guid value, string asName) : this(value as object, asName) { }
        public Value(int value, string asName) : this(value as object, asName) { }
        public Value(long value, string asName) : this(value as object, asName) { }
        public Value(short value, string asName) : this(value as object, asName) { }
        public Value(sbyte value, string asName) : this(value as object, asName) { }
        public Value(string value, string asName) : this(value as object, asName) { }
        public Value(uint value, string asName) : this(value as object, asName) { }
        public Value(ulong value, string asName) : this(value as object, asName) { }
        public Value(ushort value, string asName) : this(value as object, asName) { }

        /// <summary>
        /// Gets the value from this <see cref="Value"/>
        /// </summary>
        public object Object { get; }

        /// <summary>
        /// Returns the <see cref="Value"/> as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="Value"/> as a string.
        /// </summary>
        public override string ToString(SqlOptions sql) => sql.CreateItemID(Object);
    }
}
