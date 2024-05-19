namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    public class NewColumn
    {
        private readonly Column sqlTypeItem;

        public string ColumnName => sqlTypeItem.Name;
        public CSharpType DataType => sqlTypeItem.DataType;
        public uint Size => sqlTypeItem.Size;
        public int Digits => sqlTypeItem.Digits;
        public bool NotNull => sqlTypeItem.NotNull;
        public bool AutoIncrement => sqlTypeItem.AutoIncrement;
        public object DefaultValue => sqlTypeItem.DefaultValue;

        public NewColumn(string column, CSharpType dataType, uint size = 0, int digits = 0, bool notNull = false,
            bool autoIncrement = false, object defaultValue = null)
        {
            sqlTypeItem = new Column(column, dataType, size, digits, notNull, autoIncrement, defaultValue);
        }

        /// <summary>
        /// Returns the <see cref="NewColumn"/> as a string.
        /// </summary>
        public override string ToString() => sqlTypeItem.ToString();
        /// <summary>
        /// Returns the <see cref="NewColumn"/> as a string.
        /// </summary>
        public string ToString(SqlOptions sql) => sqlTypeItem.ToString(sql);
    }
}
