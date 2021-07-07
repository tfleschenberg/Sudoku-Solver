using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    public partial class Form1 : Form
    {
        private int[,] array = new int[9, 9];
        private TextBox[,] TextBoxArray = new TextBox[9, 9];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateForm(12, 12, 23, 22, 6);
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            TextBoxArrayToArray();
            if (Backtrack(array, 0, 0)) ArrayToTextBoxArray();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBoxArray[j, i].Text = String.Empty;
                }
            }
        }

        private void GenerateForm(int x, int y, int width, int height, int space)
        {
            Width = (Width - ClientSize.Width) + 2 * x + 9 * width + 2 * space;
            Height = (Height - ClientSize.Height) + 2 * y + 9 * height + 4 * space + SolveButton.Height;

            GenerateTextboxArray(x, y, width, height, space);
        }

        private void GenerateTextboxArray(int x, int y, int width, int height, int space)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBoxArray[j, i] = new TextBox
                    {
                        Size = new Size(width, height),
                        Location = new Point(x + j * width + space * (j / 3), y + i * height + space * (i / 3)),
                        MaxLength = 1,
                        TabIndex = j + 9 * i,
                        TextAlign = HorizontalAlignment.Center
                    };
                    TextBoxArray[j, i].Enter += new System.EventHandler(this.SelectAllInTextbox);
                    TextBoxArray[j, i].Click += new System.EventHandler(this.SelectAllInTextbox);
                    TextBoxArray[j, i].TextChanged += new System.EventHandler(this.TextboxTextChanged);
                    Controls.Add(TextBoxArray[j, i]);
                }
            }
        }

        private void TextBoxArrayToArray()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    switch (TextBoxArray[j, i].Text)
                    {
                        case "1": array[j, i] = 1; break;
                        case "2": array[j, i] = 2; break;
                        case "3": array[j, i] = 3; break;
                        case "4": array[j, i] = 4; break;
                        case "5": array[j, i] = 5; break;
                        case "6": array[j, i] = 6; break;
                        case "7": array[j, i] = 7; break;
                        case "8": array[j, i] = 8; break;
                        case "9": array[j, i] = 9; break;
                        default: array[j, i] = 0; break;
                    }
                }
            }
        }

        private void ArrayToTextBoxArray()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBoxArray[j, i].Text = array[j, i].ToString();
                }
            }
        }

        private bool Backtrack(int[,] array, int x, int y)
        {
            if (x == 9)
            {
                x = 0;
                y++;
            }
            if (y == 9)
            {
                return true;
            }

            if (array[x, y] == 0)
            {
                for (array[x, y] = 1; array[x, y] < 10; array[x, y]++)
                {
                    if (CheckCollision(array, x, y)) continue;
                    if (Backtrack(array, x + 1, y)) return true;
                }
                array[x, y] = 0;
                return false;
            }
            else
            {
                if (CheckCollision(array, x, y)) return false;
                return (Backtrack(array, x + 1, y));
            }
        }

        private bool CheckComplete()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (array[j, i] == 0) return false;
                }
            }

            return true;
        }

        private bool CheckCollision(int[,] array, int x, int y)
        {
            if (CheckRow(array, x, y)) return true;
            if (CheckColumn(array, x, y)) return true;
            if (CheckSquare(array, x, y)) return true;

            return false;
        }

        private bool CheckRow(int[,] array, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == x) continue;

                if (array[i, y] == array[x, y]) return true;
            }

            return false;
        }

        private bool CheckColumn(int[,] array, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == y) continue;

                if (array[x, i] == array[x, y]) return true;
            }

            return false;
        }

        private bool CheckSquare(int[,] array, int x, int y)
        {
            for (int i = 3 * (y / 3); i < 3 + 3 * (y / 3); i++)
            {
                for (int j = 3 * (x / 3); j < 3 + 3 * (x / 3); j++)
                {
                    if ((i == y) && (j == x)) continue;

                    if (array[j, i] == array[x, y]) return true;
                }
            }

            return false;
        }

        private void SelectAllInTextbox(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void TextboxTextChanged(object sender, EventArgs e)
        {
            switch (((TextBox)sender).Text)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    SelectNextControl((Control)sender, true, true, true, true);
                    break;
                default:
                    ((TextBox)sender).Text = String.Empty;
                    break;
            }
        }
    }
}
