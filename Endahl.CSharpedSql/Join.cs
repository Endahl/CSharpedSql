namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System.Collections.Generic;

    /// <summary>
    /// JOIN clause in SQL
    /// </summary>
    public class Join
    {
        public virtual IList<Join> Joins { get; }
        public virtual string TableName { get; }
        public virtual string TableName2 { get; }
        public virtual JoinType JoinType { get; }
        public virtual string Column { get; }
        public virtual string Column2 { get; }

        protected Join(JoinType join, string table, string column, string table2, string column2)
        {
            Joins = new List<Join>();
            TableName = table;
            TableName2 = table2;
            JoinType = join;
            Column = column;
            Column2 = column2;
        }

        public static Join operator +(Join join, Join join1)
        {
            join.Joins.Add(join1);
            return join;
        }

        /// <summary>
        /// Return the <see cref="Join"/> clause as a string. the string will be missing a table name.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }

        /// <summary>
        /// Return the <see cref="Join"/> clause as a string
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            return sql.SqlBase.Join(this, sql);
        }

        /// <summary>
        /// The INNER JOIN keyword selects records that have matching values in both tables.
        /// </summary>
        /// <param name="table">the table to join with</param>
        /// <param name="table2">the table to join with</param>
        /// <param name="column">the column from the table to join on</param>
        /// <param name="column2">the column from the table to join with</param>
        public static Join Inner(string table, string column, string table2, string column2)
        {
            return new Join(JoinType.Inner, table, column, table2, column2);
        }

        /// <summary>
        /// The LEFT JOIN keyword returns all records from the left table (table1),
        /// and the matched records from the right table (table2).
        /// The result is NULL from the right side, if there is no match.
        /// </summary>
        /// <param name="table">the table to join with</param>
        /// <param name="table2">the table to join with</param>
        /// <param name="column">the column from the table to join on</param>
        /// <param name="column2">the column from the table to join with</param>
        public static Join Left(string table, string column, string table2, string column2)
        {
            return new Join(JoinType.Left, table, column, table2, column2);
        }

        /// <summary>
        /// The RIGHT JOIN keyword returns all records from the right table (table2),
        /// and the matched records from the left table (table1).
        /// The result is NULL from the left side, when there is no match.
        /// </summary>
        /// <param name="table">the table to join with</param>
        /// <param name="table2">the table to join with</param>
        /// <param name="column">the column from the table to join on</param>
        /// <param name="column2">the column from the table to join with</param>
        public static Join Right(string table, string column, string table2, string column2)
        {
            return new Join(JoinType.Right, table, column, table2, column2);
        }

        /// <summary>
        /// he FULL OUTER JOIN keyword return all records when there is a match in either left (table1) or right (table2) table records.
        /// <para>Note: FULL OUTER JOIN can potentially return very large result-sets!</para>
        /// </summary>
        /// <param name="table">the table to join with</param>
        /// <param name="table2">the table to join with</param>
        /// <param name="column">the column from the table to join on</param>
        /// <param name="column2">the column from the table to join with</param>
        public static Join Full(string table, string column, string table2, string column2)
        {
            return new Join(JoinType.FullOuter, table, column, table2, column2);
        }
    }
}
