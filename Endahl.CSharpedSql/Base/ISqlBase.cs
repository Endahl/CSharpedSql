namespace Endahl.CSharpedSql.Base
{
    /// <summary>
    /// The base of how sql should be returned as a string
    /// </summary>
    public interface ISqlBase
    {
        string Alter(Alter alter, SqlOptions sql);
        string Case(Case @case, SqlOptions sql);
        string Column(Column column, SqlOptions sql);
        string Condition(Condition condition, SqlOptions sql);
        string Create(Create create, SqlOptions sql);
        string Delete(Delete delete, SqlOptions sql);
        string Drop(Drop drop, SqlOptions sql);
        string Function(Function function, SqlOptions sql);
        string GroupBy(GroupBy groupBy, SqlOptions sql);
        string Having(Having having, SqlOptions sql);
        string Insert(Insert insert, SqlOptions sql);
        string Join(Join join, SqlOptions sql);
        string OrderBy(OrderBy orderBy, SqlOptions sql);
        string Select(Select select, SqlOptions sql);
        string Update(Update update, SqlOptions sql);
        string Where(Where where, SqlOptions sql);

        string CSharpTypeToSqlDataType(CSharpType type, uint size = 0, int digits = 0);
        object HandleObject(object obj);
    }
}
