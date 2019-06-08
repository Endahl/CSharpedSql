namespace Endahl.CSharpedSql.SqlServer
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class SqlServerConnect : SqlConnect
    {
        /// <summary>
        /// Gets or sets a indication of whether the connection to a database is open.
        /// </summary>
        protected bool openConnection;
        protected SqlConnection connection;
        protected SqlCommand command;

        /// <summary>
        /// Get the options for this <see cref="SqlServerConnect"/> instance.
        /// </summary>
        public override SqlOptions SqlOptions { get; }
        /// <summary>
        /// Gets a indication of whether the connection to a Sql Server database is open.
        /// </summary>
        public override bool IsConnectionOpen => openConnection;
        /// <summary>
        /// Gets the string used to connect to a Sql Server database.
        /// </summary>
        public override string ConnectionString => connection.ConnectionString;

        public SqlServerConnect()
        {
            openConnection = false;
            connection = new SqlConnection();
            command = new SqlCommand
            {
                Connection = connection
            };
            SqlOptions = new SqlOptions('[', ']', SqlLanguage.SqlServer);
        }
        public SqlServerConnect(string server, string database, string user, string password) : this()
        {
            SetConnection(server, database, user, password);
        }
        public SqlServerConnect(string connectionString) : this()
        {
            connection.ConnectionString = connectionString;
        }

        /// <summary>
        /// Sets the string used to connect to a database.
        /// </summary>
        public override void SetConnection(string server, string database, string user, string password)
        {
            connection.ConnectionString = $"Server={server};Database={database};User Id={user};Password={password};";
        }
        /// <summary>
        /// Executes a parameterized SQL statement and returns the number of rows affected.
        /// </summary>
        /// <returns>Number of rows affected</returns>
        public override int Execute()
        {
            command.CommandText = base.ToString();
            AddParameters(SqlOptions.GetSqlItems);
            var affected = command.ExecuteNonQuery();
            return affected;
        }
        /// <summary>
        /// Executes a parameterized SQL statement and builds a <see cref="ISqlDataReader"/>
        /// </summary>
        /// <returns>A <see cref="ISqlDataReader"/> object</returns>
        public override ISqlDataReader ExecuteReader()
        {
            command.CommandText = base.ToString();
            AddParameters(SqlOptions.GetSqlItems);
            var reader = new SqlServerReader(command.ExecuteReader());
            return reader;
        }
        /// <summary>
        /// Executes a parameterized SQL statement, and returns the first column of the first row in the result
        /// set returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty</returns>
        public override object ExecuteScalar()
        {
            command.CommandText = base.ToString();
            AddParameters(SqlOptions.GetSqlItems);
            var result = command.ExecuteScalar();
            return result;
        }
        /// <summary>
        ///  Opens a database connection with the property settings specified by the ConnectionString.
        /// </summary>
        public override void OpenConnection()
        {
            connection.Open();
            openConnection = true;
        }
        /// <summary>
        /// Closes the connection to the database. This is the preferred method of closing
        /// any open connection.
        /// </summary>
        public override void CloseConnection()
        {
            connection.Close();
            openConnection = false;
        }
        /// <summary>
        /// Remove all quries from this <see cref="SqlServerConnect"/> instance.
        /// </summary>
        public override void Clear()
        {
            command.Parameters.Clear();
            base.Clear();
        }

        protected virtual void AddParameters(IEnumerable<SqlItem> values)
        {
            foreach (SqlItem value in values)
            {
                command.Parameters.AddWithValue(value.ItemId, value.Value);
            }
        }
    }
}
