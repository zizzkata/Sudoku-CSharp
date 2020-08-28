using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Timers;
using System.Globalization;

namespace sudoku
{
    public partial class Sudoku : Form
    {
       
        public Board board;
        public CancellationTokenSource tokenSource;
        private Stopwatch watch = new Stopwatch();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        
        
        public Sudoku()
        {
            InitializeComponent();

            Control[] numbersVisual = new Control[81];
            Dictionary<string, Control> controls = new Dictionary<string, Control>();
            for (int i = 0; i < 81; i++)
            {
                numbersVisual[i] = this.Controls.Find('l' + i.ToString(), true)[0];
            }

            controls.Add("btnGenerate", this.Controls.Find("btnGenerate", true)[0]);
            controls.Add("btnSolve_V", this.Controls.Find("btnSolve_Visual", true)[0]);
            controls.Add("btnClear", this.Controls.Find("btnClear", true)[0]);
            controls.Add("btnCancel", this.Controls.Find("btnCancel", true)[0]);
            controls.Add("btnSolve_M", this.Controls.Find("btnSolve_Multi", true)[0]);
            controls.Add("lbTime", this.Controls.Find("lbTime", true)[0]);
            controls.Add("numHints", this.Controls.Find("numHints", true)[0]);
            controls.Add("numDeep", this.Controls.Find("numDeep", true)[0]);
            controls.Add("cbCross", this.Controls.Find("cbCross", true)[0]);

            board = new Board(numbersVisual, controls);


            timer.Tick += Timer_Tick;
            timer.Interval = 10;
            WruteTime(Color.Black);
           
        }

        
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            WruteTime(Color.Black);
            if(!cbCross.Checked)
            {
                board.ClearBoard();
                board.GenerateBoardRecursive((int)numHints.Value);
            }
            else
            {
                board.ClearBoard();
                board.GenerateBoardRecursive_CrossRule((int)numHints.Value);
            }
        }

        private async void btnSolve_Visual_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();

            StartTime();


            if(!cbCross.Checked)
            {
                if (!await Task.Run(() => board.SolveBoardRecursively_Async_Visual(tokenSource.Token), tokenSource.Token))
                {
                    StopTime();
                    WruteTime(Color.Red);
                    MessageBox.Show("Canceled or no solution has been found!");
                    board.ClearBoard();
                    board.VisualizeBoard();
                }
            }
            else
            {
                if (!await Task.Run(() => board.SolveBoardRecursively_Async_Cross_Visual(tokenSource.Token), tokenSource.Token))
                {
                    StopTime();
                    WruteTime(Color.Red);
                    MessageBox.Show("Canceled or no solution has been found!");
                    board.ClearBoard();
                    board.VisualizeBoard();
                }
            }

            StopTime();
            
            tokenSource.Cancel();
            tokenSource.Dispose();
        }

        private async void btnSolve_Multi_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            StartTime();

            if(!cbCross.Checked)
            {
                if(!await Task.Run(() => board.Solve_MultiThreading((int)numDeep.Value, tokenSource)))
                {
                    WruteTime(Color.Red);
                    StopTime();
                    
                    MessageBox.Show("Canceled or no solution has been found!");
                    board.ClearBoard();
                    board.VisualizeBoard();
                }
            }
            else
            {
                if(!await Task.Run(() => board.Solve_MultiThreading_Cross((int)numDeep.Value, tokenSource)))
                {
                    WruteTime(Color.Red);
                    StopTime();
                    
                    MessageBox.Show("Canceled or no solution has been found!");
                    board.ClearBoard();
                    board.VisualizeBoard();
                }
            }
            StopTime();
            tokenSource.Cancel();
            tokenSource.Dispose(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                tokenSource.Cancel();
            }
            catch(Exception)
            {

            }
            StopTime();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            board.ClearBoard();
            board.VisualizeBoard();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(cbCross.Checked)
            {
                board.PaintCorssSection(true);
            }
            else
            {
                board.PaintCorssSection(false);
            }
        }

        // timer
        private void StartTime()
        {
            watch.Start();
            timer.Start();
        }
        private void StopTime()
        {
            watch.Reset();
            timer.Stop();
        }
        private void WruteTime(Color clr)
        {
            lbTime.ForeColor = clr;
            lbTime.Text = "Time: " + ((double)watch.ElapsedMilliseconds / 1000).ToString("0.000") + " sec";
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            WruteTime(Color.Black);
        }

    }
}
