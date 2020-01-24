namespace Endahl.CSharpedSql.Base
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Options and functions used when generating sql commands.
    /// </summary>
    public class SqlOptions
    {
        /// <summary>
        /// Contains all values from all queries that are added to the <see cref="Sql"/> instance that this <see cref="SqlOptions"/> is part of.
        /// <para>If this <see cref="SqlOptions"/>.UseParameters is false, when this list is null.</para>
        /// </summary>
        protected IList<SqlItem> SqlItems { get; }
        /// <summary>
        /// A count of how many value that has been added by the CreateItemID methods.
        /// <para>If this <see cref="SqlOptions"/>.UseParameters is false, when the count will be 0.</para>
        /// </summary>
        protected int ItemIdCount { get; set; }

        /// <summary>
        /// Get the left char used to enclose a table or column name with.
        /// </summary>
        public char IdentifierLeft { get; }
        /// <summary>
        /// Get the right char used to enclose a table or column name with.
        /// </summary>
        public char IdentifierRight { get; }
        /// <summary>
        /// Get the type of Sql to be generated.
        /// </summary>
        public SqlLanguage SqlLanguage { get; }
        /// <summary>
        /// Get an indication of whether all values will be parameterized.
        /// </summary>
        public bool UseParameters { get; }

        public SqlOptions() : this('[', ']', false) { }
        /// <param name="identifierLeft">The left char to enclose a table or column name with.</param>
        /// <param name="identifierRight">The right char to enclose a table or column name with.</param>
        /// <param name="useParameters">Should all value be parameterized.</param>
        public SqlOptions(char identifierLeft, char identifierRight, bool useParameters = true)
            : this(identifierLeft, identifierRight, SqlLanguage.SqlServer, useParameters) { }
        /// <param name="identifierLeft">The left char to enclose a table or column name with.</param>
        /// <param name="identifierRight">The right char to enclose a table or column name with.</param>
        /// <param name="sqlLanguage">The type of Sql to be generated.</param>
        /// <param name="useParameters">Should all value be parameterized.</param>
        public SqlOptions(char identifierLeft, char identifierRight, SqlLanguage sqlLanguage, bool useParameters = true)
        {
            ItemIdCount = 0;
            IdentifierLeft = identifierLeft;
            IdentifierRight = identifierRight;
            SqlLanguage = sqlLanguage;
            UseParameters = useParameters;
            if (useParameters)
                SqlItems = new List<SqlItem>();
        }

        /// <summary>
        /// Get all values from all queries that are added to the <see cref="Sql"/> instance that this <see cref="SqlOptions"/> is part of.
        /// <para>If this <see cref="SqlOptions"/>.UseParameters is false, when this will returns null.</para>
        /// </summary>
        public virtual IEnumerable<SqlItem> GetSqlItems => SqlItems;
        /// <summary>
        /// Remove all items that has been added to this <see cref="SqlOptions"/>.
        /// </summary>
        public virtual void ClearItems()
        {
            SqlItems.Clear();
            ItemIdCount = 0;
        }
        /// <summary>
        /// Returns a enclosed table or column name.
        /// </summary>
        /// <param name="value">The table or column name to enclose.</param>
        public virtual string IdentifieName(string value)
        {
            return $"{IdentifierLeft}{value.Replace(".", $"{IdentifierRight}.{IdentifierLeft}")}{IdentifierRight}";
        }
        /// <summary>
        /// Returns a safe enclosed table or column name.
        /// </summary>
        /// <param name="value">The table or column name to enclose.</param>
        public virtual string SafeIdentifieName(string value)
        {
            return $"{IdentifierLeft}{value.Replace('.', '_').Replace('*', '_')}{IdentifierRight}";
        }
        /// <summary>
        /// Returns the sql type that is equal to the <see cref="CSharpType"/>, used then altering or creating columns.
        /// </summary>
        /// <param name="type">The C# type.</param>
        /// <param name="size">The size the sql type should have. Will be ignored if the <see cref="CSharpType"/> don't support it.</param>
        /// <param name="digits">Only used when the <see cref="CSharpType"/> is Decimal</param>
        public virtual string CSharpTypeToSqlDataType(CSharpType type, int size = 255, int digits = 0)
        {
            string data = "";
            switch (type)
            {
                case CSharpType.Char:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "nchar(1)" : "CHAR(1)";
                    break;
                case CSharpType.String:
                    if (SqlLanguage == SqlLanguage.SqlServer)
                    {
                        data = "nvarchar";
                        data += size > 0 && size <= 4000 ? $"({size})" : "(max)";
                    }
                    else
                    {
                        if (size > 20000 || size <= 0)
                            data = "TEXT";
                        else
                            data = $"VARCHAR({size})";
                    }
                    break;
                case CSharpType.Bool:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "bit(1)" : "BIT(1)";
                    break;
                case CSharpType.Byte:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "tinyint" : "TINYINT UNSIGNED";
                    break;
                case CSharpType.Short:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "smallint" : "SMALLINT";
                    break;
                case CSharpType.Int:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "int" : "INT";
                    break;
                case CSharpType.Long:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "bigint" : "BIGINT";
                    break;
                case CSharpType.Decimal:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "decimal" : "DECIMAL";
                    data += size <= 36 ? $"({size}," : "(18,";
                    data += digits <= 36 ? $"{digits})" : "0)";
                    break;
                case CSharpType.Float:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "float" : "FLOAT";
                    data += size <= 53 ? $"({size})" : "(53)";
                    break;
                case CSharpType.ByteArray:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "varbinary" : "VARBINARY";
                    if (size > 0 || size <= 8000)
                        data += $"({size})";
                    else
                        data += SqlLanguage == SqlLanguage.SqlServer ? "(max)" : "(65535)";
                    break;
                case CSharpType.DateTime:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "datetime2" : "DATETIME";
                    break;
                case CSharpType.SByte:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "tinyint" : "TINYINT SIGNED";
                    break;
                case CSharpType.UShort:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "smallint" : "SMALLINT UNSIGNED";
                    break;
                case CSharpType.UInt:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "int" : "INT UNSIGNED";
                    break;
                case CSharpType.ULong:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "bigint" : "BIGINT UNSIGNED";
                    break;
                case CSharpType.Guid:
                    data = SqlLanguage == SqlLanguage.SqlServer ? "uniqueidentifier" : "CHAR(36)";
                    break;
            }
            return data;
        }
        /// <summary>
        /// Add a value that should be parameterized, and returns a id.
        /// If this <see cref="SqlOptions"/>.UseParameters is false, when the value will be returned as a string.
        /// </summary>
        /// <param name="value">The value to be added.</param>
        public virtual string CreateItemID(object value)
        {
            if (UseParameters)
            {
                var id = "@item" + ItemIdCount++;
                SqlItems.Add(new SqlItem(id, HandleObject(value)));
                return id;
            }

            var obj = HandleObject(value);
            return obj is string ? $"'{obj}'" : obj.ToString();
        }
        /// <summary>
        /// Returns a object that is ready for a sql query.
        /// </summary>
        /// <param name="obj">The object that should be made ready a sql query.</param>
        protected virtual object HandleObject(object obj)
        {
            if (obj is bool)
                obj = (bool)obj ? 1 : 0;
            else if (obj is Guid)
                obj = obj.ToString();
            else if (obj is DateTime)
                obj = ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss");
            else if (obj is DateTimeOffset)
                obj = ((DateTimeOffset)obj).ToString("yyyy-MM-dd HH:mm:ss");
            else if (SqlLanguage == SqlLanguage.SqlServer)
            {
                if (obj is sbyte)
                    obj = unchecked((byte)obj);
                else if (obj is ushort)
                    obj = unchecked((short)obj);
                else if (obj is uint)
                    obj = unchecked((int)obj);
                else if (obj is ulong)
                    obj = unchecked((long)obj);
            }
            return obj;
        }
    }

    /// <summary>
    /// The supported Sql languages
    /// </summary>
    public enum SqlLanguage
    {
        /// <summary>
        /// Microsoft Sql Server, also known as MS Sql
        /// </summary>
        SqlServer,
        /// <summary>
        /// Oracle's MySql
        /// </summary>
        MySql
    }
}
