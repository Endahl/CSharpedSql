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
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` ORDER BY `id` ASC;";

            //act
            sql.Query(Select.From("test") + OrderBy.ASC("id"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderBy_DESC()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` ORDER BY `id` DESC;";

            //act
            sql.Query(Select.From("test") + OrderBy.DESC("id"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderBy_MultiColumns()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` ORDER BY `id`, `name`, `user` ASC;";

            //act
            sql.Query(Select.From("test") + OrderBy.ASC("id", "name", "user"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
