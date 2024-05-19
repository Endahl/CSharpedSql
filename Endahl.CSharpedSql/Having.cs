namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System.Collections.Generic;

    public class Having
    {
        public bool IsAnd { get; protected set; } = true;

        public List<Having> Havings { get; }

        public virtual Condition Condition { get; }

        protected Having(Condition condition)
        {
            Havings = new List<Having>();
            Condition = condition;
        }

        /// <summary>
        /// Add a another condition to the HAVING Clause that need to be true
        /// </summary>
        /// <param name="having">The HAVING Clause to add</param>
        public virtual Having And(Having having)
        {
            System.ArgumentNullException.ThrowIfNull(having);
            Havings.Add(having);
            return this;
        }

        /// <summary>
        /// Add a another condition to the HAVING Clause that can be true instead
        /// </summary>
        /// <param name="having">The HAVING Clause to add</param>
        public virtual Having Or(Having having)
        {
            System.ArgumentNullException.ThrowIfNull(having);
            having.IsAnd = false;
            Havings.Add(having);
            return this;
        }

        /// <summary>
        /// Return the <see cref="Having"/> clause as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Return the <see cref="Having"/> clause as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => sql.SqlBase.Having(this, sql);

        /// <summary>
        /// Add a another condition to the HAVING Clause that need to be true. this is same as using 'And()'
        /// </summary>
        /// <param name="having1">The HAVING Clause to be add to</param>
        /// <param name="having2">The HAVING Clause to add</param>
        public static Having operator +(Having having1, Having having2)
        {
            return having1.And(having2);
        }
        /// <summary>
        /// Add a another condition to the HAVING Clause that need to be true. this is same as using 'And()'
        /// </summary>
        /// <param name="having1">The HAVING Clause to be add to</param>
        /// <param name="having2">The HAVING Clause to add</param>
        public static Having operator &(Having having1, Having having2)
        {
            return having1.And(having2);
        }
        /// <summary>
        /// Add a another condition to the HAVING Clause that can be true instead. this is same as using 'Or()'
        /// </summary>
        /// <param name="having1">The HAVING Clause to be add to</param>
        /// <param name="having2">The HAVING Clause to add</param>
        public static Having operator |(Having having1, Having having2)
        {
            return having1.Or(having2);
        }

        /// <summary>
        /// Returns a HAVING Clause that selects values within a given range. The values can be numbers, text, or dates.
        /// </summary>
        /// <param name="column">the column to select from</param>
        /// <param name="start">the start value</param>
        /// <param name="end">the end value</param>
        public static Having Between(ColumnItem column, object start, object end)
        {
            return new(Condition.Between(column, start, end));
        }
        /// <summary>
        /// Returns a HAVING Clause that selects values outside a given range. The values can be numbers, text, or dates.
        /// </summary>
        /// <param name="column">the column to select from</param>
        /// <param name="start">the start value</param>
        /// <param name="end">the end value</param>
        public static Having NotBetween(ColumnItem column, object start, object end)
        {
            return new(Condition.NotBetween(column, start, end));
        }
        /// <summary>
        /// Returns a HAVING Clause that selects all values that are equal to the value
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="value">the value the column should be</param>
        public static Having Equal(ColumnItem column, object value)
        {
            return new(Condition.Equal(column, value));
        }
        /// <summary>
        /// Returns a HAVING Clause that selects all values that are not equal to the value
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="value">the value the column should not be</param>
        public static Having NotEqual(ColumnItem column, object value)
        {
            return new(Condition.NotEqual(column, value));
        }
        /// <summary>
        /// Returns a HAVING Clause that selects all values that are greater than the value
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="value">the value the column should be greater than</param>
        public static Having GreaterThan(ColumnItem column, object value)
        {
            return new(Condition.GreaterThan(column, value));
        }
        /// <summary>
        /// Returns a HAVING Clause that selects all values that are less than the value
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="value">the value the column should be less than</param>
        public static Having LessThan(ColumnItem column, object value)
        {
            return new(Condition.LessThan(column, value));
        }
        /// <summary>
        /// Return a HAVING Clause that can search for a specified pattern in a column
        /// </summary>
        /// <param name="column">the column to search in</param>
        /// <param name="value">the value to search with</param>
        public static Having Like(ColumnItem column, object value)
        {
            return new(Condition.Like(column, value));
        }
        /// <summary>
        /// Returns a HAVING Clause that selects from having the column is null
        /// </summary>
        /// <param name="column">the column to look at</param>
        public static Having IsNull(ColumnItem column)
        {
            return new(Condition.IsNull(column));
        }
        /// <summary>
        /// Returns a HAVING Clause that selects from having the column is not null
        /// </summary>
        /// <param name="column">the column to look at</param>
        public static Having IsNotNull(ColumnItem column)
        {
            return new(Condition.IsNotNull(column));
        }
        /// <summary>
        /// The EXISTS operator is used to test for the existence of any record in a subquery.
        /// The EXISTS operator returns true if the subquery returns one or more records.
        /// </summary>
        /// <param name="select">the SELECT statement to check on</param>
        public static Having Exists(Select select)
        {
            return new(Condition.Exists(select));
        }
        /// <summary>
        /// The EXISTS operator is used to test for the existence of any record in a subquery.
        /// The EXISTS operator returns true if the subquery returns no records.
        /// </summary>
        /// <param name="select">the SELECT statement to check on</param>
        public static Having NotExists(Select select)
        {
            return new(Condition.NotExists(select));
        }
        /// <summary>
        /// The ANY operator returns TRUE if any of the subquery values meet the condition.
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="select">the SELECT statement to check on</param>
        public static Having Any(ColumnItem column, Select select)
        {
            return new(Condition.Any(column, select));
        }
        /// <summary>
        /// The ALL operator returns TRUE if all of the subquery values meet the condition.
        /// </summary>
        /// <param name="column">the column to look at</param>
        /// <param name="select">the SELECT statement to check on</param>
        public static Having All(ColumnItem column, Select select)
        {
            return new(Condition.All(column, select));
        }

        public static Having In(ColumnItem column, Select select)
        {
            return new(Condition.In(column, select));
        }

        public static Having NotIn(ColumnItem column, Select select)
        {
            return new(Condition.NotIn(column, select));
        }
    }
}
