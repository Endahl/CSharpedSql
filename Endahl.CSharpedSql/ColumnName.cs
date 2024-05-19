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

        public override string ToString() => ToString(new SqlOptions());
        public virtual string ToString(SqlOptions sql) => sql.IdentifieName(Name);
    }
}
