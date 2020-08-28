using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace sudoku
{
    public class EnvChecker
    {
      
        public List<int> GetPossibleNumbers(int[,] board, int y, int x)
        {
            // new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            List<int> possible_numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int[] relation_numbers = GetNumbersInRelation(board, y, x);
            for (int i = 0; i < 9; i++)
            {
                if(relation_numbers[i] != 0)
                {
                    possible_numbers[i] = 0;
                }
            }

            possible_numbers.RemoveAll(i => i == 0);

            return possible_numbers;
        }
        public List<int> GetPossibleNumbers_Cross(int[,] board, int y, int x)
        {
            // new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            List<int> possible_numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int[] relation_numbers = GetNumbersInRelation_Diagonal(board, y, x);
            for (int i = 0; i < 9; i++)
            {
                if (relation_numbers[i] != 0)
                {
                    possible_numbers[i] = 0;
                }
            }

            possible_numbers.RemoveAll(i => i == 0);

            return possible_numbers;
        }
        public int[] NextCoordinates(int[,] board, int cur_pos_y, int cur_pos_x)
        {
            int[] coordinates = { cur_pos_y, cur_pos_x };
            

            switch (cur_pos_x)
            {
                default:
                    coordinates[1]++;
                    break;
                case 8:
                    coordinates[0]++;
                    coordinates[1] = 0;
                    break;
            }

            if (coordinates[0] == 9 && coordinates[1] == 0)
                return coordinates;
            if (board[coordinates[0], coordinates[1]] == 0)
                return coordinates;
            else
                return NextCoordinates(board, coordinates[0], coordinates[1]);
        }
        public int[] NextCoordinates(int cur_pos_y, int cur_pos_x)
        {
            int[] coordinates = { cur_pos_y, cur_pos_x };
            switch (cur_pos_x)
            {
                default:
                    coordinates[1]++;
                    return coordinates;
                case 8:
                    coordinates[0]++;
                    coordinates[1] = 0;
                    return coordinates;
            }
        }
        private int[] GetNumbersInRelation(int[,] board, int y, int x)
        {
            int[] numbers = new int[9];

            // gets the numbers horizontally
            for(int i = 0; i < 9; i++)
            {
                if(board[i, x] != 0)
                {
                    numbers[board[i, x] - 1] = board[i, x];
                }
            }
            // gets the numbers vertically
            for (int i = 0; i < 9; i++)
            {
                if (board[y, i] != 0)
                {
                    numbers[board[y, i] - 1] = board[y, i];
                }
            }


            // gets the numbers inside its own cube
            int[] coordinates = CoordinatesFixation(y,x);
            int max_bound_y = coordinates[0] + 3;
            int max_bound_x = coordinates[1] + 3;

            while (coordinates[0] < max_bound_y)
            {
                while (coordinates[1] < max_bound_x)
                {
                    if(board[coordinates[0],coordinates[1]] != 0)
                    {
                        numbers[board[coordinates[0], coordinates[1]] - 1] = board[coordinates[0], coordinates[1]];
                    }

                    coordinates[1]++;
                }
                coordinates[1] -= 3;
                coordinates[0]++;
            }

            return numbers;
        }
        private int[] GetNumbersInRelation_Diagonal(int[,] board, int y, int x)
        {
            int[] numbers = new int[9];

            // gets the numbers horizontally
            for (int i = 0; i < 9; i++)
            {
                if (board[i, x] != 0)
                {
                    numbers[board[i, x] - 1] = board[i, x];
                }
            }
            // gets the numbers vertically
            for (int i = 0; i < 9; i++)
            {
                if (board[y, i] != 0)
                {
                    numbers[board[y, i] - 1] = board[y, i];
                }
            }

            // gets diagonal
            if(y==x)
            {
                for(int i = 0; i < 9; i++)
                {
                    if(board[i, i] != 0)
                    {
                        numbers[board[i, i] - 1] = board[i, i];
                    }
                }
            }
            if(((y - 8)*(-1)) == x)
            {
                for (int i = 0; i < 9; i++)
                {
                    int _y = (i - 8) * (-1);
                    //int _x = i;
                    if (board[_y, i] != 0)
                    {
                        numbers[board[_y, i] - 1] = board[_y, i];
                    }
                }
            }




            // gets the numbers inside its own cube
            int[] coordinates = CoordinatesFixation(y, x);
            int max_bound_y = coordinates[0] + 3;
            int max_bound_x = coordinates[1] + 3;

            while (coordinates[0] < max_bound_y)
            {
                while (coordinates[1] < max_bound_x)
                {
                    if (board[coordinates[0], coordinates[1]] != 0)
                    {
                        numbers[board[coordinates[0], coordinates[1]] - 1] = board[coordinates[0], coordinates[1]];
                    }

                    coordinates[1]++;
                }
                coordinates[1] -= 3;
                coordinates[0]++;
            }

            return numbers;
        }
        private int[] CoordinatesFixation(int y, int x)
        {
            int[] start_coordinates = { y, x };

            for(int i = 0; i < 2; i++)
            {
                switch (start_coordinates[i] % 3)
                {
                    //case 0: // 1st coll/row of the box
                        //do nothing
                        //break;
                    case 1: // 2nd coll/row of the box
                        start_coordinates[i]--;
                        break;
                    case 2: // 3rd coll/row of the box
                        start_coordinates[i] -= 2;
                        break;
                }
            }
            return start_coordinates;
        }

        
    }

}
