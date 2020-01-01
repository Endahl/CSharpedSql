namespace Endahl.CSharpedSql.Base
{
    public interface ISqlStatement
    {
        /// <summary>
        /// Returns the statement as a string.
        /// </summary>
        string ToString(SqlOptions sql);
    }
}
