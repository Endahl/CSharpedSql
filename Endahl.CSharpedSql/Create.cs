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
        protected List<Column> Columns { get; }

        protected IList<Column> Primarys { get; }
        protected IList<Column> Foreigns { get; }
        protected IList<Column> Uniques { get; }

        protected Create(CreateType type, string name)
        {
            CreateType = type;
            TableName = name;
            Primarys = new List<Column>();
            Foreigns = new List<Column>();
            Uniques = new List<Column>();
            Columns = new List<Column>();
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

        public IEnumerable<Column> GetColumns()
        {
            return Columns;

        }

        public Create AddColumn(Column column)
        {
            Columns.Add(column);
            return this;
        }

        /// <summary>
        /// Returns the <see cref="Create"/> statement as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        /// <summary>
        /// Returns the <see cref="Create"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            switch (CreateType)
            {
                case CreateType.Table:
                    return "CREATE " + CreateTable(sql);
                case CreateType.TableIfNotExists:
                    return sql.SqlLanguage == SqlLanguage.SqlServer ?
                        $"IF(object_id(N'{sql.IdentifieName(TableName)}', N'U') IS NULL BEGIN CREATE TABLE {CreateTable(sql)}; END"
                        : $"CREATE TABLE IF NOT EXISTS {CreateTable(sql)}";
                case CreateType.Copy:
                    return $"CREATE TABLE {sql.IdentifieName(TableName)} AS {Select.ToString(sql)}";
                default:
                    return "";
            }
        }

        private string CreateTable(SqlOptions sql)
        {
            var statement = $"{sql.IdentifieName(TableName)} (";
            if (Columns.Count > 0)
            {
                statement += $"{Columns[0].ToString(sql)}";
                AddConstraint(Columns[0]);

                for (var i = 1; i < Columns.Count; i++)
                {
                    statement += $", {Columns[i].ToString(sql)}";
                    AddConstraint(Columns[i]);
                }

                if (Primarys.Count > 0)
                {
                    statement += $", CONSTRAINT {sql.IdentifierLeft}pk_{TableName}{sql.IdentifierRight} PRIMARY KEY ({sql.IdentifierLeft}{Primarys[0].Name}{sql.IdentifierRight}";
                    for (var i = 1; i < Primarys.Count; i++)
                        statement += $",{sql.IdentifierLeft}{Primarys[i].Name}{sql.IdentifierRight}";
                    statement += ')';
                }
                foreach (var f in Foreigns)
                {
                    statement += $", CONSTRAINT {sql.IdentifierLeft}fk_{TableName}_{f.Name}{sql.IdentifierRight} FOREIGN KEY " +
                        $"({sql.IdentifierLeft}{f.Name}{sql.IdentifierRight}) REFERENCES " +
                        $"{sql.IdentifierLeft}{f.Constraint.Table}{sql.IdentifierRight}({sql.IdentifierLeft}{f.Constraint.Column}{sql.IdentifierRight})";
                }
                foreach (var u in Uniques)
                {
                    statement += $", CONSTRAINT {sql.IdentifierLeft}uc_{TableName}_{u.Name}{sql.IdentifierRight} UNIQUE ({sql.IdentifierLeft}{u.Name}{sql.IdentifierRight})";
                }
            }
            return statement + ')';
        }

        private void AddConstraint(Column item)
        {
            if (item.Constraint != null)
            {
                if (item.Constraint.ConstraintKey == Constraint.Key.PrimaryKey)
                    Primarys.Add(item);
                else if (item.Constraint.ConstraintKey == Constraint.Key.ForeignKey)
                    Foreigns.Add(item);
                else
                    Uniques.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">the name of the new table to create</param>
        /// <param name="column">the column to add to the new table</param>
        public static Create Table(string tableName, Column column, params Column[] columns)
        {
            return new Create(CreateType.Table, tableName, column, columns);
        }

        public static Create TableIfNotExists(string tableName, Column column, params Column[] columns)
        {
            return new Create(CreateType.TableIfNotExists, tableName, column, columns);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTableName"></param>
        /// <param name="fromTable"></param>
        public static Create CopyFrom(string newTableName, Select fromTable)
        {
            return new Create(CreateType.Copy, newTableName, fromTable);
        }
    }

    public enum CreateType
    {
        Table,
        TableIfNotExists,
        Copy
    }
}
