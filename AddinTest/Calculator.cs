using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddinTest
{
    /// <summary>The class for Calculator.</summary>
    public partial class Calculator : Form
    {
        private bool isInputMode;

        private double currentResult;
        private double currentNumber;
        private double preNumber;
        private string currentOp;
        private bool isDouble;
        private bool isFirstNumber;
        private bool isFirstDigit;
        private bool isOpEntered;
        private int floatDigit;

        /// <summary>constructor</summary>
        public Calculator()
        {
            InitializeComponent();

            isInputMode = false;

            Reset();
        }

        private void Reset()
        {
            textBoxInput.Text = "0";
            textBoxResult.Text = "0";

            currentResult = 0.0;
            currentNumber = 0.0;
            preNumber = 0.0;
            currentOp = "";
            isDouble = false;
            isFirstNumber = true;
            isFirstDigit = true;
            isOpEntered = false;
            floatDigit = 0;
        }

        private void handleOperator()
        {
            if (currentOp == "")
            {
                currentResult = currentNumber;
            }
            else if (currentOp == "+")
            {
                if (!isFirstNumber)
                {
                    currentResult -= preNumber;
                }
                currentResult += currentNumber;
            }
            else if (currentOp == "-")
            {
                if (!isFirstNumber)
                {
                    currentResult += preNumber;
                }
                currentResult -= currentNumber;
            }
            else if (currentOp == "*")
            {
                if (isFirstNumber)
                {
                    currentResult = currentNumber;
                }
                else
                {
                    if (preNumber != 0.0)
                    {
                        currentResult /= preNumber;
                    }
                    currentResult *= currentNumber;
                }
            }
            else if (currentOp == "/")
            {
                if (isFirstNumber)
                {
                    currentResult = currentNumber;
                }
                else
                {
                    if (preNumber != 0.0)
                    {
                        currentResult *= preNumber;
                    }
                    currentResult /= currentNumber;
                }
            }
        }

        private void OnNumberEnter(object sender, EventArgs e)
        {
            isOpEntered = false;

            string number = ((Button)sender).Text;
            if (isFirstDigit)
            {
                textBoxInput.Text = number;
                isFirstDigit = false;
            }
            else
            {
                textBoxInput.Text += number;
            }

            preNumber = currentNumber;
            if (floatDigit == 0)
            {
                if (number == "00")
                {
                    currentNumber = currentNumber * 100.0;
                }
                else
                {
                    currentNumber = currentNumber * 10.0 + Convert.ToDouble(number);
                }
            }
            else
            {
                if (number == "00")
                {
                    floatDigit += 2;
                }
                else
                {
                    currentNumber = currentNumber + Convert.ToDouble(number) * Math.Pow(0.1, floatDigit);
                    floatDigit++;
                }
            }

            handleOperator();

            if (isDouble)
            {
                textBoxResult.Text = currentResult.ToString();
            }
            else
            {
                textBoxResult.Text = ((int)currentResult).ToString();
            }
        }

        private void OnOperatorEnter(object sender, EventArgs e)
        {
            if (!isOpEntered)
            {
                currentOp = ((Button)sender).Text;
                textBoxInput.Text += currentOp;

                currentNumber = 0.0;
                floatDigit = 0;
                isFirstNumber = false;
                isOpEntered = true;
            }
        }

        private void OnDotEnter(object sender, EventArgs e)
        {
            if (floatDigit == 0)
            {
                textBoxInput.Text += ((Button)sender).Text;
                isDouble = true;
                isFirstDigit = false;
                floatDigit = 1;
            }
        }

        private void OnClearClick(object sender, MouseEventArgs e)
        {
            Reset();
        }
    }
}
