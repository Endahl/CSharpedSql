namespace Endahl.CSharpedSql.SqlServer
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// The base class for Sql Server / MS Sql
    /// </summary>
    public class SqlServer : Sql
    {
        /// <summary>
        /// Get the options for this <see cref="SqlServer"/> instance.
        /// </summary>
        public override SqlOptions SqlOptions { get; }

        public SqlServer()
        {
            SqlOptions = new SqlOptions('[', ']', SqlLanguage.SqlServer);
        }
    }
}
