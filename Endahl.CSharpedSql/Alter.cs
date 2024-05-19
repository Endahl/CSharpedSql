namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// A ALTER statement for SQL.
    /// </summary>
    public class Alter: ISqlStatement
    {
        public virtual string ConstraintName { get; }
        public virtual string Table { get; }
        public virtual AlterType AlterType { get; }
        public virtual string Column { get; }
        public virtual string ReferencesTable { get; }
        public virtual string ReferencesColumn { get; }
        public virtual string[] ExtraPrimaryKeyColumns { get; }
        public virtual WhatToAlter WhatToModify { get; }
        public virtual NewColumn NewColumnToAdd { get; }
        public virtual CSharpType DataTypeToAlter { get; }
        public virtual uint DataTypeSizeToAlter { get; }
        public virtual int DataTypeDigitsToAlter { get; }

        protected Alter(AlterType alter, string constraintName, string table, WhatToAlter whatToAlter, string column)
        {
            Column = column;
            AlterType = alter;
            ConstraintName = constraintName;
            Table = table;
            WhatToModify = whatToAlter;
        }
        protected Alter(AlterType alter, string constraintName, string table, WhatToAlter whatToAlter, string column, string[] primaryKeyColumns):
            this(alter, constraintName, table, whatToAlter, column)
        {
            ExtraPrimaryKeyColumns = primaryKeyColumns;
        }
        protected Alter(AlterType alter, string name, string table, WhatToAlter whatToAlter, string column, string referencesTable, string referencesColumn):
            this(alter, name, table, whatToAlter, column)
        {
            ReferencesColumn = referencesColumn;
            ReferencesTable = referencesTable;
        }
        protected Alter(AlterType alter, string table, WhatToAlter whatToAlter, NewColumn newColumnToAdd)
        {
            NewColumnToAdd = newColumnToAdd;
            AlterType = alter;
            Table = table;
            WhatToModify = whatToAlter;
        }
        protected Alter(AlterType alter, string table, WhatToAlter whatToAlter, string column, CSharpType dataType, uint size, int digits)
        {
            DataTypeToAlter = dataType;
            Column = column;
            AlterType = alter;
            Table = table;
            WhatToModify = whatToAlter;
            DataTypeDigitsToAlter = digits;
            DataTypeSizeToAlter = size;
        }

        /// <summary>
        /// Returns the <see cref="Alter"/> statement as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="Alter"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => sql.SqlBase.Alter(this, sql);

        public static Alter AddColumn(string table, NewColumn column)
        {
            return new(AlterType.Add, table, WhatToAlter.Column, column);
        }

        public static Alter DropColumn(string table, string column)
        {
            return new(AlterType.Drop, "", table, WhatToAlter.Column, column);
        }

        public static Alter AlterColumn(string table, string column, CSharpType dataType, uint size = 0, int digits = 0)
        {
            return new(AlterType.Alter, table, WhatToAlter.Column, column, dataType, size, digits);
        }

        public static Alter AddPrimaryKey(string table, string column, params string[] columns)
        {
            return new(AlterType.Add, $"pk_{table}", table, WhatToAlter.PrimaryKey, column, columns);
        }

        public static Alter AddForeignKey(string table, string column, string referencesTable, string referencesColumn)
        {
            return new(AlterType.Add, $"fk_{table}_{column}", table, WhatToAlter.ForeignKey, column, referencesTable, referencesColumn);
        }

        public static Alter AddUnique(string table, string column)
        {
            return new(AlterType.Add, $"uc_{table}_{column}", table, WhatToAlter.Unique, column);
        }

        public static Alter DropPrimaryKey(string table)
        {
            return new(AlterType.Drop, $"pk_{table}", table, WhatToAlter.PrimaryKey, "");
        }

        public static Alter DropForeignKey(string table, string column)
        {
            return new(AlterType.Drop, $"fk_{table}_{column}", table, WhatToAlter.ForeignKey, column);
        }

        public static Alter DropUnique(string table, string column)
        {
            return new(AlterType.Drop, $"uc_{table}_{column}", table, WhatToAlter.Unique, column);
        }
    }
}
