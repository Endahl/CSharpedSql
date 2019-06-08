namespace Endahl.CSharpedSql
{
    /// <summary>
    /// The Base class for all Connect types.
    /// </summary>
    public abstract class SqlConnect: Sql
    {
        /// <summary>
        /// Gets a indication of whether the connection to a database is open.
        /// </summary>
        public abstract bool IsConnectionOpen { get; }
        /// <summary>
        /// Gets the string used to connect to a database.
        /// </summary>
        public abstract string ConnectionString { get; }

        /// <summary>
        /// Sets the string used to connect to a database.
        /// </summary>
        public abstract void SetConnection(string server, string database, string user, string password);

        /// <summary>
        /// Executes a parameterized SQL statement and returns the number of rows affected.
        /// </summary>
        /// <returns>Number of rows affected</returns>
        public abstract int Execute();
        /// <summary>
        /// Executes a parameterized SQL statement, and returns the first column of the first row in the result
        /// set returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty</returns>
        public abstract object ExecuteScalar();
        /// <summary>
        /// Executes a parameterized SQL statement and builds a <see cref="ISqlDataReader"/>
        /// </summary>
        /// <returns>A <see cref="ISqlDataReader"/> object</returns>
        public abstract ISqlDataReader ExecuteReader();

        /// <summary>
        ///  Opens a database connection with the property settings specified by the ConnectionString.
        /// </summary>
        public abstract void OpenConnection();
        /// <summary>
        /// Closes the connection to the database. This is the preferred method of closing
        /// any open connection.
        /// </summary>
        public abstract void CloseConnection();
    }
}
