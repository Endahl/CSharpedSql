namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// A ORDER BY keyword in Sql
    /// </summary>
    public class OrderBy
    {
        public virtual ColumnItem[] Columns { get; }
        public virtual OrderByType OrderByType { get; }

        protected OrderBy(OrderByType orderByType, ColumnItem columnName, ColumnItem[] columnNames)
        {
            var arr = new ColumnItem[1 + columnNames.Length];
            arr[0] = columnName;
            for (var i = 1; i < arr.Length; i++)
                arr[i] = columnNames[i - 1];
            Columns = arr;
            OrderByType = orderByType;
        }

        /// <summary>
        /// Return the <see cref="OrderBy"/> as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Return the <see cref="OrderBy"/> as a string.
        /// </summary>
        public string ToString(SqlOptions sql) => sql.SqlBase.OrderBy(this, sql);

        /// <summary>
        /// Sortde ascending by the column
        /// </summary>
        /// <param name="columnName">the column to order by</param>
        public static OrderBy ASC(ColumnItem columnName, params ColumnItem[] columnNames)
        {
            return new(OrderByType.ASC, columnName, columnNames);
        }
        /// <summary>
        /// Sortde descending by the column
        /// </summary>
        /// <param name="columnName">the column to order by</param>
        public static OrderBy DESC(ColumnItem columnName, params ColumnItem[] columnNames)
        {
            return new(OrderByType.DESC, columnName, columnNames);
        }
    }
}
