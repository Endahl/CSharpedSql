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
            string s, v;
            s = v = "";
            if (ColumnValues.Length > 0)
            {
                s = sql.IdentifieName(ColumnValues[0].ColumnName);
                v = ColumnValues[0].Value is InsertFunction ? (ColumnValues[0].Value as InsertFunction).ToString(sql)
                    : sql.CreateItemID(ColumnValues[0].Value);
                for (var i = 1; i < ColumnValues.Length; i++)
                {
                    s += $", {sql.IdentifieName(ColumnValues[i].ColumnName)}";
                    v += ", " + (ColumnValues[i].Value is InsertFunction ? (ColumnValues[i].Value as InsertFunction).ToString(sql)
                                : sql.CreateItemID(ColumnValues[i].Value));
                }
            }
            return $"INSERT INTO {sql.IdentifieName(TableName)} ({s}) VALUES ({v})";
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
