using Endahl.CSharpedSql;
using NUnit.Framework;

namespace Endahl.CSharpedSqlTests.SqlServer
{
    public class JoinTests
    {
        [Test]
        public void Join_Inner()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT [test].[id] FROM [test] INNER JOIN [test2] ON [test].[id] = [test2].[name];";

            //act
            sql.Query(Select.From("test", "test.id") + Join.Inner("test", "id", "test2", "name"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Join_Left()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT [test].[id] FROM [test] LEFT JOIN [test2] ON [test].[id] = [test2].[name];";

            //act
            sql.Query(Select.From("test", "test.id") + Join.Left("test", "id", "test2", "name"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Join_Right()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT [test].[id] FROM [test] RIGHT JOIN [test2] ON [test].[id] = [test2].[name];";

            //act
            sql.Query(Select.From("test", "test.id") + Join.Right("test", "id", "test2", "name"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Join_Full()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT [test].[id] FROM [test] FULL OUTER JOIN [test2] ON [test].[id] = [test2].[name];";

            //act
            sql.Query(Select.From("test", "test.id") + Join.Full("test", "id", "test2", "name"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
