using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    public delegate void Pass_Board(int[,] b);

    public class Board
    {
        //board[y][x]
        public int[,] board;

        private Controls ctr;
        private EnvModifier envModifier;
        private Solutions solutions;
        public Pass_Board pass_Board;
        public Board(Control[] numbers_Visual, Dictionary<string, Control> dictionary)
        {
            board = new int[9, 9];
            ctr = new Controls(numbers_Visual, dictionary);
            pass_Board = new Pass_Board(WriteBoard);
            solutions = new Solutions(ctr, pass_Board);
            envModifier = new EnvModifier();
           
        }
        
        public void GenerateBoardRecursive()
        {
            
            solutions.RecursiveSolution(board, 0, 0);
            ctr.VisualizeBoard(board);
        }

        public void GenerateBoardRecursive(int known_numbers)
        {
            ctr.EnableControls(false);
            //ClearBoard();
            solutions.RecursiveSolution(board, 0, 0);
            envModifier.ModifyTable(board, known_numbers);
            ctr.VisualizeBoard_Contrast(board);
            ctr.EnableControls(true);
        }
        public void GenerateBoardRecursive_CrossRule(int known_numbers)
        {
            ctr.EnableControls(false);
            //ClearBoard();
            solutions.RecursiveSolution_Cross(board, 0, 0);
            envModifier.ModifyTable(board, known_numbers);
            ctr.VisualizeBoard_Contrast(board);
            ctr.EnableControls(true);
        }
        public bool SolveBoardRecursively()
        {
            return solutions.RecursiveSolution(board, 0, 0);
            
        }
        public bool SolveBoardRecursively_Visual()
        {
            ctr.EnableControls(false);
            bool return_value =  solutions.RecursiveSolution_Visual(board, 0, 0);
            ctr.EnableControls(true);
            return return_value;
        }
        public bool SolveBoardRecursively_Cross_Visual()
        {
            ctr.EnableControls(false);
            bool return_value = solutions.RecursiveSolution_Cross_Visual(board, 0, 0);
            ctr.EnableControls(true);
            return return_value;
        }
        public bool SolveBoardRecursively_Async_Cross_Visual(CancellationToken token)
        {
            ctr.EnableControls(false);
            bool return_value = solutions.RecursiveSolution_Async_Cross_Visual(board, 0, 0, token);
            ctr.EnableControls(true);
            return return_value;
        }
        public bool Solve_MultiThreading(int deep_level, CancellationTokenSource tokenSource)
        {
            ctr.EnableControls(false);
            object[] results = solutions.MultiThreadingSolution_NE(deep_level, tokenSource, board);
            if (results == null)
            {
                ctr.EnableControls(true);
                return true;
            }
            foreach (object[] o in results)
            {
                if ((bool)o[0])
                {
                    board = (int[,])o[2];
                    ctr.VisualizeBoard((int[,])o[1], (int[,])o[2]);
                    ctr.EnableControls(true);
                    return true;
                }
            }
            ctr.EnableControls(true);
            return false;
        }
        public bool Solve_MultiThreading_Cross(int deep_level, CancellationTokenSource tokenSource)
        {
            ctr.EnableControls(false);
            object[] results = solutions.MultiThreadingSolution_Cross_NE(deep_level, tokenSource, board);
            if (results == null)
            {
                ctr.EnableControls(true);
                return true;
            }
            foreach (object[] o in results)
            {
                if ((bool)o[0])
                {
                    board = (int[,])o[2];
                    ctr.VisualizeBoard((int[,])o[1], (int[,])o[2]);
                    ctr.EnableControls(true);
                    return true;
                }
            }
            ctr.EnableControls(true);
            return false;

        }
        public bool SolveBoardRecursively_Async_Visual(CancellationToken token)
        {
            ctr.EnableControls(false);
            bool return_value = solutions.RecursiveSolution_Async_Visual(board, 0, 0, token);
            ctr.EnableControls(true);
            return return_value;
        }
        public void VisualizeBoard()
        {
            ctr.VisualizeBoard_Contrast(board);
        }
        public void VisualizeBoard_Message()
        {
            ctr.Print_outTable(board);
        }
        public void PaintCorssSection(bool enabled)
        {
            ctr.CrossMode(enabled);
        }
        public void WriteBoard(int[,] b)
        {
            ctr.VisualizeBoard(board, b);
            this.board = b;
        }
        public void ClearBoard()
        {
            for (int i = 0; i < 81; i++)
            {
                board[i / 9, i % 9] = 0;
            }
        }
    }
}
