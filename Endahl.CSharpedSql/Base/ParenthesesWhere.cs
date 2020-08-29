namespace Endahl.CSharpedSql.Base
{
    public class ParenthesesWhere : Where
    {
        public Where Where { get; }

        public ParenthesesWhere(Where where) : base(null)
        {
            Where = where;
        }
    }
}
