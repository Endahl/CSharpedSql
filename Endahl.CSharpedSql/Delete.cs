namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// A DELETE statement in SQL
    /// </summary>
    public class Delete: ISqlStatement
    {
        /// <summary>
        /// Gets the name of the table that this <see cref="Delete"/> is selecting from.
        /// </summary>
        public virtual string TableName { get; }
        /// <summary>
        /// Gets or sets the <see cref="CSharpedSql.Where"/> for this statement.
        /// Can be null.
        /// </summary>
        public virtual Where Where { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="CSharpedSql.Join"/> for this <see cref="Delete"/>.
        /// Can be null.
        /// </summary>
        public virtual Join Join { get; set; }

        protected Delete(string table)
        {
            TableName = table;
        }

        /// <summary>
        /// Return the <see cref="Delete"/> statment as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }

        /// <summary>
        /// Return the <see cref="Delete"/> statment as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            return sql.SqlBase.Delete(this, sql);
        }

        /// <summary>
        /// The WHERE clause is used to filter records.
        /// </summary>
        /// <param name="where">the WHERE Clause to add</param>
        public static Delete operator +(Delete delete, Where where)
        {
            if (delete.Where == null)
                delete.Where = where;
            else
                delete.Where.And(where);
            return delete;
        }
        /// <summary>
        /// Add a another condition to the WHERE Clause that need to be true.
        /// </summary>
        /// <param name="where">The WHERE Clause to add</param>
        public static Delete operator &(Delete statement, Where where)
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
        public static Delete operator |(Delete statement, Where where)
        {
            if (statement.Where == null)
                throw new System.Exception("Can't use '|' then the statements 'Where' is null, use '+' or '&' insted!");
            else
                statement.Where.Or(where);
            return statement;
        }

        /// <summary>
        /// A JOIN clause is used to combine rows from two or more tables, based on a related column between them.
        /// <para>Remember to add the table names on columns in the DELETE statement.
        /// It is not needed in the JOIN clause.</para>
        /// </summary>
        /// <param name="join">the JOIN Clause to add</param>
        public static Delete operator +(Delete delete, Join join)
        {
            if (delete.Join == null)
                delete.Join = join;
            else
                delete.Join.Joins.Add(join);
            return delete;
        }

        /// <summary>
        /// The DELETE statement is used to delete existing records in a table.
        /// </summary>
        /// <param name="table">the table to delete from</param>
        public static Delete From(string table)
        {
            return new Delete(table);
        }
    }
}
