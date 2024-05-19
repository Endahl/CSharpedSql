using Endahl.CSharpedSql;
using NUnit.Framework;

namespace Endahl.CSharpedSqlTests.MySql
{
    public class OrderByTests
    {
        [Test]
        public void OrderBy_ASC()
        {
            //arrange
            var sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` ORDER BY `id` ASC;";

            //act
            sql.Query(Select.From("test") + OrderBy.ASC("id"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void OrderBy_DESC()
        {
            //arrange
            var sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` ORDER BY `id` DESC;";

            //act
            sql.Query(Select.From("test") + OrderBy.DESC("id"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void OrderBy_MultiColumns()
        {
            //arrange
            var sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` ORDER BY `id`, `name`, `user` ASC;";

            //act
            sql.Query(Select.From("test") + OrderBy.ASC("id", "name", "user"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
