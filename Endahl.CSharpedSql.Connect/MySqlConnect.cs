namespace Endahl.CSharpedSql.MySql
{
    using Endahl.CSharpedSql.Base;
    using MySqlConnector;
    using System.Collections.Generic;
    using System.Data;

    public class MySqlConnect : SqlConnect
    {
        protected MySqlConnection connection;
        protected MySqlCommand command;

        /// <summary>
        /// Occurs when the state of the event changes.
        /// </summary>
        public override event StateChangeEventHandler StateChange;

        /// <summary>
        /// Get the options for this <see cref="MySqlConnect"/> instance.
        /// </summary>
        public override SqlOptions SqlOptions { get; }
        /// <summary>
        /// Gets the string used to connect to a MySql database.
        /// </summary>
        public override string ConnectionString => connection.ConnectionString;

        /// <summary>
        /// Indicates the state of the <see cref="MySqlConnect"/> during the most
        /// recent network operation performed on the connection.
        /// </summary>
        public override ConnectionState State => connection.State;

        public MySqlConnect()
        {
            connection = new MySqlConnection();
            command = new MySqlCommand
            {
                Connection = connection
            };
            SqlOptions = new SqlOptions('`', '`', SqlLanguage.MySql);
            connection.StateChange += (sender, e) =>
            {
                if (StateChange != null)
                    StateChange.Invoke(sender, e);
            };
        }
        public MySqlConnect(string server, string database, string user, string password) : this()
        {
            SetConnection(server, database, user, password);
        }
        public MySqlConnect(string connectionString) : this()
        {
            connection.ConnectionString = connectionString;
        }

        /// <summary>
        /// Sets the string used to connect to a database.
        /// </summary>
        public override void SetConnection(string server, string database, string user, string password)
        {
            connection.ConnectionString = $"Server={server};Database={database};Uid={user};Pwd={password};ConvertZeroDateTime=true;";
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
            var reader = new MySqlReader(command.ExecuteReader());
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
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }
        /// <summary>
        /// Closes the connection to the database. This is the preferred method of closing
        /// any open connection.
        /// </summary>
        public override void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        /// <summary>
        /// Remove all quries from this <see cref="MySqlConnect"/> instance.
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
