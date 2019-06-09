namespace Endahl.CSharpedSql
{
    using System;

    public class ColumnValue
    {
        protected ColumnValue(string columnName, object value)
        {
            ColumnName = columnName;
            Value = value;
        }

        public ColumnValue(string columnName, bool value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, byte value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, byte[] value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, char value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, DateTime value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, DateTimeOffset value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, decimal value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, double value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, float value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, Guid value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, int value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, long value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, short value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, sbyte value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, string value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, uint value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, ulong value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, ushort value) : this(columnName, value as object) { }
        public ColumnValue(string columnName, InsertFunction value) : this(columnName, value as object) { }

        public string ColumnName { get; }
        public object Value { get; }
    }
}
