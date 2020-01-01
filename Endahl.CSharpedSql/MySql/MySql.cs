namespace Endahl.CSharpedSql.MySql
{
    using Endahl.CSharpedSql.Base;

    /// <summary>
    /// The base class for MySql
    /// </summary>
    public class MySql : Sql
    {
        /// <summary>
        /// Get the options for this <see cref="MySql"/> instance.
        /// </summary>
        public override SqlOptions SqlOptions { get; }

        public MySql()
        {
            SqlOptions = new SqlOptions('`', '`', SqlLanguage.MySql);
        }
    }
}
