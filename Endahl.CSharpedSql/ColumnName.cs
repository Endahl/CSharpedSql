namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    public class ColumnName
    {
        public string Name { get; }

        public ColumnName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        public virtual string ToString(SqlOptions sql)
        {
            return sql.IdentifieName(Name);
        }
    }
}
