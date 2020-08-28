namespace Endahl.CSharpedSql.Base
{
    public class ParenthesesWhere : Where
    {
        public Where where;

        public ParenthesesWhere(Where where) : base(null)
        {
            this.where = where;
        }
    }
}
