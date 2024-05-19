namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    public class Column
    {
        public CSharpType DataType { get; }
        public string Name { get; }
        public Constraint Constraint { get; }
        public bool NotNull { get; }
        public bool AutoIncrement { get; }
        public uint Size { get; }
        public int Digits { get; }
        public object DefaultValue { get; }

        public Column(string name, CSharpType type, uint size = 0, int digits = 0, bool notNull = false,
            bool autoIncrement = false, object defaultValue = null, Constraint constraint = null)
        {
            DataType = type;
            Name = name;
            Constraint = constraint;
            NotNull = notNull;
            Size = size;
            Digits = digits;
            DefaultValue = defaultValue;
            AutoIncrement = autoIncrement;
        }

        /// <summary>
        /// Returns the <see cref="Column"/> as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="Column"/> as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => sql.SqlBase.Column(this, sql);
    }
}
