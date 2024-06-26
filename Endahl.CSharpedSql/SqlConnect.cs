﻿namespace Endahl.CSharpedSql
{
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// The Base class for all Connect types.
    /// </summary>
    public abstract class SqlConnect: Sql
    {
        /// <summary>
        /// Gets the string used to connect to a database.
        /// </summary>
        public abstract string ConnectionString { get; }

        /// <summary>
        /// Sets the string used to connect to a database.
        /// </summary>
        public abstract void SetConnection(string server, string database, string user, string password);

        /// <summary>
        /// Indicates the state of the <see cref="SqlConnect"/> during the most
        /// recent network operation performed on the connection.
        /// </summary>
        public abstract ConnectionState State { get; }

        /// <summary>
        /// Executes a parameterized SQL statement and returns the number of rows affected.
        /// </summary>
        /// <returns>Number of rows affected</returns>
        public abstract int Execute();
        /// <summary>
        /// Asynchronously executes a parameterized SQL statement and returns the number of rows affected.
        /// </summary>
        /// <returns>Number of rows affected</returns>
        public abstract Task<int> ExecuteAsync();
        /// <summary>
        /// Executes a parameterized SQL statement, and returns the first column of the first row in the result
        /// set returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty</returns>
        public abstract object ExecuteScalar();
        /// <summary>
        /// Asynchronously executes a parameterized SQL statement, and returns the first column of the first row in the result
        /// set returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty</returns>
        public abstract Task<object> ExecuteScalarAsync();
        /// <summary>
        /// Executes a parameterized SQL statement and builds a <see cref="ISqlDataReader"/>
        /// </summary>
        /// <returns>A <see cref="ISqlDataReader"/> object</returns>
        public abstract ISqlDataReader ExecuteReader();
        /// <summary>
        /// Asynchronously executes a parameterized SQL statement and builds a <see cref="ISqlDataReader"/>
        /// </summary>
        /// <returns>A <see cref="ISqlDataReader"/> object</returns>
        public abstract Task<ISqlDataReader> ExecuteReaderAsync();

        /// <summary>
        /// Opens a database connection with the property settings specified by the ConnectionString.
        /// </summary>
        public abstract void OpenConnection();
        /// <summary>
        /// Asynchronously opens a database connection with the property settings specified by the ConnectionString.
        /// </summary>
        public abstract Task OpenConnectionAsync();
        /// <summary>
        /// Closes the connection to the database. This is the preferred method of closing
        /// any open connection.
        /// </summary>
        public abstract void CloseConnection();
        /// <summary>
        /// Asynchronously closes the connection to the database. This is the preferred method of closing
        /// any open connection.
        /// </summary>
        public abstract Task CloseConnectionAsync();

        /// <summary>
        /// Occurs when the state of the event changes.
        /// </summary>
        public abstract event StateChangeEventHandler StateChange;
    }
}
