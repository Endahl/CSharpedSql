namespace Endahl.CSharpedSql
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A Interface that provides a way of reading a forward-only stream of rows from a Sql database.
    /// </summary>
    public interface ISqlDataReader
    {
        /// <summary>
        /// Closes the <see cref="ISqlDataReader"/> object.
        /// </summary>
        void Close();
        /// <summary>
        /// Asynchronously closes the <see cref="ISqlDataReader"/> object.
        /// </summary>
        Task CloseAsync();
        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        int FieldCount { get; }
        /// <summary>
        /// Gets a value indicating whether the <see cref="ISqlDataReader"/> contains one or more rows.
        /// </summary>
        bool HasRows { get; }
        /// <summary>
        /// Gets a value indicating whether the <see cref="ISqlDataReader"/> is closed.
        /// </summary>
        bool IsClosed { get; }
        /// <summary>
        /// Advances the <see cref="ISqlDataReader"/> to the next result, when reading the results of batch
        /// SQL statements.
        /// </summary>
        bool NextResult();
        /// <summary>
        /// Asynchronously advances the <see cref="ISqlDataReader"/> to the next result, when reading the results of batch
        /// SQL statements.
        /// </summary>
        Task<bool> NextResultAsync();
        /// <summary>
        /// Advances the <see cref="ISqlDataReader"/> to the next record.
        /// </summary>
        /// <returns></returns>
        bool Read();
        /// <summary>
        /// Asynchronously advances the <see cref="ISqlDataReader"/> to the next record.
        /// </summary>
        /// <returns></returns>
        Task<bool> ReadAsync();
        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.
        /// </summary>
        int RecordsAffected { get; }

        #region Getters
        char GetChar(int index);
        char GetChar(string name);

        string GetString(int index);
        string GetString(string name);

        bool GetBool(int index);
        bool GetBool(string name);

        byte GetByte(int index);
        byte GetByte(string name);

        short GetShort(int index);
        short GetShort(string name);

        int GetInt(int index);
        int GetInt(string name);

        long GetLong(int index);
        long GetLong(string name);

        float GetFloat(int index);
        float GetFloat(string name);

        decimal GetDecimal(int index);
        decimal GetDecimal(string name);

        DateTime GetDateTime(int index);
        DateTime GetDateTime(string name);

        byte[] GetByteArray(int index);
        byte[] GetByteArray(string name);

        sbyte GetSByte(int index);
        sbyte GetSByte(string name);

        ushort GetUShort(int index);
        ushort GetUShort(string name);

        uint GetUInt(int index);
        uint GetUInt(string name);

        ulong GetULong(int index);
        ulong GetULong(string name);

        Guid GetGuid(int index);
        Guid GetGuid(string name);
        #endregion
    }
}
