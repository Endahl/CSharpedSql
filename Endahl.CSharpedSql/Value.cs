namespace Endahl.CSharpedSql
{
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

        public object Object { get; }

        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        public override string ToString(SqlOptions sql)
        {
            return sql.CreateItemID(Object);
        }
    }
}
