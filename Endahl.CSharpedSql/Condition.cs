namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    public class Condition
    {
        public virtual ConditionType ConditionType { get; }
        public virtual ColumnItem Column { get; }
        public virtual object Item { get; }
        public virtual object Item2 { get; }

        protected Condition(ConditionType condition, ColumnItem column)
        {
            ConditionType = condition;
            Column = column;
        }

        protected Condition(ConditionType condition, object item)
        {
            ConditionType = condition;
            Item = item;
        }

        protected Condition(ConditionType condition, ColumnItem column, object item2)
        {
            ConditionType = condition;
            Column = column;
            Item2 = item2;
        }

        protected Condition(ConditionType condition, Select item)
        {
            ConditionType = condition;
            Item = item;
        }

        protected Condition(ConditionType condition, ColumnItem column, Select item)
        {
            ConditionType = condition;
            Column = column;
            Item = item;
        }

        protected Condition(ConditionType condition, object item, object item2)
        {
            ConditionType = condition;
            Item = item;
            Item2 = item2;
        }

        protected Condition(ConditionType condition, ColumnItem column, object item, object item2)
        {
            ConditionType = condition;
            Column = column;
            Item = item;
            Item2 = item2;
        }

        /// <summary>
        /// Returns the <see cref="Condition"/> as a string.
        /// </summary>
        public override string ToString() =>ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="Condition"/> as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => sql.SqlBase.Condition(this, sql);

        public static Condition All(ColumnItem actualColumn, Select expected)
        {
            return new(ConditionType.All, actualColumn, expected);
        }

        public static Condition Any(ColumnItem actualColumn, Select expected)
        {
            return new(ConditionType.Any, actualColumn, expected);
        }

        public static Condition Between(ColumnItem column, object start, object end)
        {
            return new(ConditionType.Between, column, start, end);
        }

        public static Condition NotBetween(ColumnItem column, object start, object end)
        {
            return new(ConditionType.NotBetween, column, start, end);
        }

        public static Condition Equal(ColumnItem actualColumn, object expected)
        {
            return new(ConditionType.Equal, actualColumn, expected);
        }

        public static Condition Equal(object actual, object expected)
        {
            return new(ConditionType.Equal, actual, expected);
        }

        public static Condition NotEqual(ColumnItem actualColumn, object expected)
        {
            return new(ConditionType.NotEqual, actualColumn, expected);
        }

        public static Condition NotEqual(object actual, object expected)
        {
            return new(ConditionType.NotEqual, actual, expected);
        }

        public static Condition Exists(Select select)
        {
            return new(ConditionType.Exists, select);
        }

        public static Condition NotExists(Select select)
        {
            return new(ConditionType.NotExists, select);
        }

        public static Condition GreaterThan(ColumnItem actualColumn, object expected)
        {
            return new(ConditionType.GreaterThan, actualColumn, expected);
        }

        public static Condition GreaterThan(object actual, object expected)
        {
            return new(ConditionType.GreaterThan, actual, expected);
        }

        public static Condition LessThan(ColumnItem actualColumn, object expected)
        {
            return new(ConditionType.LessThan, actualColumn, expected);
        }

        public static Condition LessThan(object actual, object expected)
        {
            return new(ConditionType.LessThan, actual, expected);
        }

        public static Condition Like(ColumnItem actualColumn, object like)
        {
            return new(ConditionType.Like, actualColumn, like);
        }

        public static Condition IsNull(ColumnItem actualColumn)
        {
            return new(ConditionType.IsNull, actualColumn);
        }

        public static Condition IsNotNull(ColumnItem actualColumn)
        {
            return new(ConditionType.IsNotNull, actualColumn);
        }

        public static Condition In(ColumnItem actualColumn, Select select)
        {
            return new(ConditionType.In, actualColumn, select);
        }

        public static Condition NotIn(ColumnItem actualColumn, Select select)
        {
            return new(ConditionType.NotIn, actualColumn, select);
        }
    }
}
