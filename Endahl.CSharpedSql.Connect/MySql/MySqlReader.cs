namespace Endahl.CSharpedSql.MySql
{
    using MySqlConnector;
    using System;

    /// <summary>
    /// Provides a way of reading a forward-only stream of rows from a MySql database.
    /// </summary>
    public sealed class MySqlReader : ISqlDataReader
    {
        private readonly MySqlDataReader reader;

        public MySqlReader(MySqlDataReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        public int FieldCount => reader.FieldCount;
        /// <summary>
        /// Gets a value indicating whether the <see cref="MySqlReader"/> contains one or more rows.
        /// </summary>
        public bool HasRows => reader.HasRows;
        /// <summary>
        /// Gets a value indicating whether the <see cref="MySqlReader"/> is closed.
        /// </summary>
        public bool IsClosed => reader.IsClosed;
        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.
        /// </summary>
        public int RecordsAffected => reader.RecordsAffected;
        /// <summary>
        /// Closes the <see cref="MySqlReader"/> object.
        /// </summary>
        public void Close()
        {
            reader.Close();
        }


        public bool GetBool(int index)
        {
            return (ulong)reader[index] == 1;
        }

        public bool GetBool(string name)
        {
            return (ulong)reader[name] == 1;
        }

        public byte GetByte(int index)
        {
            return (byte)reader[index];
        }

        public byte GetByte(string name)
        {
            return (byte)reader[name];
        }

        public byte[] GetByteArray(int index)
        {
            return (byte[])reader[index];
        }

        public byte[] GetByteArray(string name)
        {
            return (byte[])reader[name];
        }

        public char GetChar(int index)
        {
            return (char)reader[index];
        }

        public char GetChar(string name)
        {
            return (char)reader[name];
        }

        public DateTime GetDateTime(int index)
        {
            return (DateTime)reader[index];
        }

        public DateTime GetDateTime(string name)
        {
            return (DateTime)reader[name];
        }

        public decimal GetDecimal(int index)
        {
            return (decimal)reader[index];
        }

        public decimal GetDecimal(string name)
        {
            return (decimal)reader[name];
        }

        public float GetFloat(int index)
        {
            return (float)reader[index];
        }

        public float GetFloat(string name)
        {
            return (float)reader[name];
        }

        public Guid GetGuid(int index)
        {
            return new Guid((byte[])reader[index]);
        }

        public Guid GetGuid(string name)
        {
            return new Guid((byte[])reader[name]);
            
        }

        public int GetInt(int index)
        {
            return (int)reader[index];
        }

        public int GetInt(string name)
        {
            return (int)reader[name];
        }

        public long GetLong(int index)
        {
            return (long)reader[index];
        }

        public long GetLong(string name)
        {
            return (long)reader[name];
        }

        public sbyte GetSByte(int index)
        {
            return (sbyte)reader[index];
        }

        public sbyte GetSByte(string name)
        {
            return (sbyte)reader[name];
        }

        public short GetShort(int index)
        {
            return (short)reader[index];
        }

        public short GetShort(string name)
        {
            return (short)reader[name];
        }

        public string GetString(int index)
        {
            return (string)reader[index];
        }

        public string GetString(string name)
        {
            return (string)reader[name];
        }

        public uint GetUInt(int index)
        {
            return (uint)reader[index];
        }

        public uint GetUInt(string name)
        {
            return (uint)reader[name];
        }

        public ulong GetULong(int index)
        {
            return (ulong)reader[index];
        }

        public ulong GetULong(string name)
        {
            return (ulong)reader[name];
        }

        public ushort GetUShort(int index)
        {
            return (ushort)reader[index];
        }

        public ushort GetUShort(string name)
        {
            return (ushort)reader[name];
        }

        /// <summary>
        /// Advances the <see cref="MySqlReader"/> to the next result, when reading the results of batch
        /// SQL statements.
        /// </summary>
        public bool NextResult()
        {
            return reader.NextResult();
        }
        /// <summary>
        /// Advances the <see cref="MySqlReader"/> to the next record.
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            return reader.Read();
        }
    }
}
