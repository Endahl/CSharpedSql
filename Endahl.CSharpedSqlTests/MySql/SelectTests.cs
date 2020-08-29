using Endahl.CSharpedSql;
using NUnit.Framework;

namespace Endahl.CSharpedSqlTests.MySql
{
    public class SelectTests
    {
        [Test]
        public void Select_All_From()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test`;";

            //act
            sql.Query(Select.From("test"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Select_Top_All_From()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT * FROM `test` LIMIT 3;";

            //act
            sql.Query(Select.TopFrom(3, "test"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Select_Distinct_All_From()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT DISTINCT * FROM `test`;";

            //act
            sql.Query(Select.DistinctFrom("test"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Select_SomeColumns_From()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT `hello`, `name` FROM `test`;";

            //act
            sql.Query(Select.From("test", "hello", "name"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Select_From_Where_OrderBy_Join()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT `test`.`hello`, `test2`.`name` FROM `test` LEFT JOIN `test2` ON `test`.`id` = `test2`.`name` WHERE `test`.`id` = @item0 ORDER BY `test2`.`name` ASC;";

            //act
            sql.Query(Select.From("test", "test.hello", "test2.name") + Where.Equal("test.id", 2) + Join.Left("test", "id", "test2", "name") + OrderBy.ASC("test2.name"));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Select_Multi_Wheres()
        {
            //arrange
            SqlConnect sql = new CSharpedSql.MySql.MySqlConnect();
            var expected = "SELECT `hello`, `name` FROM `test` WHERE `id` = @item0 AND `name` = @item1 OR `cp` = @item2;";

            //act
            sql.Query(Select.From("test", "hello", "name")
                + Where.Equal("id", 1)
                + Where.Equal("name", "bob")
                | Where.Equal("cp", 3));
            var actual = sql.ToString().TrimEnd();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
