namespace Endahl.CSharpedSql.SqlServer
{
    using System.Collections.Generic;

    public static class QueryStringExtensions
    {
        public const string Key = "SqlServer";

        /// <summary>
        /// Add a custom Sql Server query string
        /// </summary>
        public static void AddSqlServer(this QueryString query, string command)
        {
            if (!query.Querys.ContainsKey(Key))
                query.Querys.Add(Key, new List<string>());
            query.Querys[Key].Add(command);
        }
    }
}
