using System.Collections.Specialized;
using System.Linq.Expressions;

namespace FormsCalc;

public partial class Form1 : Form
{
    private List<double> numberBuffer = new List<double>(2);
    private Func<double, double, double> BinaryOperation = null;

    bool isAddMode = true;
    bool isFinalExpression = false;

    public Form1()
    {
        InitializeComponent();
    }


    private void AddNumberInBuffer(string numberStr)
    {
        if (double.TryParse(numberStr, out double number) == false)
            throw new FormatException("Invalid number!");

        this.numberBuffer.Add(number);
    }


    private double Sum(double num1, double num2) => num1 + num2;

    private double Minus(double num1, double num2) => num1 - num2;

    private double Multiply(double num1, double num2) => num1 * num2;

    private double Divide(double num1, double num2)
    {
        if (num2 == 0)
            throw new DivideByZeroException();

        return num1 / num2;
    }

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
            if (this.isAddMode == false)
            {
                this.ExpressionTextBox.Text = string.Empty;
                this.isAddMode = true;
            }

            if (this.isFinalExpression)
                ClearButton_Click(sender, e);

            this.ExpressionTextBox.Text += currentButton.Text;
        }
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        if (sender is Button)
        {
            this.ExpressionTextBox.Text = string.Empty;
            this.LastExpressionLabel.Text = string.Empty;
            this.BinaryOperation = null;
            this.isAddMode = true;
            this.numberBuffer.Clear();
        }
    }

    private void ExpressionButton_Click(object sender, EventArgs e)
    {
        if (sender is Button currentButton)
        {
            this.isFinalExpression = false;

            AddNumberInBuffer(this.ExpressionTextBox.Text);

            if (this.numberBuffer.Count == (int)BufferStatus.Processing)
                SetExpressionLogic(currentButton);

            if (this.numberBuffer.Count == (int)BufferStatus.Full)
            {
                this.ExpressionTextBox.Text = $"{this.BinaryOperation.Invoke(numberBuffer[0], numberBuffer[1])}";
                this.numberBuffer.Clear();

                AddNumberInBuffer(this.ExpressionTextBox.Text);
                SetExpressionLogic(currentButton);
            }

            this.LastExpressionLabel.Text = $"{this.ExpressionTextBox.Text}{currentButton.Text}";
            this.isAddMode = false;
        }
    }

    private void EqualsButton_Click(object sender, EventArgs e)
    {

        if (sender is Button currentButton)
        {
            if (this.numberBuffer.Count == (int)BufferStatus.Processing)
            {
                this.LastExpressionLabel.Text += this.ExpressionTextBox.Text + currentButton.Text;
                AddNumberInBuffer(this.ExpressionTextBox.Text);
                this.ExpressionTextBox.Text = $"{numberBuffer[0] + numberBuffer[1]}";
                this.isAddMode = false;
                this.numberBuffer.Clear();
                return;
            }

            this.LastExpressionLabel.Text = this.ExpressionTextBox.Text + currentButton.Text;
            this.isAddMode = false;
            this.isFinalExpression = true;
        }

    }
}