namespace Endahl.CSharpedSql
{
    using System.Collections.Generic;

    public class Case: ColumnItem
    {
        protected virtual List<Case> Whens { get; }
        protected virtual object ElseResult { get; set; }

        public virtual Condition Condition { get; }
        public virtual object Result { get; }

        protected Case(Condition condition, object result)
        {
            Name = $"CASE WHEN {condition.Column} {condition.ConditionType.ToString()} {result.ToString()} END";
            Whens = new List<Case>();
            Condition = condition;
            Result = result;
        }

        public override string ToString()
        {
            return ToString(new SqlOptions());
        }
        public override string ToString(SqlOptions sql)
        {
            var @case = $"CASE WHEN {Condition.ToString(sql)} THEN {sql.CreateItemID(Result)} ";
            foreach (var when in Whens)
            {
                @case += $"WHEN {when.Condition.ToString(sql)} THEN {sql.CreateItemID(when.Result)} ";
            }
            if (ElseResult != null)
                @case += $"ELSE {sql.CreateItemID(ElseResult)} ";
            return @case + "END";
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
