namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    public class Constraint
    {
        public virtual ConstraintKey ConstraintKey { get; }

        public virtual string Table { get; }
        public virtual string Column { get; }

        protected Constraint(ConstraintKey key)
        {
            ConstraintKey = key;
        }

        protected Constraint(ConstraintKey key, string table, string column) : this(key)
        {
            Table = table;
            Column = column;
        }

        public static Constraint PrimaryKey()
        {
            return new(ConstraintKey.PrimaryKey);
        }

        public static Constraint ForeignKey(string table, string column)
        {
            return new(ConstraintKey.ForeignKey, table, column);
        }

        public static Constraint Unique()
        {
            return new(ConstraintKey.Unique);
        }
    }
}
