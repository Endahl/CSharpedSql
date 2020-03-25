namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System.Collections.Generic;

    /// <summary>
    /// The base class for all SQL types.
    /// </summary>
    public abstract class Sql
    {
        /// <summary>
        /// A list containing the quries added to this <see cref="Sql"/> instance.
        /// </summary>
        protected virtual IList<ISqlStatement> Quries { get; }
        /// <summary>
        /// Gets the number of quries added to this <see cref="Sql"/> instance.
        /// </summary>
        public virtual int QuriesCount => Quries.Count;
        /// <summary>
        /// Get the options for this <see cref="Sql"/> instance.
        /// </summary>
        public abstract SqlOptions SqlOptions { get; }

        public Sql()
        {
            Quries = new List<ISqlStatement>();
        }

        /// <summary>
        /// Remove all quries from this <see cref="Sql"/> instance.
        /// </summary>
        public virtual void Clear()
        {
            SqlOptions.ClearItems();
            Quries.Clear();
        }

        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="alter">A CREATE statement</param>
        public virtual void Query(Alter alter)
        {
            Quries.Add(alter);
        }
        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="create">A CREATE statement</param>
        public virtual void Query(Create create)
        {
            Quries.Add(create);
        }
        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="drop">A DROP statement</param>
        public virtual void Query(Drop drop)
        {
            Quries.Add(drop);
        }
        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="insert">A INSERT statement</param>
        public virtual void Query(Insert insert)
        {
            Quries.Add(insert);
        }
        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="delete">A DELETE statement, good practice to add a <see cref="Where"/> clause</param>
        public virtual void Query(Delete delete)
        {
            Quries.Add(delete);
        }
        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="update">A UPDATE statement, good practice to add a <see cref="Where"/> clause</param>
        public virtual void Query(Update update)
        {
            Quries.Add(update);
        }
        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="select">A SELECT statement</param>
        public virtual void Query(Select select)
        {
            Quries.Add(select);
        }
        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="statement"></param>
        public virtual void Query(QueryString statement)
        {
            Quries.Add(statement);
        }
        /// <summary>
        /// Add a query to this <see cref="Sql"/> instance.
        /// </summary>
        /// <param name="statement"></param>
        public virtual void Query(ISqlStatement statement)
        {
            Quries.Add(statement);
        }

        /// <summary>
        /// Return the SQL query/queris as a string.
        /// </summary>
        public override string ToString()
        {
            var result = "";
            foreach (var statement in Quries)
                result += $"{statement.ToString(SqlOptions)};";
            return result;
        }
    }
}
