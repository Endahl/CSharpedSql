namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System.Collections.Generic;

    public class QueryString : ISqlStatement
    {
        public virtual IDictionary<string, IList<string>> Querys { get; }

        public QueryString()
        {
            Querys = new Dictionary<string, IList<string>>();
        }

        /// <summary>
        /// Returns the <see cref="QueryString"/> statement as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="QueryString"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => sql.SqlBase.QueryString(this, sql);
    }
}
