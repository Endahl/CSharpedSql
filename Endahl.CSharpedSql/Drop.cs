namespace Endahl.CSharpedSql
{
    /// <summary>
    /// A DROP statement in SQL
    /// </summary>
    public class Drop: ISqlStatement
    {
        public virtual bool DropIfTableExists { get; }
        public virtual string TableName { get; }

        protected Drop(string name, bool ifTableExists)
        {
            TableName = name;
            DropIfTableExists = ifTableExists;
        }

        public override string ToString()
        {
            return ToString(new SqlOptions());
        }

        public virtual string ToString(SqlOptions sql)
        {
            return DropIfTableExists ?
                (sql.SqlLanguage == SqlLanguage.SqlServer
                ? $"IF(object_id(N'{sql.IdentifieName(TableName)}', N'U') IS NULL BEGIN DROP TABLE {sql.IdentifieName(TableName)}"
                : $"DROP TABLE IF EXISTS {sql.IdentifieName(TableName)}")
                : $"DROP TABLE {sql.IdentifieName(TableName)}";
        }

        /// <summary>
        /// Delete a table and all information in it from the database
        /// <para>Be careful before dropping a table. Deleting a table will result in loss of complete information stored in the table!</para>
        /// </summary>
        /// <param name="table">The table to drop</param>
        /// <returns></returns>
        public static Drop Table(string table)
        {
            return new Drop(table, false);
        }

        /// <summary>
        /// Delete a table if it exists and all information in it from the database
        /// <para>Be careful before dropping a table. Deleting a table will result in loss of complete information stored in the table!</para>
        /// </summary>
        /// <param name="table">The table to drop</param>
        /// <returns></returns>
        public static Drop TableIfExists(string table)
        {
            return new Drop(table, true);
        }
    }
}
