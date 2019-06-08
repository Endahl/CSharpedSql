namespace Endahl.CSharpedSql
{
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

        public override string ToString()
        {
            return ToString(new SqlOptions());
        }

        public virtual string ToString(SqlOptions sql)
        {
            switch (ConditionType)
            {
                case ConditionType.Between:
                    return $"{Column.ToString(sql)} BETWEEN {sql.CreateItemID(Item)} AND {sql.CreateItemID(Item2)}";
                case ConditionType.NotBetween:
                    return $"{Column.ToString(sql)} NOT BETWEEN {sql.CreateItemID(Item)} AND {sql.CreateItemID(Item2)}";
                case ConditionType.Equal:
                    return ConditionToString("=", sql);
                case ConditionType.NotEqual:
                    return ConditionToString("<>", sql);
                case ConditionType.GreaterThan:
                    return ConditionToString(">", sql);
                case ConditionType.LessThan:
                    return ConditionToString("<", sql);
                case ConditionType.Like:
                    return ConditionToString("LIKE", sql);
                case ConditionType.IsNull:
                    return $"{Column.ToString(sql)} IS NULL";
                case ConditionType.IsNotNull:
                    return $"{Column.ToString(sql)} IS NOT NULL";
                case ConditionType.All:
                    return $"{Column.ToString(sql)} = ALL ({(Item as Select).ToString(sql)})";
                case ConditionType.Any:
                    return $"{Column.ToString(sql)} = ANY ({(Item as Select).ToString(sql)})";
                case ConditionType.Exists:
                    return $"EXISTS ({(Item as Select).ToString(sql)})";
                case ConditionType.NotExists:
                    return $"NOT EXISTS ({(Item as Select).ToString(sql)})";
                case ConditionType.In:
                    return $"{Column.ToString(sql)} IN ({(Item as Select).ToString(sql)})";
                case ConditionType.NotIn:
                    return $"{Column.ToString(sql)} NOT IN ({(Item as Select).ToString(sql)})";
                default:
                    return "";
            }
        }

        private string ConditionToString(string condition, SqlOptions sql)
        {
            var result = Column != null ? Column.ToString(sql) : sql.CreateItemID(Item);
            return $"{result} {condition} {sql.CreateItemID(Item2)}";
        }

        public static Condition All(ColumnItem actualColumn, Select expected)
        {
            return new Condition(ConditionType.All, actualColumn, expected);
        }

        public static Condition Any(ColumnItem actualColumn, Select expected)
        {
            return new Condition(ConditionType.Any, actualColumn, expected);
        }

        public static Condition Between(ColumnItem column, object start, object end)
        {
            return new Condition(ConditionType.Between, column, start, end);
        }

        public static Condition NotBetween(ColumnItem column, object start, object end)
        {
            return new Condition(ConditionType.NotBetween, column, start, end);
        }

        public static Condition Equal(ColumnItem actualColumn, object expected)
        {
            return new Condition(ConditionType.Equal, actualColumn, expected);
        }

        public static Condition Equal(object actual, object expected)
        {
            return new Condition(ConditionType.Equal, actual, expected);
        }

        public static Condition NotEqual(ColumnItem actualColumn, object expected)
        {
            return new Condition(ConditionType.NotEqual, actualColumn, expected);
        }

        public static Condition NotEqual(object actual, object expected)
        {
            return new Condition(ConditionType.NotEqual, actual, expected);
        }

        public static Condition Exists(Select select)
        {
            return new Condition(ConditionType.Exists, select);
        }

        public static Condition NotExists(Select select)
        {
            return new Condition(ConditionType.NotExists, select);
        }

        public static Condition GreaterThan(ColumnItem actualColumn, object expected)
        {
            return new Condition(ConditionType.GreaterThan, actualColumn, expected);
        }

        public static Condition GreaterThan(object actual, object expected)
        {
            return new Condition(ConditionType.GreaterThan, actual, expected);
        }

        public static Condition LessThan(ColumnItem actualColumn, object expected)
        {
            return new Condition(ConditionType.LessThan, actualColumn, expected);
        }

        public static Condition LessThan(object actual, object expected)
        {
            return new Condition(ConditionType.LessThan, actual, expected);
        }

        public static Condition Like(ColumnItem actualColumn, object like)
        {
            return new Condition(ConditionType.Like, actualColumn, like);
        }

        public static Condition IsNull(ColumnItem actualColumn)
        {
            return new Condition(ConditionType.IsNull, actualColumn);
        }

        public static Condition IsNotNull(ColumnItem actualColumn)
        {
            return new Condition(ConditionType.IsNotNull, actualColumn);
        }

        public static Condition In(ColumnItem actualColumn, Select select)
        {
            return new Condition(ConditionType.In, actualColumn, select);
        }

        public static Condition NotIn(ColumnItem actualColumn, Select select)
        {
            return new Condition(ConditionType.NotIn, actualColumn, select);
        }
    }

    public enum ConditionType
    {
        All,
        Any,
        Between,
        NotBetween,
        /// <summary>
        /// =
        /// </summary>
        Equal,
        /// <summary>
        /// !=
        /// </summary>
        NotEqual,
        Exists,
        NotExists,
        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan,
        /// <summary>
        /// &lt;
        /// </summary>
        LessThan,
        Like,
        In,
        NotIn,
        /// <summary>
        /// = null
        /// </summary>
        IsNull,
        /// <summary>
        /// != null
        /// </summary>
        IsNotNull
    }
}
