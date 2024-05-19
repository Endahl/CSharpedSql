namespace Endahl.CSharpedSql.SqlServer
{
    using System;
    using Endahl.CSharpedSql.MySql;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Provides a way of reading a forward-only stream of rows from a Sql Server database.
    /// </summary>
    public sealed class SqlServerReader(SqlDataReader reader) : ISqlDataReader
    {
        private readonly SqlDataReader reader = reader;

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        public int FieldCount => reader.FieldCount;
        /// <summary>
        /// Gets a value indicating whether the <see cref="SqlServerReader"/> contains one or more rows.
        /// </summary>
        public bool HasRows => reader.HasRows;
        /// <summary>
        /// Gets a value indicating whether the <see cref="SqlServerReader"/> is closed.
        /// </summary>
        public bool IsClosed => reader.IsClosed;
        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.
        /// </summary>
        public int RecordsAffected => reader.RecordsAffected;

        /// <summary>
        /// Closes the <see cref="SqlServerReader"/> object.
        /// </summary>
        public void Close()
        {
            reader.Close();
        }
        /// <summary>
        /// Asynchronously closes the <see cref="MySqlReader"/> object.
        /// </summary>
        public async Task CloseAsync()
        {
            await reader.CloseAsync();
        }

        public bool GetBool(int index) => (byte)reader[index] == 1;

        public bool GetBool(string name) => (byte)reader[name] == 1;

        public byte GetByte(int index) => (byte)reader[index];

        public byte GetByte(string name) => (byte)reader[name];

        public byte[] GetByteArray(int index) => (byte[])reader[index];

        public byte[] GetByteArray(string name) => (byte[])reader[name];

        public char GetChar(int index) => (char)reader[index];

        public char GetChar(string name) => (char)reader[name];

        public DateTime GetDateTime(int index) => (DateTime)reader[index];

        public DateTime GetDateTime(string name) => (DateTime)reader[name];

        public decimal GetDecimal(int index) => (decimal)reader[index];

        public decimal GetDecimal(string name) => (decimal)reader[name];

        public float GetFloat(int index) => (float)reader[index];

        public float GetFloat(string name) => (float)reader[name];

        public Guid GetGuid(int index) => (Guid)reader[index];

        public Guid GetGuid(string name) => (Guid)reader[name];

        public int GetInt(int index) => (int)reader[index];

        public int GetInt(string name) => (int)reader[name];

        public long GetLong(int index) => (long)reader[index];

        public long GetLong(string name) => (long)reader[name];

        public sbyte GetSByte(int index) => unchecked((sbyte)reader[index]);

        public sbyte GetSByte(string name) => unchecked((sbyte)reader[name]);

        public short GetShort(int index) => (short)reader[index];

        public short GetShort(string name) => (short)reader[name];

        public string GetString(int index) => (string)reader[index];

        public string GetString(string name) => (string)reader[name];

        public uint GetUInt(int index) => unchecked((uint)reader[index]);

        public uint GetUInt(string name) => unchecked((uint)reader[name]);

        public ulong GetULong(int index) => unchecked((ulong)reader[index]);

        public ulong GetULong(string name) => unchecked((ulong)reader[name]);

        public ushort GetUShort(int index) => unchecked((ushort)reader[index]);

        public ushort GetUShort(string name) => unchecked((ushort)reader[name]);

        /// <summary>
        /// Advances the <see cref="SqlServerReader"/> to the next result, when reading the results of batch
        /// SQL statements.
        /// </summary>
        public bool NextResult() => reader.NextResult();
        /// <summary>
        /// Asynchronously advances the <see cref="MySqlReader"/> to the next result, when reading the results of batch
        /// SQL statements.
        /// </summary>
        public async Task<bool> NextResultAsync() => await reader.NextResultAsync();

        /// <summary>
        /// Advances the <see cref="SqlServerReader"/> to the next record.
        /// </summary>
        /// <returns></returns>
        public bool Read() => reader.Read();
        /// <summary>
        /// Asynchronously advances the <see cref="MySqlReader"/> to the next record.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ReadAsync() => await reader.ReadAsync();
    }
}
