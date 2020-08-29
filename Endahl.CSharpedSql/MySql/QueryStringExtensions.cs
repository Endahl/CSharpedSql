namespace Endahl.CSharpedSql.MySql
{
    using System.Collections.Generic;

    public static class QueryStringExtensions
    {
        public const string Key = "MySql";

        /// <summary>
        /// Add a custom MySql query string
        /// </summary>
        public static void AddMySql(this QueryString query, string command)
        {
            if (!query.Querys.ContainsKey(Key))
                query.Querys.Add(Key, new List<string>());
            query.Querys[Key].Add(command);
        }
    }
}
