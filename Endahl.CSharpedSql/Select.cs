namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// A SELECT statement for SQL.
    /// </summary>
    public class Select: ISqlStatement
    {
        /// <summary>
        /// The limit of rows to find. Is only sets then the method <see cref="Select"/>.TopFrom() is called.
        /// </summary>
        public virtual int Top { get; protected set; } = 0;

        /// <summary>
        /// Specify which row to start retrieving data from. Is only sets then the method <see cref="Select"/>.TopFrom() is called.
        /// </summary>
        public virtual int Offset { get; protected set; } = -1;

        /// <summary>
        /// Gets the name of the table that this <see cref="Select"/> is selecting from.
        /// </summary>
        public virtual string TableName { get; }
        /// <summary>
        /// Gets the columns, cases, values or functions that this <see cref="Select"/> is selecting.
        /// </summary>
        public virtual ColumnItem[] Columns { get; }
        /// <summary>
        /// Gets the type this <see cref="Select"/> is.
        /// </summary>
        public virtual SelectType SelectType { get; }
        /// <summary>
        /// Gets or sets the <see cref="CSharpedSql.Where"/> for this statement.
        /// Can be null.
        /// </summary>
        public virtual Where Where { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="CSharpedSql.Join"/> for this <see cref="Select"/>.
        /// Can be null.
        /// </summary>
        public virtual Join Join { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="CSharpedSql.Having"/> for this <see cref="Select"/>.
        /// Can be null.
        /// </summary>
        public virtual Having Having { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="CSharpedSql.OrderBy"/> for this <see cref="Select"/>.
        /// Can be null.
        /// </summary>
        public virtual OrderBy OrderBy { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="CSharpedSql.GroupBy"/> for this <see cref="Select"/>.
        /// Can be null.
        /// </summary>
        public virtual GroupBy GroupBy { get; set; }

        protected Select(string table, SelectType selectType, params ColumnItem[] columns)
        {
            TableName = table;
            Columns = columns;
            SelectType = selectType;
        }

        /// <summary>
        /// Returns the <see cref="Select"/> statement as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="Select"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => sql.SqlBase.Select(this, sql);

        /// <summary>
        /// The WHERE clause is used to filter records.
        /// </summary>
        /// <param name="where">the WHERE Clause to add</param>
        public static Select operator +(Select statement, Where where)
        {
            if (statement.Where == null)
                statement.Where = where;
            else
                statement.Where.And(where);
            return statement;
        }
        /// <summary>
        /// Add a another condition to the WHERE Clause that need to be true.
        /// </summary>
        /// <param name="where">The WHERE Clause to add</param>
        public static Select operator &(Select statement, Where where)
        {
            if (statement.Where == null)
                statement.Where = where;
            else
                statement.Where.And(where);
            return statement;
        }
        /// <summary>
        /// Add a another condition to the WHERE Clause that can be true instead.
        /// </summary>
        /// <param name="where">The WHERE Clause to add</param>
        public static Select operator |(Select statement, Where where)
        {
            if (statement.Where == null)
                throw new System.Exception("Can't use '|' then the statements 'Where' is null, use '+' or '&' insted!");
            else
                statement.Where.Or(where);
            return statement;
        }

        public static Select operator +(Select statement, Having having)
        {
            if (statement.Having == null)
                statement.Having = having;
            else
                statement.Having.And(having);
            return statement;
        }
        public static Select operator &(Select statement, Having having)
        {
            if (statement.Having == null)
                statement.Having = having;
            else
                statement.Having.And(having);
            return statement;
        }
        public static Select operator |(Select statement, Having having)
        {
            if (statement.Having == null)
                throw new System.Exception("Can't use '|' then the statements 'Where' is null, use '+' or '&' insted!");
            else
                statement.Having.Or(having);
            return statement;
        }

        public static Select operator +(Select select, GroupBy groupBy)
        {
            select.GroupBy = groupBy;
            return select;
        }
        /// <summary>
        /// The ORDER BY keyword is used to sort the result-set in ascending or descending order.
        /// </summary>
        /// <param name="orderBy">the ORDER BY keyword to add</param>
        public static Select operator +(Select select, OrderBy orderBy)
        {
            select.OrderBy = orderBy;
            return select;
        }
        /// <summary>
        /// A JOIN clause is used to combine rows from two or more tables, based on a related column between them.
        /// <para>Remember to add the table names on columns in the SELECT statement.
        /// It is not needed in the JOIN clause.</para>
        /// </summary>
        /// <param name="join">the JOIN clause to add</param>
        public static Select operator +(Select select, Join join)
        {
            if (select.Join == null)
                select.Join = join;
            else
                select.Join.Joins.Add(join);
            return select;
        }

        /// <summary>
        /// The SELECT statement is used to select data from a database.
        /// </summary>
        /// <param name="table">the table to select from</param>
        /// <param name="columns">the columns to return</param>
        public static Select From(string table, params ColumnItem[] columns)
        {
            return new Select(table, SelectType.Normal, columns);
        }
        /// <summary>
        /// The SELECT DISTINCT statement is used to return only distinct (different) values.
        /// </summary>
        /// <param name="table">the table to select from</param>
        /// <param name="columns">the columns to return</param>
        public static Select DistinctFrom(string table, params ColumnItem[] columns)
        {
            return new Select(table, SelectType.Distinct, columns);
        }
        /// <summary>
        /// The SELECT TOP clause is used to specify the number of records to return.
        /// <para>In MySql this is the same as LIMIT.</para>
        /// </summary>
        /// <param name="top">the max number to return</param>
        /// <param name="table">the table to select from</param>
        /// <param name="columns">the columns to return</param>
        public static Select TopFrom(int top, string table, params ColumnItem[] columns)
        {
            return new Select(table, SelectType.Top, columns)
            {
                Top = top
            };
        }
        /// <summary>
        /// The SELECT TOP clause is used to specify the number of records to return.
        /// <para>In MySql this is the same as LIMIT.</para>
        /// </summary>
        /// <param name="top">the max number to return</param>
        /// <param name="offset">Specify which row to start retrieving data from</param>
        /// <param name="table">the table to select from</param>
        /// <param name="columns">the columns to return</param>
        public static Select TopFrom(int top, int offset, string table, params ColumnItem[] columns)
        {
            return new Select(table, SelectType.Top, columns)
            {
                Top = top,
                Offset = offset
            };
        }
    }
}
