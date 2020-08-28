using System;
using System.Collections.Generic;
using System.Text;

namespace sudoku
{
    public class EnvModifier
    {
        private Random random = new Random();
        public void ModifyTable(int[,] board, int leftOut_numbers)
        {
            int leftOut = 81 - leftOut_numbers;

            for (int i = 0; i < leftOut; i++)
            {
                int pos = random.Next(0, 81);
                if(board[pos/9, pos%9] != 0)
                {
                    board[pos/9, pos%9] = 0;
                }
                else
                {
                    i--;
                }
            }
        }
    }
}
