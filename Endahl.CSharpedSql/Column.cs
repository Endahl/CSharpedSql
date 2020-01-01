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
        public int Size { get; }
        public int Digits { get; }
        public object DefaultValue { get; }

        public Column(string name, CSharpType type, int size = 0, int digits = 0, bool notNull = false,
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
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        /// <summary>
        /// Returns the <see cref="Column"/> as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            var line = $"{sql.IdentifieName(Name)} ";

            line += sql.CSharpTypeToSqlDataType(DataType, Size, Digits);

            line += NotNull ? " NOT NULL" : "";
            if (AutoIncrement)
            {
                line += sql.SqlLanguage == SqlLanguage.SqlServer ? " IDENTITY(1,1)" : " AUTO_INCREMENT";
            }
            else if (DefaultValue != null)
            {
                line += $" DEFAULT {sql.CreateItemID(DefaultValue)}";
            }

            return line;
        }
    }
}
