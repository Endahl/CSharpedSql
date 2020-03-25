namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System.Collections.Generic;

    public class QueryString : ISqlStatement
    {
        protected virtual IDictionary<SqlLanguage, string> Querys { get; }

        /// <summary>
        /// Gets or Sets the MySql command
        /// </summary>
        public virtual string MySql
        {
            get => Querys.TryGetValue(SqlLanguage.MySql, out var statement) ? statement : null;
            set
            {
                SqlString(SqlLanguage.MySql, value);
            }
        }
        /// <summary>
        /// Gets or Sets the Sql Server command
        /// </summary>
        public virtual string SqlServer
        {
            get => Querys.TryGetValue(SqlLanguage.SqlServer, out var statement) ? statement : null;
            set
            {
                SqlString(SqlLanguage.SqlServer, value);
            }
        }

        public QueryString()
        {
            Querys = new Dictionary<SqlLanguage, string>();
        }

        /// <summary>
        /// Returns the <see cref="QueryString"/> statement as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        /// <summary>
        /// Returns the <see cref="QueryString"/> statement as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql)
        {
            if (Querys.ContainsKey(sql.SqlLanguage))
                return Querys[sql.SqlLanguage];
            return "";
        }

        protected virtual void SqlString(SqlLanguage language, string statement)
        {
            Querys.Add(language, statement);
        }
    }
}
