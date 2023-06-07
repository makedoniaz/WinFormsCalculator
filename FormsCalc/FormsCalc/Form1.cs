using System.Linq.Expressions;

namespace FormsCalc;


public enum BufferStatus
{
    Empty = 0,
    Processing = 1,
    Full = 2,
}

public partial class Form1 : Form
{
    private List<double> buffer = new List<double>(2);
    private bool isFinalExpression = false;
    private Func<double, double, double> BinaryOperation = null;

    public Form1()
    {
        InitializeComponent();
    }

    private void ClearTextBox(TextBox textBox) => textBox.Text = string.Empty;

    private void ClearLabel(Label label) => label.Text = string.Empty;

    private void SetLastExpressionLabel(Button expressionButton)
    {
        string symbol = (buffer.Count == (int)BufferStatus.Full) ? this.EqualsButton.Text : expressionButton.Text;
        this.LastExpressionLabel.Text += this.ExpressionTextBox.Text + symbol;
    }

    private void AddNumberInBuffer(string numberStr)
    {
        if (double.TryParse(this.ExpressionTextBox.Text, out double num) == false)
            throw new Exception();

        buffer.Add(num);
    }

    private void ShowFinalExpression(Func<double, double, double> BinaryOperation)
    {
        this.ExpressionTextBox.Text = $"{BinaryOperation.Invoke(buffer[0], buffer[1])}";
        isFinalExpression = true;
        buffer.Clear();
    }

    private double Sum(double num1, double num2) => num1 + num2;

    private double Minus(double num1, double num2) => num1 - num2;

    private double Multiply(double num1, double num2) => num1 * num2;

    private double Divide(double num1, double num2) => num1 / num2;

    private void SetExpressionLogic(Button expressionButton)
    {
        switch (expressionButton.Text)
        {
            case "+":
                this.BinaryOperation = Sum;
                break;
            case "-":
                this.BinaryOperation = Minus;
                break;
            case "×":
                this.BinaryOperation = Multiply;
                break;
            case "÷":
                this.BinaryOperation = Divide;
                break;

        }
    }

    private void NumberButton_Click(object sender, EventArgs e)
    {
        if (sender is Button currentButton)
        {
            if (this.isFinalExpression)
            {
                ClearTextBox(this.ExpressionTextBox);
                ClearLabel(this.LastExpressionLabel);
                this.isFinalExpression = false;
            }

            this.ExpressionTextBox.Text += currentButton.Text;
        }
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        if (sender is Button currentNumberButton)
        {
            ClearTextBox(this.ExpressionTextBox);
            ClearLabel(this.LastExpressionLabel);
            buffer.Clear();
        }
    }

    private void ExpressionButton_Click(object sender, EventArgs e)
    {
        if (sender is Button currentExpressionButton)
        {

            isFinalExpression = false;

            if (buffer.Count == (int)BufferStatus.Empty)
                ClearLabel(this.LastExpressionLabel);


            AddNumberInBuffer(this.ExpressionTextBox.Text);

            if (buffer.Count == (int)BufferStatus.Processing)
            {
                SetExpressionLogic(currentExpressionButton);
            }

            SetLastExpressionLabel(currentExpressionButton);
            ClearTextBox(this.ExpressionTextBox);

            if (buffer.Count == (int)BufferStatus.Full)
                ShowFinalExpression(this.BinaryOperation);
        }
    }

    private void EqualsButton_Click(object sender, EventArgs e)
    {
        if (this.ExpressionTextBox.Text == string.Empty && this.LastExpressionLabel.Text != string.Empty)
        {
            this.ExpressionTextBox.Text = $"{buffer[0]}";
            ClearLabel(LastExpressionLabel);
            buffer.Clear();
        }

        else if (this.ExpressionTextBox.Text != string.Empty && this.LastExpressionLabel.Text != string.Empty &&
            isFinalExpression == false)
            ExpressionButton_Click(sender, EventArgs.Empty);

        this.isFinalExpression = true;
    }
}