namespace Endahl.CSharpedSql
{
    /// <summary>
    /// A DELETE statement in SQL
    /// </summary>
    public class Delete: ISqlStatement
    {
        public virtual string TableName { get; }
        public virtual Where Where { get; set; }

        protected Delete(string table)
        {
            TableName = table;
        }

        /// <summary>
        /// Return the DELETE statment as a string
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }

        /// <summary>
        /// Return the DELETE statment as a string
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            var statement = $"DELETE FROM {sql.IdentifieName(TableName)}";
            if (Where != null)
                statement += $"{Where.ToString(sql)}";
            return statement;
        }

        /// <summary>
        /// The WHERE clause is used to filter records.
        /// </summary>
        /// <param name="where">the WHERE Clause to add</param>
        public static Delete operator +(Delete delete, Where where)
        {
            delete.Where = where;
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
