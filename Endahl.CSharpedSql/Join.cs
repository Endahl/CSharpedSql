namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// JOIN clause in SQL
    /// </summary>
    public class Join
    {
        public virtual string TableName { get; }
        public virtual JoinType JoinType { get; }
        public virtual string ColumnFrom { get; }
        public virtual string Column { get; }

        protected Join(JoinType join, string table, string columnFrom, string column)
        {
            TableName = table;
            JoinType = join;
            ColumnFrom = columnFrom;
            Column = column;
        }

        /// <summary>
        /// Return the <see cref="Join"/> clause as a string. the string will be missing a table name.
        /// </summary>
        public override string ToString()
        {
            return ToString("", new SqlOptions());
        }

        /// <summary>
        /// Return the <see cref="Join"/> clause as a string
        /// </summary>
        public virtual string ToString(string table1, SqlOptions sql)
        {
            string join;
            if (JoinType == JoinType.FullOuter)
                join = "FULL OUTER";
            else
                join = JoinType.ToString().ToUpper();

            return $"{join} JOIN {sql.IdentifieName(TableName)} ON " +
                $"{sql.IdentifieName(table1)}.{sql.IdentifieName(ColumnFrom)} = " +
                $"{sql.IdentifieName(TableName)}.{sql.IdentifieName(Column)}";
        }

        /// <summary>
        /// The INNER JOIN keyword selects records that have matching values in both tables.
        /// </summary>
        /// <param name="table2">the table to join with</param>
        /// <param name="columnFrom">the column from the table to join on</param>
        /// <param name="column">the column from the table to join with</param>
        public static Join Inner(string table2, string columnFrom, string column)
        {
            return new Join(JoinType.Inner, table2, columnFrom, column);
        }

        /// <summary>
        /// The LEFT JOIN keyword returns all records from the left table (table1),
        /// and the matched records from the right table (table2).
        /// The result is NULL from the right side, if there is no match.
        /// </summary>
        /// <param name="table2">the table to join with</param>
        /// <param name="columnFrom">the column from the table to join on</param>
        /// <param name="column">the column from the table to join with</param>
        public static Join Left(string table2, string columnFrom, string column)
        {
            return new Join(JoinType.Left, table2, columnFrom, column);
        }

        /// <summary>
        /// The RIGHT JOIN keyword returns all records from the right table (table2),
        /// and the matched records from the left table (table1).
        /// The result is NULL from the left side, when there is no match.
        /// </summary>
        /// <param name="table2">the table to join with</param>
        /// <param name="columnFrom">the column from the table to join on</param>
        /// <param name="column">the column from the table to join with</param>
        public static Join Right(string table2, string columnFrom, string column)
        {
            return new Join(JoinType.Right, table2, columnFrom, column);
        }

        /// <summary>
        /// he FULL OUTER JOIN keyword return all records when there is a match in either left (table1) or right (table2) table records.
        /// <para>Note: FULL OUTER JOIN can potentially return very large result-sets!</para>
        /// </summary>
        /// <param name="table2">the table to join with</param>
        /// <param name="columnFrom">the column from the table to join on</param>
        /// <param name="column">the column from the table to join with</param>
        public static Join Full(string table2, string columnFrom, string column)
        {
            return new Join(JoinType.FullOuter, table2, columnFrom, column);
        }
    }
}
