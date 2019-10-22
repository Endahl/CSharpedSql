namespace Endahl.CSharpedSql
{
    /// <summary>
    /// A DELETE statement in SQL
    /// </summary>
    public class Delete: ISqlStatement
    {
        public virtual string TableName { get; }
        public virtual Where Where { get; set; }
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
            var statement = $"DELETE FROM {sql.IdentifieName(TableName)}";
            if (Join != null)
                statement += $" {Join.ToString(TableName, sql)}";
            if (Where != null)
                statement += $" {Where.ToString(sql)}";
            return statement;
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
        /// A JOIN clause is used to combine rows from two or more tables, based on a related column between them.
        /// <para>Remember to add the table names on columns in the DELETE statement.
        /// It is not needed in the JOIN clause.</para>
        /// </summary>
        /// <param name="join">the JOIN Clause to add</param>
        public static Delete operator +(Delete delete, Join join)
        {
            delete.Join = join;
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
