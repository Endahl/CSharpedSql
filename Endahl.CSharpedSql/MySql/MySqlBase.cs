namespace Endahl.CSharpedSql.MySql.Base
{
    using System;
    using Endahl.CSharpedSql.Base;

    public class MySqlBase : ISqlBase
    {
        public virtual string Alter(Alter alter, SqlOptions sql)
        {
            var statement = $"ALTER TABLE {sql.IdentifieName(alter.Table)} ";
            switch (alter.WhatToModify)
            {
                case WhatToAlter.PrimaryKey:
                    var columnPK = sql.IdentifieName(alter.Column);
                    foreach (var c in alter.ExtraPrimaryKeyColumns)
                        columnPK += $",{sql.IdentifieName(c)}";
                    statement += alter.AlterType == AlterType.Add
                        ? $"ADD CONSTRAINT {sql.SafeIdentifieName(alter.ConstraintName)} PRIMARY KEY ({columnPK})"
                        : "DROP PRIMARY KEY";
                    break;
                case WhatToAlter.ForeignKey:
                    statement += alter.AlterType == AlterType.Add
                        ? $"ADD CONSTRAINT {sql.SafeIdentifieName(alter.ConstraintName)} FOREIGN KEY ({alter.Column}) REFERENCES " +
                            $"{sql.IdentifieName(alter.ReferencesTable)}({sql.IdentifieName(alter.ReferencesColumn)})"
                        : $"DROP FOREIGN KEY {sql.SafeIdentifieName(alter.ConstraintName)}";
                    break;
                case WhatToAlter.Unique:
                    statement += alter.AlterType == AlterType.Add
                        ? $"ADD CONSTRAINT {sql.SafeIdentifieName(alter.ConstraintName)} UNIQUE ({alter.Column})"
                        : "DROP INDEX KEY";
                    break;
                case WhatToAlter.Column:
                    switch (alter.AlterType)
                    {
                        case AlterType.Add:
                            statement += $"ADD {alter.NewColumnToAdd.ToString(sql)}";
                            break;
                        case AlterType.Drop:
                            statement += $"DROP COLUMN {sql.IdentifieName(alter.Column)}";
                            break;
                        case AlterType.Alter:
                            statement += $"MODIFY COLUMN {sql.IdentifieName(alter.Column)} " +
                                $"{sql.CSharpTypeToSqlDataType(alter.DataTypeToAlter, alter.DataTypeSizeToAlter, alter.DataTypeDigitsToAlter)}";
                            break;
                    }
                    break;
            }

            return statement;
        }

        public virtual string Case(Case @case, SqlOptions sql)
        {
            var c = $"CASE WHEN {@case.Condition.ToString(sql)} THEN {sql.CreateItemID(@case.Result)} ";
            foreach (var when in @case.Whens)
                c += $"WHEN {when.Condition.ToString(sql)} THEN {sql.CreateItemID(when.Result)} ";
            if (@case.ElseResult != null)
                c += $"ELSE {sql.CreateItemID(@case.ElseResult)} ";
            return c + "END";
        }

        public virtual string Column(Column column, SqlOptions sql)
        {
            var line = $"{sql.IdentifieName(column.Name)} ";
            line += sql.CSharpTypeToSqlDataType(column.DataType, column.Size, column.Digits);
            line += column.NotNull ? " NOT NULL" : "";
            if (column.AutoIncrement)
                line += " AUTO_INCREMENT";
            else if (column.DefaultValue != null)
                line += $" DEFAULT {sql.CreateItemID(column.DefaultValue)}";

            return line;
        }

        public virtual string Condition(Condition condition, SqlOptions sql)
        {
            return condition.ConditionType switch
            {
                ConditionType.Between => $"{condition.Column.ToString(sql)} BETWEEN {sql.CreateItemID(condition.Item)} AND {sql.CreateItemID(condition.Item2)}",
                ConditionType.NotBetween => $"{condition.Column.ToString(sql)} NOT BETWEEN {sql.CreateItemID(condition.Item)} AND {sql.CreateItemID(condition.Item2)}",
                ConditionType.Equal => ConditionToString(condition, "=", sql),
                ConditionType.NotEqual => ConditionToString(condition, "<>", sql),
                ConditionType.GreaterThan => ConditionToString(condition, ">", sql),
                ConditionType.LessThan => ConditionToString(condition, "<", sql),
                ConditionType.Like => ConditionToString(condition, "LIKE", sql),
                ConditionType.IsNull => $"{condition.Column.ToString(sql)} IS NULL",
                ConditionType.IsNotNull => $"{condition.Column.ToString(sql)} IS NOT NULL",
                ConditionType.All => $"{condition.Column.ToString(sql)} = ALL ({(condition.Item as Select).ToString(sql)})",
                ConditionType.Any => $"{condition.Column.ToString(sql)} = ANY ({(condition.Item as Select).ToString(sql)})",
                ConditionType.Exists => $"EXISTS ({(condition.Item as Select).ToString(sql)})",
                ConditionType.NotExists => $"NOT EXISTS ({(condition.Item as Select).ToString(sql)})",
                ConditionType.In => $"{condition.Column.ToString(sql)} IN ({(condition.Item as Select).ToString(sql)})",
                ConditionType.NotIn => $"{condition.Column.ToString(sql)} NOT IN ({(condition.Item as Select).ToString(sql)})",
                _ => "",
            };
        }
        private string ConditionToString(Condition condition, string conditionString, SqlOptions sql)
        {
            var result = condition.Column != null ? condition.Column.ToString(sql) : sql.CreateItemID(condition.Item);
            return $"{result} {conditionString} {sql.CreateItemID(condition.Item2)}";
        }

        public virtual string Create(Create create, SqlOptions sql)
        {
            return create.CreateType switch
            {
                CreateType.Table => "CREATE " + CreateTable(create, sql),
                CreateType.TableIfNotExists => $"CREATE TABLE IF NOT EXISTS {CreateTable(create, sql)}",
                CreateType.Copy => $"CREATE TABLE {sql.IdentifieName(create.TableName)} AS {create.Select.ToString(sql)}",
                _ => "",
            };
        }
        private string CreateTable(Create create, SqlOptions sql)
        {
            var statement = $"{sql.IdentifieName(create.TableName)} (";
            if (create.Columns.Count > 0)
            {
                statement += $"{create.Columns[0].ToString(sql)}";
                AddConstraint(create, create.Columns[0]);

                for (var i = 1; i < create.Columns.Count; i++)
                {
                    statement += $", {create.Columns[i].ToString(sql)}";
                    AddConstraint(create, create.Columns[i]);
                }

                if (create.Primarys.Count > 0)
                {
                    statement += $", CONSTRAINT {sql.IdentifierLeft}pk_{create.TableName}{sql.IdentifierRight} PRIMARY KEY ({sql.IdentifierLeft}{create.Primarys[0].Name}{sql.IdentifierRight}";
                    for (var i = 1; i < create.Primarys.Count; i++)
                        statement += $",{sql.IdentifierLeft}{create.Primarys[i].Name}{sql.IdentifierRight}";
                    statement += ')';
                }
                foreach (var f in create.Foreigns)
                {
                    statement += $", CONSTRAINT {sql.IdentifierLeft}fk_{create.TableName}_{f.Name}{sql.IdentifierRight} FOREIGN KEY " +
                        $"({sql.IdentifierLeft}{f.Name}{sql.IdentifierRight}) REFERENCES " +
                        $"{sql.IdentifierLeft}{f.Constraint.Table}{sql.IdentifierRight}({sql.IdentifierLeft}{f.Constraint.Column}{sql.IdentifierRight})";
                }
                foreach (var u in create.Uniques)
                {
                    statement += $", CONSTRAINT {sql.IdentifierLeft}uc_{create.TableName}_{u.Name}{sql.IdentifierRight} UNIQUE ({sql.IdentifierLeft}{u.Name}{sql.IdentifierRight})";
                }
            }
            return statement + ')';
        }
        private void AddConstraint(Create create, Column item)
        {
            if (item.Constraint != null)
            {
                if (item.Constraint.ConstraintKey == ConstraintKey.PrimaryKey)
                    create.Primarys.Add(item);
                else if (item.Constraint.ConstraintKey == ConstraintKey.ForeignKey)
                    create.Foreigns.Add(item);
                else
                    create.Uniques.Add(item);
            }
        }

        public virtual string CSharpTypeToSqlDataType(CSharpType type, uint size = 0, int digits = 0)
        {
            string data = "";
            switch (type)
            {
                case CSharpType.Char:
                    data = "CHAR(1)";
                    break;
                case CSharpType.String:
                    if (size > 21844 && size <= 65535)
                        data = "TEXT";
                    else if (size > 65535 && size <= 16777215)
                        data = "MEDIUMTEXT";
                    else if (size == 0 || size > 16777215)
                        data = "LONGTEXT";
                    else
                        data = $"VARCHAR({size})";
                    break;
                case CSharpType.Bool:
                    data = "BIT(1)";
                    break;
                case CSharpType.Byte:
                    data = "TINYINT UNSIGNED";
                    break;
                case CSharpType.Short:
                    data = "SMALLINT";
                    break;
                case CSharpType.Int:
                    data = "INT";
                    break;
                case CSharpType.Long:
                    data = "BIGINT";
                    break;
                case CSharpType.Decimal:
                    data = "DECIMAL";
                    data += size <= 36 ? $"({size}," : "(18,";
                    data += digits <= 36 ? $"{digits})" : "0)";
                    break;
                case CSharpType.Float:
                    data = "FLOAT";
                    data += size <= 53 ? $"({size})" : "(53)";
                    break;
                case CSharpType.ByteArray:
                    data = "VARBINARY";
                    if (size > 0 || size <= 8000)
                        data += $"({size})";
                    else
                        data += "(65535)";
                    break;
                case CSharpType.DateTime:
                    data = "DATETIME";
                    break;
                case CSharpType.SByte:
                    data = "TINYINT SIGNED";
                    break;
                case CSharpType.UShort:
                    data = "SMALLINT UNSIGNED";
                    break;
                case CSharpType.UInt:
                    data = "INT UNSIGNED";
                    break;
                case CSharpType.ULong:
                    data = "BIGINT UNSIGNED";
                    break;
                case CSharpType.Guid:
                    data = "BINARY(16)";
                    break;
            }
            return data;
        }

        public virtual string Delete(Delete delete, SqlOptions sql)
        {
            var statement = "DELETE ";
            if (delete.Join != null)
                statement += $"{delete.TableName} ";

            statement += $"FROM {sql.IdentifieName(delete.TableName)}";

            if (delete.Join != null)
                statement += $" {delete.Join.ToString(sql)}";
            if (delete.Where != null)
                statement += $" {delete.Where.ToString(sql)}";
            return statement;
        }

        public virtual string Drop(Drop drop, SqlOptions sql)
        {
            return drop.DropIfTableExists ? $"DROP TABLE IF EXISTS {sql.IdentifieName(drop.TableName)}"
                : $"DROP TABLE {sql.IdentifieName(drop.TableName)}";
        }

        public virtual string Function(Function function, SqlOptions sql)
        {
            string result = "";
            switch (function.FunctionType)
            {
                case FunctionType.Average:
                    result = $"AVG({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Count:
                    result = $"Count({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Sum:
                    result = $"Sum({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Absolute:
                    result = "ABS";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.ArcCos:
                    result = "ACOS";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result = $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Ascii:
                    result = $"ASCII({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.ArcSin:
                    result = "ASIN";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.ArcTan:
                    result = "ATAN";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Ceiling:
                    result = "CEILING";
                    if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Cos:
                    result = "COS";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Cot:
                    result = "COT";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.DateDifference:
                    result = $"DATEDIFF({sql.IdentifieName(function.ColumnName)}, {sql.CreateItemID(function.String)})";
                    break;
                case FunctionType.DateDifferenceColumn:
                    result = $"DATEDIFF({sql.IdentifieName(function.ColumnName)}, {sql.IdentifieName(function.String)})";
                    break;
                case FunctionType.Degrees:
                    result = "DEGREES";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Exp:
                    result = "EXP";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Floor:
                    result = "FLOOR";
                    if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.GetDate:
                    result = "NOW()";
                    break;
                case FunctionType.GetDay:
                    result = $"DAY({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.GetMonth:
                    result = $"MONTH({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.GetYear:
                    result = $"YEAR({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.IndexOf:
                    result = $"LOCATE({sql.CreateItemID(function.String)}, {sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Insert:
                    result = $"INSERT({sql.IdentifieName(function.ColumnName)}, {sql.CreateItemID(function.Number)}, {sql.CreateItemID(function.Number2)}, {sql.CreateItemID(function.String)})";
                    break;
                case FunctionType.IsNull:
                    result = $"IFNULL({sql.IdentifieName(function.ColumnName)}, ";
                    if (function.Number != null)
                        result += $"{sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"{sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"{sql.IdentifieName(function.String)})";
                    break;
                case FunctionType.Left:
                    result = $"LEFT({sql.IdentifieName(function.ColumnName)}, {sql.CreateItemID(function.Number)})";
                    break;
                case FunctionType.LeftTrim:
                    result = $"LTRIM({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Length:
                    result = $"CHAR_LENGTH({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Locate:
                    result = $"LOCATE({sql.CreateItemID(function.String)}, {sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Log:
                    result = $"LOG";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Log10:
                    result = $"LOG10";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.NewGuid:
                    result = "UUID()";
                    break;
                case FunctionType.NullIf:
                    result = "NULLIF";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.CreateItemID(function.String)})";
                    result += $", {sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Max:
                    result = $"MAX({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Min:
                    result = $"MIN({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Pi:
                    result = "PI()";
                    break;
                case FunctionType.Power:
                    result = $"POWER({sql.IdentifieName(function.ColumnName)}, {sql.CreateItemID(function.Number)})";
                    break;
                case FunctionType.Radians:
                    result = $"RADIANS";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.CreateItemID(function.String)})";
                    result += $", {sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Random:
                    if (function.Number2 != null)
                        result = $"FLOOR(RAND()*({sql.CreateItemID(function.Number2)}-{sql.CreateItemID(function.Number)}+1)+{sql.CreateItemID(function.Number)})";
                    else if (function.Number != null)
                        result = $"RAND({sql.CreateItemID(function.Number)})";
                    else
                        result = "RAND()";
                    break;
                case FunctionType.Repeat:
                    result = $"REPEAT({sql.IdentifieName(function.ColumnName)}, {sql.CreateItemID(function.Number)})";
                    break;
                case FunctionType.Replace:
                    result = $"REPLACE({sql.IdentifieName(function.ColumnName)}, {sql.CreateItemID(function.String)}, {sql.CreateItemID(function.String2)})";
                    break;
                case FunctionType.Reverse:
                    result = $"REEVERSE({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Right:
                    result = $"RIGHT({sql.IdentifieName(function.ColumnName)}, {sql.CreateItemID(function.Number)})";
                    break;
                case FunctionType.RightTrim:
                    result = $"RTRIM({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Round:
                    result = "ROUND";
                    if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)}, {sql.CreateItemID(function.Number)})";
                    else
                        result += $"({sql.CreateItemID(function.String)}, {sql.CreateItemID(function.Number)})";
                    break;
                case FunctionType.Sign:
                    result = "SIGN";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.CreateItemID(function.String)})";
                    break;
                case FunctionType.Sin:
                    result = "SIN";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.CreateItemID(function.String)})";
                    break;
                case FunctionType.Space:
                    result = $"SPACE({sql.CreateItemID(function.Number)})";
                    break;
                case FunctionType.SquareRoot:
                    result = "SQRT";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.CreateItemID(function.String)})";
                    break;
                case FunctionType.SubString:
                    result = $"SUBSTRING({sql.IdentifieName(function.ColumnName)}, {sql.CreateItemID(function.Number)}, {sql.CreateItemID(function.Number2)})";
                    break;
                case FunctionType.Tan:
                    result = "TAN";
                    if (function.Number != null)
                        result += $"({sql.CreateItemID(function.Number)})";
                    else if (function.Decimal != null)
                        result += $"({sql.CreateItemID(function.Decimal)})";
                    else
                        result += $"({sql.CreateItemID(function.String)})";
                    break;
                case FunctionType.ToLower:
                    result = $"LOWER({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.ToUpper:
                    result = $"UPPER({sql.IdentifieName(function.ColumnName)})";
                    break;
                case FunctionType.Trim:
                    result = $"TRIM({sql.IdentifieName(function.ColumnName)})";
                    break;
            }
            return result;
        }

        public virtual string GroupBy(GroupBy groupBy, SqlOptions sql)
        {
            var result = $"GROUP BY {sql.IdentifieName(groupBy.ColumnNames[0])}";
            for (var i = 1; i < groupBy.ColumnNames.Length; i++)
                result += $", {sql.IdentifieName(groupBy.ColumnNames[i])}";
            return result;
        }

        public virtual object HandleObject(object obj)
        {
            if (obj is bool boolean)
                obj = boolean ? 1 : 0;
            else if (obj is Guid guid)
                obj = guid.ToByteArray();
            else if (obj is DateTime dateTime)
                obj = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            else if (obj is DateTimeOffset dateTimeOffset)
                obj = dateTimeOffset.ToString("yyyy-MM-dd HH:mm:ss");
            return obj;
        }

        public virtual string Having(Having having, SqlOptions sql)
        {
            return "HAVING " + ToStringWihtoutWhere(having, sql);
        }
        private string ToStringWihtoutWhere(Having having, SqlOptions sql)
        {
            var ha = having.Condition.ToString(sql);

            foreach (var h in having.Havings)
            {
                if (h.IsAnd)
                    ha += " AND " + ToStringWihtoutWhere(h, sql);
                else
                    ha += " OR " + ToStringWihtoutWhere(h, sql);
            }
            return ha;
        }

        public virtual string Insert(Insert insert, SqlOptions sql)
        {
            string s, v;
            s = v = "";
            if (insert.ColumnValues.Length > 0)
            {
                s = sql.IdentifieName(insert.ColumnValues[0].ColumnName);
                v = insert.ColumnValues[0].Value is InsertFunction ? (insert.ColumnValues[0].Value as InsertFunction).ToString(sql)
                    : sql.CreateItemID(insert.ColumnValues[0].Value);
                for (var i = 1; i < insert.ColumnValues.Length; i++)
                {
                    s += $", {sql.IdentifieName(insert.ColumnValues[i].ColumnName)}";
                    v += ", " + (insert.ColumnValues[i].Value is InsertFunction ? (insert.ColumnValues[i].Value as InsertFunction).ToString(sql)
                                : sql.CreateItemID(insert.ColumnValues[i].Value));
                }
            }
            return $"INSERT INTO {sql.IdentifieName(insert.TableName)} ({s}) VALUES ({v})";
        }

        public virtual string Join(Join join, SqlOptions sql)
        {
            string s;
            if (join.JoinType == JoinType.FullOuter)
                s = "FULL OUTER";
            else
                s = join.JoinType.ToString().ToUpper();

            s += $" JOIN {sql.IdentifieName(join.TableName2)} ON " +
                $"{sql.IdentifieName(join.TableName)}.{sql.IdentifieName(join.Column)} = " +
                $"{sql.IdentifieName(join.TableName2)}.{sql.IdentifieName(join.Column2)}";

            foreach (var j in join.Joins)
                s += " " + j.ToString(sql);

            return s;
        }

        public virtual string OrderBy(OrderBy orderBy, SqlOptions sql)
        {
            var s = "ORDER BY ";
            if (orderBy.Columns[0] is Case)
                s += $"({orderBy.Columns[0].ToString(sql)})";
            else
                s += orderBy.Columns[0].ToString(sql);
            for (var i = 1; i < orderBy.Columns.Length; i++)
            {
                if (orderBy.Columns[i] is Case)
                    s += $", ({orderBy.Columns[i].ToString(sql)})";
                else
                    s += $", {orderBy.Columns[i].ToString(sql)}";
            }
            return s + $" {orderBy.OrderByType}";
        }

        public virtual string QueryString(QueryString queryString, SqlOptions sql)
        {
            var s = "";
            if (queryString.Querys.TryGetValue(QueryStringExtensions.Key, out var querys))
            {
                foreach (var query in querys)
                    s += query + ";";
                s.Trim(';');
            }
            return s;
        }

        public virtual string Select(Select select, SqlOptions sql)
        {
            var limit = "";
            var ex = "";
            if (select.SelectType == SelectType.Distinct)
                ex = "DISTINCT ";
            else if (select.SelectType == SelectType.Top)
            {
                limit = " LIMIT " + select.Top;
                if (select.Offset > 0)
                    limit += ", " + select.Offset;
            }

            var selectString = $"SELECT {ex}";
            if (select.Columns.Length == 0)
                selectString += "*";
            else
            {
                selectString += $"{select.Columns[0].ToString(sql)}";
                if (select.Columns[0] is Case || select.Columns[0] is Function || select.Columns[0] is Value)
                    selectString += $" AS {sql.IdentifieName(select.Columns[0].Name)}";
                for (var i = 1; i < select.Columns.Length; i++)
                {
                    selectString += $", {select.Columns[i].ToString(sql)}";
                    if (select.Columns[i] is Case || select.Columns[i] is Function || select.Columns[i] is Value)
                        selectString += $" AS {sql.IdentifieName(select.Columns[i].Name)}";
                }
            }
            selectString += $" FROM {sql.IdentifieName(select.TableName)}";
            if (select.Join != null)
                selectString += " " + select.Join.ToString(sql);
            if (select.Where != null)
                selectString += " " + select.Where.ToString(sql);
            if (select.GroupBy != null)
                selectString += " " + select.GroupBy.ToString(sql);
            if (select.Having != null)
                selectString += " " + select.Having.ToString(sql);
            if (select.OrderBy != null)
                selectString += " " + select.OrderBy.ToString(sql);
            return selectString + limit;
        }

        public virtual string Update(Update update, SqlOptions sql)
        {
            var statement = $"UPDATE {sql.IdentifieName(update.TableName)} SET {sql.IdentifieName(update.ColumnValues[0].ColumnName)} " +
                $"= {sql.CreateItemID(update.ColumnValues[0].Value)}";
            foreach (var item in update.ColumnValues)
                statement += $", {sql.IdentifieName(item.ColumnName)} = {sql.CreateItemID(item.Value)}";
            if (update.Where != null)
                statement += " " + update.Where.ToString(sql);
            return statement;
        }

        public virtual string Where(Where where, SqlOptions sql)
        {
            return "WHERE " + ToStringWihtoutWhere(where, sql);
        }
        private string ToStringWihtoutWhere(Where where, SqlOptions sql)
        {
            string w;
            if (where is ParenthesesWhere pw)
                w = $"({ToStringWihtoutWhere(pw.Where, sql)})";
            else
                w = where.Condition.ToString(sql);

            foreach (var wh in where.Wheres)
            {
                if (wh.IsAnd)
                    w += " AND " + ToStringWihtoutWhere(wh, sql);
                else
                    w += " OR " + ToStringWihtoutWhere(wh, sql);
            }
            return w;
        }
    }
}
