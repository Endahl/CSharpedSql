namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// A INSERT statement in SQL
    /// </summary>
    public class Insert: ISqlStatement
    {
        public string TableName { get; }

        public ColumnValue[] ColumnValues { get; }

        protected Insert(string table, params ColumnValue[] columnValues)
        {
            TableName = table;
            ColumnValues = columnValues;
        }

        /// <summary>
        /// Return the <see cref="Insert"/> statement as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        /// <summary>
        /// Return the <see cref="Insert"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            return sql.SqlBase.Insert(this, sql);
        }

        /// <summary>
        /// The INSERT INTO statement is used to insert new records in a table.
        /// </summary>
        /// <param name="table">the table to insert into</param>
        /// <param name="values">the values to insert</param>
        public static Insert Into(string table, params ColumnValue[] values)
        {
            return new Insert(table, values);
        }
    }
}
