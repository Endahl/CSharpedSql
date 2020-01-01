namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// A column in a <see cref="Select"/> statement.
    /// Can be a string, a <see cref="Case"/>, a <see cref="Function"/> or a <see cref="Value"/>
    /// </summary>
    public class ColumnItem
    {
        /// <summary>
        /// Gets or sets the name for this <see cref="ColumnItem"/>.
        /// </summary>
        public virtual string Name { get; set; }

        private ColumnItem(string name)
        {
            Name = name;
        }
        protected ColumnItem() { }

        /// <summary>
        /// Returns the <see cref="ColumnItem"/> as a string.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Returns the <see cref="ColumnItem"/> as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            if (Name == "*")
                return Name;
            return sql.IdentifieName(Name);
        }

        // allow the class to be created like a String. Example ( ColumnItem item = "test"; )
        public static implicit operator ColumnItem(string name)
        {
            return new ColumnItem(name);
        }
    }
}
