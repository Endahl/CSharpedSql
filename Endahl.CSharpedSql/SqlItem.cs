namespace Endahl.CSharpedSql
{
    /// <summary>
    /// A item containing a item id and a value to use in a sql command
    /// </summary>
    public class SqlItem
    {
        /// <summary>
        /// Return a item containing a item id and a value to use in a sql command
        /// </summary>
        public SqlItem(string itemId, object value) 
        {
            ItemId = itemId;
            Value = value;
        }

        /// <summary>
        /// The name of the id
        /// </summary>
        public virtual string ItemId { get; }
        /// <summary>
        /// The value of the id
        /// </summary>
        public virtual object Value { get; }
    }
}
