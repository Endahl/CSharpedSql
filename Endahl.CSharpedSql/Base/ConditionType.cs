namespace Endahl.CSharpedSql.Base
{
    public enum ConditionType
    {
        All,
        Any,
        Between,
        NotBetween,
        /// <summary>
        /// =
        /// </summary>
        Equal,
        /// <summary>
        /// !=
        /// </summary>
        NotEqual,
        Exists,
        NotExists,
        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan,
        /// <summary>
        /// &lt;
        /// </summary>
        LessThan,
        Like,
        In,
        NotIn,
        /// <summary>
        /// = null
        /// </summary>
        IsNull,
        /// <summary>
        /// != null
        /// </summary>
        IsNotNull
    }
}
