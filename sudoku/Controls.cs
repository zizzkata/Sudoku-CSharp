using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace sudoku
{
    public class Controls
    {
        Control[] numbersVisual;
        Dictionary<string, Control> controls;
        private delegate void SafeCallNumber(int y, int x, Color color, int number);
        private delegate void _SafeCallNumber(int y, int x, Color color);
        private delegate void SafeCallControl(string control, bool enable);

        public Controls(Control[] numbers, Dictionary<string, Control> dictonary)
        {
            numbersVisual = numbers;
            controls = dictonary;
        }
        public void Print_outTable(int[,] board)
        {
            string table = "";
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    table += board[i, j];

                    if (j % 3 == 2)
                    {
                        table += " ";
                        if (j == 8)
                            table += '\r';
                    }
                }
            }
            MessageBox.Show(table);
        }
        public void VisualizeNumber(int y, int x, Color color)
        {
            int pos = (y * 9) + x;

            numbersVisual[pos].ForeColor = color;
            numbersVisual[pos].Refresh();
        }
        public void VisualizeNumber(int y, int x, Color color, int number)
        {
            int pos = (y * 9) + x;

            numbersVisual[pos].Text = number.ToString();
            numbersVisual[pos].ForeColor = color;
            numbersVisual[pos].Refresh();
        }
        public void VisualizeNumber_Invoke(int y, int x, Color color, int number)
        {
            int pos = (y * 9) + x;

            if (numbersVisual[pos].InvokeRequired)
            {
                var d = new SafeCallNumber(VisualizeNumber_Invoke);
                numbersVisual[pos].Invoke(d, new object[] { y, x, color, number });
            }
            else
            {
                numbersVisual[pos].Text = number.ToString();
                numbersVisual[pos].ForeColor = color;
                numbersVisual[pos].Refresh();
            }
            
        }
        public void EnableControls_Invoke(string control, bool enable)
        {
            if (controls[control].InvokeRequired)
            {
                var d = new SafeCallControl(EnableControls_Invoke);
                controls[control].Invoke(d, new object[] { control, enable });
            }
            else
            {
                controls[control].Enabled = enable;
            }

        }
        public void VisualizeBackColorNumber_Invoke(int x, int y, Color color)
        {
            int pos = (y * 9) + x;
            if (numbersVisual[pos].InvokeRequired)
            {
                var d = new _SafeCallNumber(VisualizeBackColorNumber_Invoke);
                numbersVisual[pos].Invoke(d, new object[] { x , y, color });
            }
            else
            {
                numbersVisual[pos].BackColor = color;
                numbersVisual[pos].Refresh();
            }

        }
        public void VisualizeNumber_Invoke(int y, int x, Color color)
        {
            int pos = (y * 9) + x;

            if (numbersVisual[pos].InvokeRequired)
            {
                var d = new _SafeCallNumber(VisualizeNumber_Invoke);
                numbersVisual[pos].Invoke(d, new object[] { y, x, color});
            }
            else
            {
                numbersVisual[pos].ForeColor = color;
                numbersVisual[pos].Refresh();
            }

        }
        public void VisualizeNumber(int pos, Color color, int number)
        {
            
            numbersVisual[pos].Text = number.ToString();
            numbersVisual[pos].ForeColor = color;
            numbersVisual[pos].Refresh();
        }
        public string GetNumber(int y, int x)
        {
            return  numbersVisual[(y * 9) + x].Text;
        }
        public void VisualizeBoard(int[,] board)
        {
            for (int i = 0; i < 81; i++)
            {
                int num = board[i / 9, i % 9];
                if (num == 0)
                {
                    VisualizeNumber_Invoke(i / 9, i % 9, Color.Silver, num);
                }
                else
                {
                    VisualizeNumber_Invoke(i / 9, i % 9, Color.Black, num);
                }
            }
            
        }
        public void VisualizeBoard_Contrast(int[,] board)
        {
            for (int i = 0; i < 81; i++)
            {
                int num = board[i / 9, i % 9];
                if (num == 0)
                {
                    VisualizeNumber(i, Color.Silver, num);
                }
                else
                {
                    VisualizeNumber(i, Color.MediumBlue, num);
                }
            }

        }

        public void EnableControls(bool enable)
        {
            
            
            EnableControls_Invoke("btnGenerate", enable);
            EnableControls_Invoke("btnSolve_V", enable);
            EnableControls_Invoke("btnSolve_M", enable);
            EnableControls_Invoke("btnClear", enable);
            //EnableControls_Invoke("btnCancel", enable);
            EnableControls_Invoke("numHints", enable);
            EnableControls_Invoke("numDeep", enable);
            EnableControls_Invoke("cbCross", enable);
        }
        public void CrossMode(bool enable)
        {
            if(!enable)
            {
                for (int i = 0; i < 9; i++)
                {
                    VisualizeBackColorNumber_Invoke(i, i, Color.Transparent);
                    VisualizeBackColorNumber_Invoke((i - 8) * (-1), i, Color.Transparent);
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    VisualizeBackColorNumber_Invoke(i, i, Color.Aquamarine);
                    VisualizeBackColorNumber_Invoke((i - 8) * (-1), i, Color.Aquamarine);
                }
            }
        }
        public void VisualizeBoard(int[,] template, int[,] board)
        {
            
            for (int i = 0; i < 81; i++)
            {
                int y = i / 9;
                int x = i % 9;
                if (template[y, x] != 0)
                {
                    VisualizeNumber_Invoke(y, x, Color.MediumBlue,board[y,x]);
                }
                else
                {
                    VisualizeNumber_Invoke(y, x, Color.Black, board[y,x]);
                }
            }
        }
        

    }
}
