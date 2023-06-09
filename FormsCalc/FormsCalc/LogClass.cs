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

    public override string ToString() => ExpressionTime.ToString() + "\t" + expression;

    public Log(string expression, DateTime expressionTime)
    {
        Expression = expression;
        ExpressionTime = expressionTime;
    }
}
