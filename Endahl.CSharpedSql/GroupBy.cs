﻿namespace Endahl.CSharpedSql
{
    public class GroupBy
    {
        public virtual string[] ColumnNames { get; }

        protected GroupBy(string columnName, string[] columnNames)
        {
            var arr = new string[1 + columnNames.Length];
            arr[0] = columnName;
            for (var i = 1; i < arr.Length; i++)
                arr[i] = columnNames[i - 1];

            ColumnNames = arr;
        }

        /// <summary>
        /// Returns the <see cref="GroupBy"/> as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        /// <summary>
        /// Returns the <see cref="GroupBy"/> as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            var result = $"GROUP BY {sql.IdentifieName(ColumnNames[0])}";
            for (var i = 1; i < ColumnNames.Length; i++)
                result += $", {sql.IdentifieName(ColumnNames[i])}";
            return result;
        }

        public static GroupBy Column(string columnName, params string[] columnNames)
        {
            return new GroupBy(columnName, columnNames);
        }
    }
}
