namespace Endahl.CSharpedSql
{
    using Endahl.CSharpedSql.Base;
    using System.Collections.Generic;

    public class Case: ColumnItem
    {
        public virtual List<Case> Whens { get; }
        public virtual object ElseResult { get; set; }

        public virtual Condition Condition { get; }
        public virtual object Result { get; }

        protected Case(Condition condition, object result)
        {
            Name = $"CASE WHEN {condition.Column} {condition.ConditionType.ToString()} {result.ToString()} END";
            Whens = new List<Case>();
            Condition = condition;
            Result = result;
        }

        /// <summary>
        /// Returns the <see cref="Case"/> as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        /// <summary>
        /// Returns the <see cref="Case"/> as a string.
        /// </summary>
        public override string ToString(SqlOptions sql)
        {
            return sql.SqlBase.Case(this, sql);
        }

        public virtual Case ElseIf(Condition condition, object result)
        {
            if (condition == null)
                throw new System.ArgumentNullException("condition");
            Whens.Add(new Case(condition, result));
            return this;
        }

        public virtual void Else(object result)
        {
            ElseResult = result;
        }

        public virtual Case As(string name)
        {
            Name = name;
            return this;
        }

        public static Case operator +(Case @case, Case when)
        {
            @case.ElseIf(when.Condition, when.Result);
            @case.Whens.AddRange(when.Whens);
            return @case;
        }

        public static Case IF(Condition condition, object result)
        {
            if (condition == null)
                throw new System.ArgumentNullException("condition");
            return new Case(condition, result);
        }
    }
}
