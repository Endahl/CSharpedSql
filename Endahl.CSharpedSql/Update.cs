namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// A UPDATE statement in SQL.
    /// </summary>
    public class Update: ISqlStatement
    {
        /// <summary>
        /// Gets the name of the table that this <see cref="Update"/> is updating.
        /// </summary>
        public virtual string TableName { get; }
        /// <summary>
        /// Gets or sets the <see cref="CSharpedSql.Where"/> for this statement.
        /// Can be null.
        /// </summary>
        public virtual Where Where { get; set; }
        public virtual ColumnValue[] ColumnValues { get; }

        protected Update(string table, params ColumnValue[] columnValues)
        {
            ColumnValues = columnValues;
            TableName = table;
        }

        /// <summary>
        /// Return the <see cref="Update"/> statement as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        /// <summary>
        /// Return the <see cref="Update"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            return sql.SqlBase.Update(this, sql);
        }

        /// <summary>
        /// The WHERE clause is used to filter records.
        /// </summary>
        /// <param name="where">the WHERE Clause to add</param>
        public static Update operator +(Update update, Where where)
        {
            if (update.Where == null)
                update.Where = where;
            else
                update.Where.And(where);
            return update;
        }
        /// <summary>
        /// Add a another condition to the WHERE Clause that need to be true.
        /// </summary>
        /// <param name="where">The WHERE Clause to add</param>
        public static Update operator &(Update statement, Where where)
        {
            if (statement.Where == null)
                statement.Where = where;
            else
                statement.Where.And(where);
            return statement;
        }
        /// <summary>
        /// Add a another condition to the WHERE Clause that can be true instead.
        /// </summary>
        /// <param name="where">The WHERE Clause to add</param>
        public static Update operator |(Update statement, Where where)
        {
            if (statement.Where == null)
                throw new System.Exception("Can't use '|' then the statements 'Where' is null, use '+' or '&' insted!");
            else
                statement.Where.Or(where);
            return statement;
        }

        /// <summary>
        /// The UPDATE statement is used to modify the existing records in a table.
        /// </summary>
        /// <param name="table">the table to update on</param>
        /// <param name="values">the values to update with</param>
        public static Update Set(string table, params ColumnValue[] values)
        {
            return new Update(table, values);
        }
    }
}
