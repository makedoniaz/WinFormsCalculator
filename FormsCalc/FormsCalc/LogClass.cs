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

    public DateTime ExpressionDate { get; set; }

    public Log(string expression)
    {
        Expression = expression;
    }
}
