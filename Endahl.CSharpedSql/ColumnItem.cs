namespace Endahl.CSharpedSql
{
    /// <summary>
    /// This class can be created like a String
    /// </summary>
    public class ColumnItem
    {
        public virtual string Name { get; set; }

        private ColumnItem(string name)
        {
            Name = name;
        }
        protected ColumnItem() { }

        public override string ToString()
        {
            return Name;
        }
        public virtual string ToString(SqlOptions sql)
        {
            if (Name == "*")
                return Name;
            return sql.IdentifieName(Name);
        }

        // allow the class to be created like a String. Example (ColumnItem item = "test";)
        public static implicit operator ColumnItem(string name)
        {
            return new ColumnItem(name);
        }
    }
}
