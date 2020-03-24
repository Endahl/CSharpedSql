using Endahl.CSharpedSql;
using NUnit.Framework;

namespace Endahl.CSharpedSqlTests.MySql
{
    public class WhereTests
    {
        [Test]
        public void Where_Equal()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` = @item0;";

            //act
            sql.Query(Select.From("test") + Where.Equal("id", 23));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_All()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` = ALL (SELECT * FROM `table`);";

            //act
            sql.Query(Select.From("test") + Where.All("id", Select.From("table")));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_Any()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` = ANY (SELECT * FROM `table`);";

            //act
            sql.Query(Select.From("test") + Where.Any("id", Select.From("table")));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_GreaterThan()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` > @item0;";

            //act
            sql.Query(Select.From("test") + Where.GreaterThan("id", 23));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_LessThan()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` < @item0;";

            //act
            sql.Query(Select.From("test") + Where.LessThan("id", 23));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_NotEqual()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` <> @item0;";

            //act
            sql.Query(Select.From("test") + Where.NotEqual("id", 23));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_Between()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` BETWEEN @item0 AND @item1;";

            //act
            sql.Query(Select.From("test") + Where.Between("id", 4, 8));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_NotBetween()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` NOT BETWEEN @item0 AND @item1;";

            //act
            sql.Query(Select.From("test") + Where.NotBetween("id", 4, 9));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_Exists()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE EXISTS (SELECT `id` FROM `user` WHERE `id` = @item0);";

            //act
            sql.Query(Select.From("test") + Where.Exists(Select.From("user", "id") + Where.Equal("id", 23)));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_NotExists()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE NOT EXISTS (SELECT `id` FROM `user` WHERE `id` = @item0);";

            //act
            sql.Query(Select.From("test") + Where.NotExists(Select.From("user", "id") + Where.Equal("id", 23)));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_Like()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` LIKE @item0;";

            //act
            sql.Query(Select.From("test") + Where.Like("id", "ggg%"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_And_Where()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` = @item0 AND `name` <> @item1;";

            //act
            sql.Query(Select.From("test") + (Where.Equal("id", 23) + Where.NotEqual("name", "Kim")));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_Or_Where()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE `id` = @item0 OR `name` <> @item1;";

            //act
            sql.Query(Select.From("test") + (Where.Equal("id", 23) | Where.NotEqual("name", "Tim")));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Where_Parentheses()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` WHERE (`id` = @item0 OR `pas` = @item1) AND `name` <> @item2;";

            //act
            sql.Query(Select.From("test") + Where.Parentheses(Where.Equal("id", 23) | Where.Equal("pas", 1)) & Where.NotEqual("name", "Tim"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}