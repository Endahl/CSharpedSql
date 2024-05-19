namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;

    public class InsertFunction
    {
        public virtual Function Function { get; }

        protected InsertFunction(Function function)
        {
            Function = function;
        }

        /// <summary>
        /// Returns the <see cref="InsertFunction"/> as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="InsertFunction"/> as a string.
        /// </summary>
        public virtual string ToString(SqlOptions sql) => Function.ToString(sql);

        #region String Functions
        /// <summary>
        /// Returns a string of the specified number of space characters
        /// </summary>
        public static InsertFunction Space(int length)
        {
            return new(Function.Space(length));
        }
        #endregion

        #region Numeric Functions
        /// <summary>
        /// Return the absolute value of a number
        /// </summary>
        public static InsertFunction Absolute(int value)
        {
            return new(Function.Absolute(value));
        }
        /// <summary>
        /// Return the absolute value of a number
        /// </summary>
        public static InsertFunction Absolute(double value)
        {
            return new(Function.Absolute(value));
        }
        /// <summary>
        /// Returns the arc cosine of a number
        /// </summary>
        public static InsertFunction ACos(int value)
        {
            return new(Function.ACos(value));
        }
        /// <summary>
        /// Returns the arc cosine of a number
        /// </summary>
        public static InsertFunction ACos(double value)
        {
            return new(Function.ACos(value));
        }
        /// <summary>
        /// Returns the arc sine of a number
        /// </summary>
        public static InsertFunction ASin(int value)
        {
            return new(Function.ASin(value));
        }
        /// <summary>
        /// Returns the arc sine of a number
        /// </summary>
        public static InsertFunction ASin(double value)
        {
            return new(Function.ASin(value));
        }
        /// <summary>
        /// Returns the arc tangent of a number
        /// </summary>
        public static InsertFunction ATan(int value)
        {
            return new(Function.ATan(value));
        }
        /// <summary>
        /// Returns the arc tangent of a number
        /// </summary>
        public static InsertFunction ATan(double value)
        {
            return new(Function.ATan(value));
        }
        /// <summary>
        /// Returns the smallest integer value that is >= a number
        /// </summary>
        public static InsertFunction Ceiling(double value)
        {
            return new(Function.Ceiling(value));
        }
        /// <summary>
        /// Returns the cosine of a number
        /// </summary>
        public static InsertFunction Cos(int value)
        {
            return new(Function.Cos(value));
        }
        /// <summary>
        /// Returns the cosine of a number
        /// </summary>
        public static InsertFunction Cos(double value)
        {
            return new(Function.Cos(value));
        }
        /// <summary>
        /// Returns the cotangent of a number
        /// </summary>
        public static InsertFunction Cot(int value)
        {
            return new(Function.Cot(value));
        }
        /// <summary>
        /// Returns the cotangent of a number
        /// </summary>
        public static InsertFunction Cot(double value)
        {
            return new(Function.Cot(value));
        }
        /// <summary>
        /// Converts a value in radians to degrees
        /// </summary>
        public static InsertFunction Degrees(int value)
        {
            return new(Function.Degrees(value));
        }
        /// <summary>
        /// Converts a value in radians to degrees
        /// </summary>
        public static InsertFunction Degrees(double value)
        {
            return new(Function.Degrees(value));
        }
        /// <summary>
        /// Returns e raised to the power of a specified number
        /// </summary>
        public static InsertFunction Exp(int value)
        {
            return new(Function.Exp(value));
        }
        /// <summary>
        /// Returns e raised to the power of a specified number
        /// </summary>
        public static InsertFunction Exp(double value)
        {
            return new(Function.Exp(value));
        }
        /// <summary>
        /// Returns the largest integer value that is &lt;= to a number
        /// </summary>
        public static InsertFunction Floor(double value)
        {
            return new(Function.Floor(value));
        }
        /// <summary>
        /// Returns the natural logarithm of a number
        /// </summary>
        public static InsertFunction Log(int value)
        {
            return new(Function.Log(value));
        }
        /// <summary>
        /// Returns the natural logarithm of a number
        /// </summary>
        public static InsertFunction Log(double value)
        {
            return new(Function.Log(value));
        }
        /// <summary>
        /// Returns the natural logarithm of a number to base 10
        /// </summary>
        public static InsertFunction Log10(int value)
        {
            return new(Function.Log10(value));
        }
        /// <summary>
        /// Returns the natural logarithm of a number to base 10
        /// </summary>
        public static InsertFunction Log10(double value)
        {
            return new(Function.Log10(value));
        }
        /// <summary>
        /// Returns the value of PI
        /// </summary>
        public static InsertFunction PI()
        {
            return new(Function.PI());
        }
        /// <summary>
        /// Converts a degree value into radians
        /// </summary>
        public static InsertFunction Radians(int value)
        {
            return new(Function.Radians(value));
        }
        /// <summary>
        /// Converts a degree value into radians
        /// </summary>
        public static InsertFunction Radians(double value)
        {
            return new(Function.Radians(value));
        }
        /// <summary>
        /// Returns a random number between 0 and 1
        /// </summary>
        public static InsertFunction Random()
        {
            return new(Function.Random());
        }
        /// <summary>
        /// Returns a repeatable sequence of random numbers between 0 and 1
        /// </summary>
        public static InsertFunction Random(int seed)
        {
            return new(Function.Random(seed));
        }
        /// <summary>
        /// Returns a random number between start and end
        /// </summary>
        public static InsertFunction Random(int start, int end)
        {
            return new(Function.Random(start, end));
        }
        /// <summary>
        /// Rounds a number to a specified number of decimal places
        /// </summary>
        public static InsertFunction Round(double value, int decimals)
        {
            return new(Function.Round(value, decimals));
        }
        /// <summary>
        /// Returns the sign of a number
        /// (number &gt; 0 returns 1),
        /// (number = 0 returns 0) or
        /// (number &lt; 0 returns -1)
        /// </summary>
        public static InsertFunction Sign(int value)
        {
            return new(Function.Sign(value));
        }
        /// <summary>
        /// Returns the sign of a number
        /// (number &gt; 0 returns 1),
        /// (number = 0 returns 0) or
        /// (number &lt; 0 returns -1)
        /// </summary>
        public static InsertFunction Sign(double value)
        {
            return new(Function.Sign(value));
        }
        /// <summary>
        /// Returns the sine of a number
        /// </summary>
        public static InsertFunction Sin(int value)
        {
            return new(Function.Sin(value));
        }
        /// <summary>
        /// Returns the sine of a number
        /// </summary>
        public static InsertFunction Sin(double value)
        {
            return new(Function.Sin(value));
        }
        /// <summary>
        /// Returns the square root of a number
        /// </summary>
        public static InsertFunction SquareRoot(int value)
        {
            return new(Function.SquareRoot(value));
        }
        /// <summary>
        /// Returns the square root of a number
        /// </summary>
        public static InsertFunction SquareRoot(double value)
        {
            return new(Function.SquareRoot(value));
        }
        /// <summary>
        /// Returns the tangent of a number
        /// </summary>
        public static InsertFunction Tan(int value)
        {
            return new(Function.Tan(value));
        }
        /// <summary>
        /// Returns the tangent of a number
        /// </summary>
        public static InsertFunction Tan(double value)
        {
            return new(Function.Tan(value));
        }
        #endregion

        #region DateTime Functions
        /// <summary>
        /// Returns the current date and time
        /// </summary>
        public static InsertFunction GetDate()
        {
            return new(Function.GetDate());
        }
        #endregion

        /// <summary>
        /// Return a Globally Unique Identifier
        /// </summary>
        public static InsertFunction NewGuid()
        {
            return new(Function.NewGuid());
        }
    }
}
