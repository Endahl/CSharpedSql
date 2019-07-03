namespace Endahl.CSharpedSql
{
    /// <summary>
    /// A UPDATE statement in SQL.
    /// </summary>
    public class Update: ISqlStatement
    {
        public virtual string TableName { get; }
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
            var statement = $"UPDATE {sql.IdentifieName(TableName)} SET {sql.IdentifieName(ColumnValues[0].ColumnName)} " +
                $"= {sql.CreateItemID(ColumnValues[0].Value)}";
            foreach (var item in ColumnValues)
                statement += $", {sql.IdentifieName(item.ColumnName)} = {sql.CreateItemID(item.Value)}";
            if (Where != null)
                statement += " " + Where.ToString(sql);
            return statement;
        }

        /// <summary>
        /// The WHERE clause is used to filter records.
        /// </summary>
        /// <param name="where">the WHERE Clause to add</param>
        public static Update operator +(Update update, Where where)
        {
            update.Where = where;
            return update;
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
