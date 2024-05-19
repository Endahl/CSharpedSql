namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

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

        /// <summary>
        /// Returns the <see cref="Drop"/> statement as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="Drop"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => sql.SqlBase.Drop(this, sql);

        /// <summary>
        /// Delete a table and all information in it from the database
        /// <para>Be careful before dropping a table. Deleting a table will result in loss of complete information stored in the table!</para>
        /// </summary>
        /// <param name="table">The table to drop</param>
        /// <returns></returns>
        public static Drop Table(string table)
        {
            return new(table, false);
        }

        /// <summary>
        /// Delete a table if it exists and all information in it from the database
        /// <para>Be careful before dropping a table. Deleting a table will result in loss of complete information stored in the table!</para>
        /// </summary>
        /// <param name="table">The table to drop</param>
        /// <returns></returns>
        public static Drop TableIfExists(string table)
        {
            return new(table, true);
        }
    }
}
