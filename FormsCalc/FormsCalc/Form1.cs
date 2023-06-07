using System.Linq.Expressions;

namespace FormsCalc;


public enum BufferStatus
{
    Empty = 0,
    Full = 2,
} 

public partial class Form1 : Form
{
    private List<double> buffer = new List<double>(2);
    bool isFinalExpression = false;

    public Form1()
    {
        InitializeComponent();
    }

    private void ClearTextBox(TextBox textBox) => textBox.Text = string.Empty;

    private void ClearLabel(Label label) => label.Text = string.Empty;

    private void SetLastExpressionLabel (Button expressionButton)
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

    private void ShowFinalExpression() {
        this.ExpressionTextBox.Text = $"{buffer[0] + buffer[1]}";
        isFinalExpression = true;
        buffer.Clear();
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
            SetLastExpressionLabel(currentExpressionButton);
            ClearTextBox(this.ExpressionTextBox);

            if (buffer.Count == (int)BufferStatus.Full)
                ShowFinalExpression();
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