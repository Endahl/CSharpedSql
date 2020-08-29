namespace Endahl.CSharpedSql.Base
{
    using System.Collections.Generic;

    /// <summary>
    /// Options and functions used when generating sql commands.
    /// </summary>
    public class SqlOptions
    {
        /// <summary>
        /// Contains all values from all queries that are added to the <see cref="Sql"/> instance that this <see cref="SqlOptions"/> is part of.
        /// <para>If this <see cref="SqlOptions"/>.UseParameters is false, when this list is null.</para>
        /// </summary>
        protected IList<SqlItem> SqlItems { get; }
        /// <summary>
        /// A count of how many value that has been added by the CreateItemID methods.
        /// <para>If this <see cref="SqlOptions"/>.UseParameters is false, when the count will be 0.</para>
        /// </summary>
        protected int ItemIdCount { get; set; }

        /// <summary>
        /// Get the left char used to enclose a table or column name with.
        /// </summary>
        public char IdentifierLeft { get; }
        /// <summary>
        /// Get the right char used to enclose a table or column name with.
        /// </summary>
        public char IdentifierRight { get; }
        /// <summary>
        /// Get an indication of whether all values will be parameterized.
        /// </summary>
        public bool UseParameters { get; }

        public ISqlBase SqlBase { get; }

        public SqlOptions() : this('[', ']', new SqlServer.Base.SqlServerBase(), false) { }
        /// <param name="identifierLeft">The left char to enclose a table or column name with.</param>
        /// <param name="identifierRight">The right char to enclose a table or column name with.</param>
        /// <param name="useParameters">Should all value be parameterized.</param>
        public SqlOptions(char identifierLeft, char identifierRight, ISqlBase sqlBase, bool useParameters = true)
        {
            ItemIdCount = 0;
            IdentifierLeft = identifierLeft;
            IdentifierRight = identifierRight;
            SqlBase = sqlBase;
            UseParameters = useParameters;
            if (useParameters)
                SqlItems = new List<SqlItem>();
        }

        /// <summary>
        /// Get all values from all queries that are added to the <see cref="Sql"/> instance that this <see cref="SqlOptions"/> is part of.
        /// <para>If this <see cref="SqlOptions"/>.UseParameters is false, when this will returns null.</para>
        /// </summary>
        public virtual IEnumerable<SqlItem> GetSqlItems => SqlItems;
        /// <summary>
        /// Remove all items that has been added to this <see cref="SqlOptions"/>.
        /// </summary>
        public virtual void ClearItems()
        {
            SqlItems.Clear();
            ItemIdCount = 0;
        }
        /// <summary>
        /// Returns a enclosed table or column name.
        /// </summary>
        /// <param name="value">The table or column name to enclose.</param>
        public virtual string IdentifieName(string value)
        {
            return $"{IdentifierLeft}{value.Replace(".", $"{IdentifierRight}.{IdentifierLeft}")}{IdentifierRight}";
        }
        /// <summary>
        /// Returns a safe enclosed table or column name.
        /// </summary>
        /// <param name="value">The table or column name to enclose.</param>
        public virtual string SafeIdentifieName(string value)
        {
            return $"{IdentifierLeft}{value.Replace('.', '_').Replace('*', '_')}{IdentifierRight}";
        }
        /// <summary>
        /// Returns the sql type that is equal to the <see cref="CSharpType"/>, used then altering or creating columns.
        /// </summary>
        /// <param name="type">The C# type.</param>
        /// <param name="size">The size the sql type should have. Will be ignored if the <see cref="CSharpType"/> don't support it.</param>
        /// <param name="digits">Only used when the <see cref="CSharpType"/> is Decimal</param>
        public virtual string CSharpTypeToSqlDataType(CSharpType type, uint size = 0, int digits = 0) => SqlBase.CSharpTypeToSqlDataType(type, size, digits);
        /// <summary>
        /// Add a value that should be parameterized, and returns a id.
        /// If this <see cref="SqlOptions"/>.UseParameters is false, when the value will be returned as a string.
        /// </summary>
        /// <param name="value">The value to be added.</param>
        public virtual string CreateItemID(object value)
        {
            if (UseParameters)
            {
                var id = "@item" + ItemIdCount++;
                SqlItems.Add(new SqlItem(id, HandleObject(value)));
                return id;
            }

            var obj = HandleObject(value);
            return obj is string ? $"'{obj}'" : obj.ToString();
        }
        /// <summary>
        /// Returns a object that is ready for a sql query.
        /// </summary>
        /// <param name="obj">The object that should be made ready a sql query.</param>
        protected virtual object HandleObject(object obj) => SqlBase.HandleObject(obj);
    }
}
