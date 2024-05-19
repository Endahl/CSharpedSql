using Endahl.CSharpedSql;
using NUnit.Framework;

namespace Endahl.CSharpedSqlTests.SqlServer
{
    public class SelectTests
    {
        [Test]
        public void Select_All_From()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT * FROM [test];";

            //act
            sql.Query(Select.From("test"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Select_Top_All_From()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT TOP 3 * FROM [test];";

            //act
            sql.Query(Select.TopFrom(3, "test"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Select_Distinct_All_From()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT DISTINCT * FROM [test];";

            //act
            sql.Query(Select.DistinctFrom("test"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Select_SomeColumns_From()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT [hello], [name] FROM [test];";

            //act
            sql.Query(Select.From("test", "hello", "name"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Select_From_Where_OrderBy_Join()
        {
            //arrange
            var sql = new CSharpedSql.SqlServer.SqlServerConnect();
            var expected = "SELECT [test].[hello], [test2].[name] FROM [test] LEFT JOIN [test2] ON [test].[id] = [test2].[name] WHERE [test].[id] = @item0 ORDER BY [test2].[name] ASC;";

            //act
            sql.Query(Select.From("test", "test.hello", "test2.name") + Where.Equal("test.id", 2) + Join.Left("test", "id", "test2", "name") + OrderBy.ASC("test2.name"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
