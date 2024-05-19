namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Create: ISqlStatement
    {
        public virtual CreateType CreateType { get; }
        public virtual string TableName { get; }

        /// <summary>
        /// The select used for CopyFrom(...)
        /// </summary>
        public Select Select { get; }
        public List<Column> Columns { get; }

        public IList<Column> Primarys { get; }
        public IList<Column> Foreigns { get; }
        public IList<Column> Uniques { get; }

        protected Create(CreateType type, string name)
        {
            CreateType = type;
            TableName = name;
            Primarys = new List<Column>();
            Foreigns = new List<Column>();
            Uniques = new List<Column>();
            Columns = [];
        }

        protected Create(CreateType type, string name, Column column, IEnumerable<Column> columns) : this(type, name)
        {
            Columns.Add(column);
            Columns.AddRange(columns);
        }

        protected Create(CreateType type, string name, Select select)
        {
            CreateType = type;
            TableName = name;
            Select = select;
        }

        public IEnumerable<Column> GetColumns() => Columns;

        public Create AddColumn(Column column)
        {
            Columns.Add(column);
            return this;
        }

        /// <summary>
        /// Returns the <see cref="Create"/> statement as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="Create"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => sql.SqlBase.Create(this, sql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">the name of the new table to create</param>
        /// <param name="column">the column to add to the new table</param>
        public static Create Table(string tableName, Column column, params Column[] columns)
        {
            return new(CreateType.Table, tableName, column, columns);
        }

        public static Create TableIfNotExists(string tableName, Column column, params Column[] columns)
        {
            return new(CreateType.TableIfNotExists, tableName, column, columns);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTableName"></param>
        /// <param name="fromTable"></param>
        public static Create CopyFrom(string newTableName, Select fromTable)
        {
            return new(CreateType.Copy, newTableName, fromTable);
        }
    }

    public enum CreateType
    {
        Table,
        TableIfNotExists,
        Copy
    }
}
