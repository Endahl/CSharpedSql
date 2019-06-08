namespace Endahl.CSharpedSql
{
    public class Constraint
    {
        public enum Key
        {
            PrimaryKey,
            ForeignKey,
            Unique
        }
        public virtual Key ConstraintKey { get; }

        public virtual string Table { get; }
        public virtual string Column { get; }

        protected Constraint(Key key)
        {
            ConstraintKey = key;
        }

        protected Constraint(Key key, string table, string column) : this(key)
        {
            Table = table;
            Column = column;
        }

        public static Constraint PrimaryKey()
        {
            return new Constraint(Key.PrimaryKey);
        }

        public static Constraint ForeignKey(string table, string column)
        {
            return new Constraint(Key.ForeignKey, table, column);
        }

        public static Constraint Unique()
        {
            return new Constraint(Key.Unique);
        }
    }
}
