namespace LogClass;

public class Log
{
    string expression;

    public string Expression {
        get => this.expression;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException();

            this.expression = value;
        }
    
    }

    public DateTime ExpressionTime { get; set; }

    public Log(string expression, DateTime expressionTime)
    {
        Expression = expression;
        ExpressionTime = expressionTime;
    }

    public override string ToString() => ExpressionTime.ToShortDateString() + "  " + expression;
}
