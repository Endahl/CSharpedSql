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
        public virtual int DataTypeSizeToAlter { get; }
        public virtual int DataTypeDigitsToAlter { get; }

        public enum WhatToAlter
        {
            PrimaryKey,
            ForeignKey,
            Unique,
            Column,
        }

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
        protected Alter(AlterType alter, string table, WhatToAlter whatToAlter, string column, CSharpType dataType, int size, int digits)
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
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        /// <summary>
        /// Returns the <see cref="Alter"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            var statement = $"ALTER TABLE {sql.IdentifieName(Table)} ";
            switch (WhatToModify)
            {
                case WhatToAlter.PrimaryKey:
                    var columnPK = sql.IdentifieName(Column);
                    foreach (var c in ExtraPrimaryKeyColumns)
                        columnPK += $",{sql.IdentifieName(c)}";
                    statement += AlterType == AlterType.Add
                        ? $"ADD CONSTRAINT {sql.SafeIdentifieName(ConstraintName)} PRIMARY KEY ({columnPK})"
                        : (sql.SqlLanguage == SqlLanguage.SqlServer ? $"DROP CONSTRAINT {sql.SafeIdentifieName(ConstraintName)}": $"DROP PRIMARY KEY");
                    break;
                case WhatToAlter.ForeignKey:
                    statement += AlterType == AlterType.Add
                        ? $"ADD CONSTRAINT {sql.SafeIdentifieName(ConstraintName)} FOREIGN KEY ({Column}) REFERENCES " +
                            $"{sql.IdentifieName(ReferencesTable)}({sql.IdentifieName(ReferencesColumn)})"
                        : (sql.SqlLanguage == SqlLanguage.SqlServer ? $"DROP CONSTRAINT {sql.SafeIdentifieName(ConstraintName)}"
                            : $"DROP FOREIGN KEY {sql.SafeIdentifieName(ConstraintName)}");
                    break;
                case WhatToAlter.Unique:
                    statement += AlterType == AlterType.Add
                        ? $"ADD CONSTRAINT {sql.SafeIdentifieName(ConstraintName)} UNIQUE ({Column})"
                        : (sql.SqlLanguage == SqlLanguage.SqlServer ? $"DROP CONSTRAINT {sql.SafeIdentifieName(ConstraintName)}" : $"DROP INDEX KEY");
                    break;
                case WhatToAlter.Column:
                    switch (AlterType)
                    {
                        case AlterType.Add:
                            statement += $"ADD {NewColumnToAdd.ToString(sql)}";
                            break;
                        case AlterType.Drop:
                            statement += $"DROP COLUMN {sql.IdentifieName(Column)}";
                            break;
                        case AlterType.Alter:
                            statement += sql.SqlLanguage == SqlLanguage.SqlServer ? $"ALTER" : $"MODIFY";
                            statement += $"COLUMN {sql.IdentifieName(Column)} " +
                                $"{sql.CSharpTypeToSqlDataType(DataTypeToAlter, DataTypeSizeToAlter, DataTypeDigitsToAlter)}";
                            break;
                    }
                    break;
            }

            return statement;
        }


        public static Alter AddColumn(string table, NewColumn column)
        {
            return new Alter(AlterType.Add, table, WhatToAlter.Column, column);
        }

        public static Alter DropColumn(string table, string column)
        {
            return new Alter(AlterType.Drop, "", table, WhatToAlter.Column, column);
        }

        public static Alter AlterColumn(string table, string column, CSharpType dataType, int size = 255, int digits = 0)
        {
            return new Alter(AlterType.Alter, table, WhatToAlter.Column, column, dataType, size, digits);
        }

        public static Alter AddPrimaryKey(string table, string column, params string[] columns)
        {
            return new Alter(AlterType.Add, $"pk_{table}", table, WhatToAlter.PrimaryKey, column, columns);
        }

        public static Alter AddForeignKey(string table, string column, string referencesTable, string referencesColumn)
        {
            return new Alter(AlterType.Add, $"fk_{table}_{column}", table, WhatToAlter.ForeignKey, column, referencesTable, referencesColumn);
        }

        public static Alter AddUnique(string table, string column)
        {
            return new Alter(AlterType.Add, $"uc_{table}_{column}", table, WhatToAlter.Unique, column);
        }

        public static Alter DropPrimaryKey(string table)
        {
            return new Alter(AlterType.Drop, $"pk_{table}", table, WhatToAlter.PrimaryKey, "");
        }

        public static Alter DropForeignKey(string table, string column)
        {
            return new Alter(AlterType.Drop, $"fk_{table}_{column}", table, WhatToAlter.ForeignKey, column);
        }

        public static Alter DropUnique(string table, string column)
        {
            return new Alter(AlterType.Drop, $"uc_{table}_{column}", table, WhatToAlter.Unique, column);
        }
    }
}
