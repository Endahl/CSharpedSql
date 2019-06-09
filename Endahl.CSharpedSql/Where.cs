namespace Endahl.CSharpedSql
{
    using System.Collections.Generic;

    /// <summary>
    /// A WHERE Clause in SQL
    /// </summary>
    public class Where
    {
        private bool and = true;

        protected List<Where> wheres;

        /// <summary>
        /// Gets the <see cref="CSharpedSql.Condition"/> for this <see cref="Where"/>
        /// </summary>
        public virtual Condition Condition { get; }

        protected Where(Condition condition)
        {
            wheres = new List<Where>();
            Condition = condition;
        }

        /// <summary>
        /// Add a another condition to the WHERE Clause that need to be true
        /// </summary>
        /// <param name="where">The WHERE Clause to add</param>
        public virtual Where And(Where where)
        {
            wheres.Add(where);
            return this;
        }

        /// <summary>
        /// Add a another condition to the WHERE Clause that can be true instead
        /// </summary>
        /// <param name="where">The WHERE Clause to add</param>
        public virtual Where Or(Where where)
        {
            where.and = false;
            wheres.Add(where);
            return this;
        }

        /// <summary>
        /// Return the <see cref="Where"/> clause as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }

        /// <summary>
        /// Return the <see cref="Where"/> clause as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            return "WHERE " + ToStringWihtoutWhere(sql);
        }

        private string ToStringWihtoutWhere(SqlOptions sql)
        {
            var where = Condition.ToString(sql);

            foreach (var wh in wheres)
            {
                if (wh.and)
                    where += " AND " + wh.ToStringWihtoutWhere(sql);
                else
                    where += " OR " + wh.ToStringWihtoutWhere(sql);
            }
            return where;
        }

        /// <summary>
        /// Add a another condition to the WHERE Clause that need to be true. this is same as using 'And()'
        /// </summary>
        /// <param name="where1">The WHERE Clause to be add to</param>
        /// <param name="where2">The WHERE Clause to add</param>
        public static Where operator +(Where where1, Where where2)
        {
            return where1.And(where2);
        }

        /// <summary>
        /// Add a another condition to the WHERE Clause that need to be true. this is same as using 'And()'
        /// </summary>
        /// <param name="where1">The WHERE Clause to be add to</param>
        /// <param name="where2">The WHERE Clause to add</param>
        public static Where operator &(Where where1, Where where2)
        {
            return where1.And(where2);
        }

        /// <summary>
        /// Add a another condition to the WHERE Clause that can be true instead. this is same as using 'Or()'
        /// </summary>
        /// <param name="where1">The WHERE Clause to be add to</param>
        /// <param name="where2">The WHERE Clause to add</param>
        public static Where operator |(Where where1, Where where2)
        {
            return where1.Or(where2);
        }

        /// <summary>
        /// Returns a WHERE Clause that selects values within a given range. The values can be numbers, text, or dates.
        /// </summary>
        /// <param name="column">the column to select from</param>
        /// <param name="start">the start value</param>
        /// <param name="end">the end value</param>
        public static Where Between(string column, object start, object end)
        {
            return new Where(Condition.Between(column, start, end));
        }

        /// <summary>
        /// Returns a WHERE Clause that selects values outside a given range. The values can be numbers, text, or dates.
        /// </summary>
        /// <param name="column">the column to select from</param>
        /// <param name="start">the start value</param>
        /// <param name="end">the end value</param>
        public static Where NotBetween(string column, object start, object end)
        {
            return new Where(Condition.NotBetween(column, start, end));
        }

        /// <summary>
        /// Returns a WHERE Clause that selects all values that are equal to the value
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="value">the value the column should be</param>
        public static Where Equal(string column, object value)
        {
            return new Where(Condition.Equal(column, value));
        }

        /// <summary>
        /// Returns a WHERE Clause that selects all values that are not equal to the value
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="value">the value the column should not be</param>
        public static Where NotEqual(string column, object value)
        {
            return new Where(Condition.NotEqual(column, value));
        }

        /// <summary>
        /// Returns a WHERE Clause that selects all values that are greater than the value
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="value">the value the column should be greater than</param>
        public static Where GreaterThan(string column, object value)
        {
            return new Where(Condition.GreaterThan(column, value));
        }

        /// <summary>
        /// Returns a WHERE Clause that selects all values that are less than the value
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="value">the value the column should be less than</param>
        public static Where LessThan(string column, object value)
        {
            return new Where(Condition.LessThan(column, value));
        }

        /// <summary>
        /// Return a WHERE Clause that can search for a specified pattern in a column
        /// </summary>
        /// <param name="column">the column to search in</param>
        /// <param name="value">the value to search with</param>
        public static Where Like(string column, object value)
        {
            return new Where(Condition.Like(column, value));
        }

        /// <summary>
        /// Returns a WHERE Clause that selects from where the column is null
        /// </summary>
        /// <param name="column">the column to look at</param>
        public static Where IsNull(string column)
        {
            return new Where(Condition.IsNull(column));
        }

        /// <summary>
        /// Returns a WHERE Clause that selects from where the column is not null
        /// </summary>
        /// <param name="column">the column to look at</param>
        public static Where IsNotNull(string column)
        {
            return new Where(Condition.IsNotNull(column));
        }

        /// <summary>
        /// The EXISTS operator is used to test for the existence of any record in a subquery.
        /// The EXISTS operator returns true if the subquery returns one or more records.
        /// </summary>
        /// <param name="select">the SELECT statement to check on</param>
        public static Where Exists(Select select)
        {
            return new Where(Condition.Exists(select));
        }

        /// <summary>
        /// The EXISTS operator is used to test for the existence of any record in a subquery.
        /// The EXISTS operator returns true if the subquery returns no records.
        /// </summary>
        /// <param name="select">the SELECT statement to check on</param>
        public static Where NotExists(Select select)
        {
            return new Where(Condition.NotExists(select));
        }

        /// <summary>
        /// The ANY operator returns TRUE if any of the subquery values meet the condition.
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="select">the SELECT statement to check on</param>
        public static Where Any(string column, Select select)
        {
            return new Where(Condition.Any(column, select));
        }

        /// <summary>
        /// The ALL operator returns TRUE if all of the subquery values meet the condition.
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="select">the SELECT statement to check on</param>
        public static Where All(string column, Select select)
        {
            return new Where(Condition.All(column, select));
        }

        public static Where In(string coulmn, Select select)
        {
            return new Where(Condition.In(coulmn, select));
        }

        public static Where NotIn(string coulmn, Select select)
        {
            return new Where(Condition.NotIn(coulmn, select));
        }
    }
}
