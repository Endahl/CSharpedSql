﻿namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System;

    /// <summary>
    /// SQL function
    /// </summary>
    public class Function : ColumnItem
    {
        public override string Name {
            get => base.Name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    base.Name = value;
            }
        }
        public virtual FunctionType FunctionType { get; }
        public virtual string ColumnName { get; }
        public virtual string String { get; }
        public virtual string String2 { get; }
        public virtual int? Number { get; }
        public virtual int? Number2 { get; }
        public virtual double? Decimal { get; }

        #region Constructers
        protected Function(FunctionType functionType)
        {
            Name = functionType.ToString();
            FunctionType = functionType;
        }
        protected Function(FunctionType functionType, int number) : this(functionType)
        {
            Number = number;
        }
        protected Function(FunctionType functionType, int number, int number2) : this(functionType, number)
        {
            Number2 = number2;
        }
        protected Function(FunctionType functionType, double number) : this(functionType)
        {
            Decimal = number;
        }
        protected Function(FunctionType functionType, double number, int number2) : this(functionType, number)
        {
            Number = number2;
        }
        protected Function(FunctionType functionType, string columnName): this(functionType)
        {
            Name += $"({columnName})";
            ColumnName = columnName;
        }
        protected Function(FunctionType functionType, string columnName, string subString) : this (functionType, columnName)
        {
            String = subString;
        }
        protected Function(FunctionType functionType, string columnName, string subString, string subString2):
            this(functionType, columnName, subString)
        {
            String2 = subString2;
        }
        protected Function(FunctionType functionType, string columnName, int number) : this(functionType, columnName)
        {
            Number = number;
        }
        protected Function(FunctionType functionType, string columnName, int number, int number2) : this(functionType, columnName, number)
        {
            Number2 = number2;
        }
        protected Function(FunctionType functionType, string columnName, int number, int number2, string subString):
            this(functionType, columnName, number, number2)
        {
            String = subString;
        }
        protected Function(FunctionType functionType, string columnName, double number): this(functionType, columnName)
        {
            Decimal = number;
        }
        #endregion

        /// <summary>
        /// Set the name for the select result
        /// </summary>
        public virtual Function As(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        /// Returns the <see cref="Function"/> as a string.
        /// </summary>
        public override string ToString() => ToString(new SqlOptions());
        /// <summary>
        /// Returns the <see cref="Function"/> as a string.
        /// </summary>
        public override string ToString(SqlOptions sql) => sql.SqlBase.Function(this, sql);

        #region String Functions
        /// <summary>
        /// returns the ASCII value for the specific character
        /// </summary>
        public static Function Ascii(string columnName)
        {
            return new(FunctionType.Ascii, columnName);
        }
        /// <summary>
        /// Returns the position of the first occurrence of a substring in a string
        /// </summary>
        public static Function IndexOf(string columnName, string subString)
        {
            return new(FunctionType.IndexOf, columnName, subString);
        }
        /// <summary>
        /// Inserts a string within a string at the specified position and for a certain number of characters
        /// </summary>
        public static Function Insert(string columnName, int start, int length, string subString)
        {
            return new(FunctionType.Insert, columnName, start, length, subString);
        }
        /// <summary>
        /// Extracts a number of characters from a string (starting from left)
        /// </summary>
        public static Function Left(string columnName, int count)
        {
            return new(FunctionType.Left, columnName, count);
        }
        /// <summary>
        /// Returns the length of a string
        /// </summary>
        public static Function Length(string columnName)
        {
            return new(FunctionType.Length, columnName);
        }
        /// <summary>
        /// Returns the position of the first occurrence of a substring in a string
        /// </summary>
        public static Function Locate(string columnName, string subString)
        {
            return new(FunctionType.Locate, columnName, subString);
        }
        /// <summary>
        /// Removes leading spaces from a string
        /// </summary>
        public static Function LeftTrim(string columnName)
        {
            return new(FunctionType.LeftTrim, columnName);
        }
        /// <summary>
        /// Repeats a string a specified number of times
        /// </summary>
        public static Function Repeat(string columnName, int count)
        {
            return new(FunctionType.Repeat, columnName, count);
        }
        /// <summary>
        /// Replaces all occurrences of a substring within a string, with a new substring
        /// </summary>
        public static Function Replace(string columnName, string oldString, string newString)
        {
            return new(FunctionType.Replace, columnName, oldString, newString);
        }
        /// <summary>
        /// Reverses a string and returns the result
        /// </summary>
        public static Function Reverse(string columnName)
        {
            return new(FunctionType.Reverse, columnName);
        }
        /// <summary>
        /// Extracts a number of characters from a string (starting from right)
        /// </summary>
        public static Function Right(string columnName, int count)
        {
            return new(FunctionType.Right, columnName, count);
        }
        /// <summary>
        /// Removes trailing spaces from a string
        /// </summary>
        public static Function RightTrim(string columnName)
        {
            return new(FunctionType.RightTrim, columnName);
        }
        /// <summary>
        /// Returns a string of the specified number of space characters
        /// </summary>
        public static Function Space(int length)
        {
            return new(FunctionType.Space, length);
        }
        /// <summary>
        /// Extracts a substring from a string (starting at any position)
        /// </summary>
        public static Function SubString(string columnName, int start, int length)
        {
            return new(FunctionType.SubString, columnName, start, length);
        }
        /// <summary>
        /// Converts a string to lower-case
        /// </summary>
        public static Function ToLower(string columnName)
        {
            return new(FunctionType.ToLower, columnName);
        }
        /// <summary>
        /// Converts a string to upper-case
        /// </summary>
        public static Function ToUpper(string columnName)
        {
            return new(FunctionType.ToUpper, columnName);
        }
        /// <summary>
        /// Removes leading and trailing spaces from a string
        /// </summary>
        public static Function Trim(string columnName)
        {
            return new(FunctionType.Trim, columnName);
        }
        #endregion

        #region Numeric Functions
        /// <summary>
        /// Return the absolute value of a number
        /// </summary>
        public static Function Absolute(string columnName)
        {
            return new(FunctionType.Absolute, columnName);
        }
        /// <summary>
        /// Return the absolute value of a number
        /// </summary>
        public static Function Absolute(int value)
        {
            return new(FunctionType.Absolute, value);
        }
        /// <summary>
        /// Return the absolute value of a number
        /// </summary>
        public static Function Absolute(double value)
        {
            return new(FunctionType.Absolute, value);
        }
        /// <summary>
        /// Returns the arc cosine of a number
        /// </summary>
        public static Function ACos(string columnName)
        {
            return new(FunctionType.ArcCos, columnName);
        }
        /// <summary>
        /// Returns the arc cosine of a number
        /// </summary>
        public static Function ACos(int value)
        {
            return new(FunctionType.ArcCos, value);
        }
        /// <summary>
        /// Returns the arc cosine of a number
        /// </summary>
        public static Function ACos(double value)
        {
            return new(FunctionType.ArcCos, value);
        }
        /// <summary>
        /// Returns the arc sine of a number
        /// </summary>
        public static Function ASin(string columnName)
        {
            return new(FunctionType.ArcSin, columnName);
        }
        /// <summary>
        /// Returns the arc sine of a number
        /// </summary>
        public static Function ASin(int value)
        {
            return new(FunctionType.ArcSin, value);
        }
        /// <summary>
        /// Returns the arc sine of a number
        /// </summary>
        public static Function ASin(double value)
        {
            return new(FunctionType.ArcSin, value);
        }
        /// <summary>
        /// Returns the arc tangent of a number
        /// </summary>
        public static Function ATan(string columnName)
        {
            return new(FunctionType.ArcTan, columnName);
        }
        /// <summary>
        /// Returns the arc tangent of a number
        /// </summary>
        public static Function ATan(int value)
        {
            return new(FunctionType.ArcTan, value);
        }
        /// <summary>
        /// Returns the arc tangent of a number
        /// </summary>
        public static Function ATan(double value)
        {
            return new(FunctionType.ArcTan, value);
        }
        /// <summary>
        /// Returns the average value of a numeric column
        /// </summary>
        public static Function Average(string columnName)
        {
            return new(FunctionType.Average, columnName);
        }
        /// <summary>
        /// Returns the smallest integer value that is >= a number
        /// </summary>
        public static Function Ceiling(string columnName)
        {
            return new(FunctionType.Ceiling, columnName);
        }
        /// <summary>
        /// Returns the smallest integer value that is >= a number
        /// </summary>
        public static Function Ceiling(double value)
        {
            return new(FunctionType.Ceiling, value);
        }
        /// <summary>
        /// returns the number of rows that matches a specified criteria
        /// </summary>
        public static Function Count(string columnName)
        {
            return new(FunctionType.Count, columnName);
        }
        /// <summary>
        /// Returns the cosine of a number
        /// </summary>
        public static Function Cos(string columnName)
        {
            return new(FunctionType.Cos, columnName);
        }
        /// <summary>
        /// Returns the cosine of a number
        /// </summary>
        public static Function Cos(int value)
        {
            return new(FunctionType.Cos, value);
        }
        /// <summary>
        /// Returns the cosine of a number
        /// </summary>
        public static Function Cos(double value)
        {
            return new(FunctionType.Cos, value);
        }
        /// <summary>
        /// Returns the cotangent of a number
        /// </summary>
        public static Function Cot(string columnName)
        {
            return new(FunctionType.Cot, columnName);
        }
        /// <summary>
        /// Returns the cotangent of a number
        /// </summary>
        public static Function Cot(int value)
        {
            return new(FunctionType.Cot, value);
        }
        /// <summary>
        /// Returns the cotangent of a number
        /// </summary>
        public static Function Cot(double value)
        {
            return new(FunctionType.Cot, value);
        }
        /// <summary>
        /// Converts a value in radians to degrees
        /// </summary>
        public static Function Degrees(string columnName)
        {
            return new(FunctionType.Degrees, columnName);
        }
        /// <summary>
        /// Converts a value in radians to degrees
        /// </summary>
        public static Function Degrees(int value)
        {
            return new(FunctionType.Degrees, value);
        }
        /// <summary>
        /// Converts a value in radians to degrees
        /// </summary>
        public static Function Degrees(double value)
        {
            return new(FunctionType.Degrees, value);
        }
        /// <summary>
        /// Returns e raised to the power of a specified number
        /// </summary>
        public static Function Exp(string columnName)
        {
            return new(FunctionType.Exp, columnName);
        }
        /// <summary>
        /// Returns e raised to the power of a specified number
        /// </summary>
        public static Function Exp(int value)
        {
            return new(FunctionType.Exp, value);
        }
        /// <summary>
        /// Returns e raised to the power of a specified number
        /// </summary>
        public static Function Exp(double value)
        {
            return new(FunctionType.Exp, value);
        }
        /// <summary>
        /// Returns the largest integer value that is &lt;= to a number
        /// </summary>
        public static Function Floor(string columnName)
        {
            return new(FunctionType.Floor, columnName);
        }
        /// <summary>
        /// Returns the largest integer value that is &lt;= to a number
        /// </summary>
        public static Function Floor(double value)
        {
            return new(FunctionType.Floor, value);
        }
        /// <summary>
        /// Returns the natural logarithm of a number
        /// </summary>
        public static Function Log(string columnName)
        {
            return new(FunctionType.Log, columnName);
        }
        /// <summary>
        /// Returns the natural logarithm of a number
        /// </summary>
        public static Function Log(int value)
        {
            return new(FunctionType.Log, value);
        }
        /// <summary>
        /// Returns the natural logarithm of a number
        /// </summary>
        public static Function Log(double value)
        {
            return new(FunctionType.Log, value);
        }
        /// <summary>
        /// Returns the natural logarithm of a number to base 10
        /// </summary>
        public static Function Log10(string columnName)
        {
            return new(FunctionType.Log10, columnName);
        }
        /// <summary>
        /// Returns the natural logarithm of a number to base 10
        /// </summary>
        public static Function Log10(int value)
        {
            return new(FunctionType.Log10, value);
        }
        /// <summary>
        /// Returns the natural logarithm of a number to base 10
        /// </summary>
        public static Function Log10(double value)
        {
            return new(FunctionType.Log10, value);
        }
        /// <summary>
        /// Returns the largest value of the selected column
        /// </summary>
        public static Function Max(string columnName)
        {
            return new(FunctionType.Max, columnName);
        }
        /// <summary>
        /// Returns the smallest value of the selected column
        /// </summary>
        public static Function Min(string columnName)
        {
            return new(FunctionType.Min, columnName);
        }
        /// <summary>
        /// Returns the value of PI
        /// </summary>
        public static Function PI()
        {
            return new(FunctionType.Pi);
        }
        /// <summary>
        /// Returns the value of a number raised to the power of another number (x^y)
        /// </summary>
        public static Function Power(string columnName, int number)
        {
            return new(FunctionType.Power, columnName, number);
        }
        /// <summary>
        /// Converts a degree value into radians
        /// </summary>
        public static Function Radians(string columnName)
        {
            return new(FunctionType.Radians, columnName);
        }
        /// <summary>
        /// Converts a degree value into radians
        /// </summary>
        public static Function Radians(int value)
        {
            return new(FunctionType.Radians, value);
        }
        /// <summary>
        /// Converts a degree value into radians
        /// </summary>
        public static Function Radians(double value)
        {
            return new(FunctionType.Radians, value);
        }
        /// <summary>
        /// Returns a random number between 0 and 1
        /// </summary>
        public static Function Random()
        {
            return new(FunctionType.Random);
        }
        /// <summary>
        /// Returns a repeatable sequence of random numbers between 0 and 1
        /// </summary>
        public static Function Random(int seed)
        {
            return new(FunctionType.Random, seed);
        }
        /// <summary>
        /// Returns a random number between start and end
        /// </summary>
        public static Function Random(int start, int end)
        {
            return new(FunctionType.Random, start, end);
        }
        /// <summary>
        /// Rounds a number to a specified number of decimal places
        /// </summary>
        public static Function Round(string columnName, int decimals)
        {
            return new(FunctionType.Round, columnName, decimals);
        }
        /// <summary>
        /// Rounds a number to a specified number of decimal places
        /// </summary>
        public static Function Round(double value, int decimals)
        {
            return new(FunctionType.Round, value, decimals);
        }
        /// <summary>
        /// Returns the sign of a number
        /// (number &gt; 0 returns 1),
        /// (number = 0 returns 0) or
        /// (number &lt; 0 returns -1)
        /// </summary>
        public static Function Sign(string columnName)
        {
            return new(FunctionType.Sign, columnName);
        }
        /// <summary>
        /// Returns the sign of a number
        /// (number &gt; 0 returns 1),
        /// (number = 0 returns 0) or
        /// (number &lt; 0 returns -1)
        /// </summary>
        public static Function Sign(int value)
        {
            return new(FunctionType.Sign, value);
        }
        /// <summary>
        /// Returns the sign of a number
        /// (number &gt; 0 returns 1),
        /// (number = 0 returns 0) or
        /// (number &lt; 0 returns -1)
        /// </summary>
        public static Function Sign(double value)
        {
            return new(FunctionType.Sign, value);
        }
        /// <summary>
        /// Returns the sine of a number
        /// </summary>
        public static Function Sin(string columnName)
        {
            return new(FunctionType.Sin, columnName);
        }
        /// <summary>
        /// Returns the sine of a number
        /// </summary>
        public static Function Sin(int value)
        {
            return new(FunctionType.Sin, value);
        }
        /// <summary>
        /// Returns the sine of a number
        /// </summary>
        public static Function Sin(double value)
        {
            return new(FunctionType.Sin, value);
        }
        /// <summary>
        /// Returns the square root of a number
        /// </summary>
        public static Function SquareRoot(string columnName)
        {
            return new(FunctionType.SquareRoot, columnName);
        }
        /// <summary>
        /// Returns the square root of a number
        /// </summary>
        public static Function SquareRoot(int value)
        {
            return new(FunctionType.SquareRoot, value);
        }
        /// <summary>
        /// Returns the square root of a number
        /// </summary>
        public static Function SquareRoot(double value)
        {
            return new(FunctionType.SquareRoot, value);
        }
        /// <summary>
        /// returns the total sum of a numeric column
        /// </summary>
        public static Function Sum(string columnName)
        {
            return new(FunctionType.Sum, columnName);
        }
        /// <summary>
        /// Returns the tangent of a number
        /// </summary>
        public static Function Tan(string columnName)
        {
            return new(FunctionType.Tan, columnName);
        }
        /// <summary>
        /// Returns the tangent of a number
        /// </summary>
        public static Function Tan(int value)
        {
            return new(FunctionType.Tan, value);
        }
        /// <summary>
        /// Returns the tangent of a number
        /// </summary>
        public static Function Tan(double value)
        {
            return new(FunctionType.Tan, value);
        }
        #endregion

        #region DateTime Functions
        /// <summary>
        /// Returns the number of days between two date values
        /// </summary>
        public static Function DateDifference(string columnName, DateTime date)
        {
            return new(FunctionType.DateDifference, columnName, date.ToString());
        }
        /// <summary>
        /// Returns the number of days between two date values
        /// </summary>
        public static Function DateDifference(string columnName, string differentColumnName)
        {
            return new(FunctionType.DateDifferenceColumn, columnName, differentColumnName);
        }
        /// <summary>
        /// Returns the current date and time
        /// </summary>
        public static Function GetDate()
        {
            return new(FunctionType.GetDate);
        }
        /// <summary>
        /// Returns the day of the month for a specified date
        /// </summary>
        public static Function GetDay(string columnName)
        {
            return new(FunctionType.GetDay, columnName);
        }
        /// <summary>
        /// Returns the month part for a specified date
        /// </summary>
        public static Function GetMonth(string columnName)
        {
            return new(FunctionType.GetMonth, columnName);
        }
        /// <summary>
        /// Returns the year part for a specified date
        /// </summary>
        public static Function GetYear(string columnName)
        {
            return new(FunctionType.GetYear, columnName);
        }
        #endregion

        #region Advance Functions
        /// <summary>
        /// Return a specified value if the expression is NULL, otherwise return the expression
        /// </summary>
        public static Function IsNull(string columnName, string value)
        {
            return new(FunctionType.IsNull, columnName, value);
        }
        /// <summary>
        /// Return a specified value if the expression is NULL, otherwise return the expression
        /// </summary>
        public static Function IsNull(string columnName, int value)
        {
            return new(FunctionType.IsNull, columnName, value);
        }
        /// <summary>
        /// Return a specified value if the expression is NULL, otherwise return the expression
        /// </summary>
        public static Function IsNull(string columnName, double value)
        {
            return new(FunctionType.IsNull, columnName, value);
        }
        /// <summary>
        /// Return a Globally Unique Identifier
        /// </summary>
        public static Function NewGuid()
        {
            return new(FunctionType.NewGuid);
        }
        /// <summary>
        /// Compares two expressions and returns NULL if they are equal. Otherwise, the first expression is returned
        /// </summary>
        public static Function NullIf(string columnName, string expression)
        {
            return new(FunctionType.NullIf, columnName, expression);
        }
        /// <summary>
        /// Compares two expressions and returns NULL if they are equal. Otherwise, the first expression is returned
        /// </summary>
        public static Function NullIf(string columnName, int expression)
        {
            return new(FunctionType.NullIf, columnName, expression);
        }
        /// <summary>
        /// Compares two expressions and returns NULL if they are equal. Otherwise, the first expression is returned
        /// </summary>
        public static Function NullIf(string columnName, double expression)
        {
            return new(FunctionType.NullIf, columnName, expression);
        }
        #endregion
    }
}
