using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography.Pkcs;

namespace sudoku
{
    public class Solutions
    { 
        private Controls ctr;
        private Random random = new Random();
        private EnvChecker envCheck = new EnvChecker();
        Pass_Board pass_Board;
        public Solutions(Controls ctr, Pass_Board pass_Board)
        {
            this.ctr = ctr;
            this.pass_Board = pass_Board;
        }

        public bool RecursiveSolution(int[,] board, int y, int x)
        {
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution(board, next_coordinates[0], next_coordinates[1]))
                    return true;

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Cross(int[,] board, int y, int x)
        {
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers_Cross(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution(board, next_coordinates[0], next_coordinates[1]))
                    return true;

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Cross_Visual(int[,] board, int y, int x)
        {
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers_Cross(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                ctr.VisualizeNumber_Invoke(y, x, Color.Black, board[y, x]); // Visual

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Cross_Visual(board, next_coordinates[0], next_coordinates[1]))
                    return true;

                ctr.VisualizeNumber_Invoke(y, x, Color.Red); // Visual

                board[y, x] = 0;
            }
            return false;
        }

        public bool RecursiveSolution_Visual(int[,] board, int y, int x)
        {

            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                ctr.VisualizeNumber_Invoke(y, x, Color.Black, board[y, x]); // Visual

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Visual(board, next_coordinates[0], next_coordinates[1]))
                    return true;

                ctr.VisualizeNumber_Invoke(y, x, Color.Red); // Visual
                board[y, x] = 0;
            }
            return false;
        }

        public bool RecursiveSolution_Async(int[,] board, int y, int x, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException(token); // takes less toll on the processor
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Async(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Async_NE(int[,] board, int y, int x, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return false;
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Async_NE(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Async_NE_Visual(int[,] board, int y, int x, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return false;
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                ctr.VisualizeNumber_Invoke(y, x, Color.Black, board[y, x]); // Visual

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Async_NE_Visual(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                ctr.VisualizeNumber_Invoke(y, x, Color.Red); // Visual

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Async_Visual(int[,] board, int y, int x, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return false;
               
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                ctr.VisualizeNumber_Invoke(y, x, Color.Black, board[y, x]); // Visual

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Async_Visual(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                ctr.VisualizeNumber_Invoke(y, x, Color.Red); // Visual

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Async_Cross_Visual(int[,] board, int y, int x, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return false;

            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers_Cross(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                ctr.VisualizeNumber_Invoke(y, x, Color.Black, board[y, x]); // Visual

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Async_Cross_Visual(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                ctr.VisualizeNumber_Invoke(y, x, Color.Red); // Visual

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Cross_Async(int[,] board, int y, int x, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException(token); // takes less toll on the processor
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers_Cross(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Cross_Async(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Cross_Async_NE(int[,] board, int y, int x, CancellationToken token)
        {

            if (token.IsCancellationRequested)
                return false;
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers_Cross(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Cross_Async_NE(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Cross_Async_NE_Visual(int[,] board, int y, int x, CancellationToken token)
        {

            if (token.IsCancellationRequested)
                return false;
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers_Cross(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];
                possible_numbers.RemoveAt(cur_pos);

                ctr.VisualizeNumber_Invoke(y, x, Color.Black, board[y, x]); // Visual

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Cross_Async_NE_Visual(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                ctr.VisualizeNumber_Invoke(y, x, Color.Red); // Visual

                board[y, x] = 0;
            }
            return false;
        }
        public bool RecursiveSolution_Cross_Async_Visual(int[,] board, int y, int x, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException(token); // takes less toll on the processor
            while (board[y, x] != 0)
            {
                int[] _next_coordinates = envCheck.NextCoordinates(y, x);
                if (_next_coordinates[0] == 9)
                    return true;
                y = _next_coordinates[0];
                x = _next_coordinates[1];
            }
            int[] next_coordinates = envCheck.NextCoordinates(y, x);
            List<int> possible_numbers = envCheck.GetPossibleNumbers_Cross(board, y, x);

            while (possible_numbers.Count > 0)
            {
                int cur_pos = random.Next(0, possible_numbers.Count);
                board[y, x] = possible_numbers[cur_pos];

                ctr.VisualizeNumber_Invoke(y, x, Color.Black, board[y, x]); // Visual

                possible_numbers.RemoveAt(cur_pos);

                if (next_coordinates[0] == 9)
                    return true;
                if (RecursiveSolution_Cross_Async_Visual(board, next_coordinates[0], next_coordinates[1], token))
                    return true;

                ctr.VisualizeNumber_Invoke(y, x, Color.Red); // Visual

                board[y, x] = 0;
            }
            return false;
        }

        
        /////////////////////////////////////////////////////////////////////////////////
        
        public void MultiThreadingRecursiveSolution(int deep_level, List<int[,]> boards, EnvChecker env)
        {
            if (deep_level < 1)
            {

                CancellationTokenSource tokenSource = new CancellationTokenSource();
                List<Task> workers = new List<Task>();

                foreach (int[,] board in boards)
                {
                    CancellationToken __token = tokenSource.Token;
                    if (__token.IsCancellationRequested)
                        break;
                    Task t = Task.Run(() => Worker(board, tokenSource, __token), __token);
                    workers.Add(t);
                }

                try
                {
                    Task.WhenAll(workers.ToArray());
                }
                catch (OperationCanceledException)
                {
                    Debug.Print($"\n {nameof(OperationCanceledException)} thrown");
                }
                finally
                {
                    //tokenSource.Dispose(); // cannot be properly disposed ---> let the garbage collector dispose it later
                }
            }
            else
            {
                List<int[,]> next_gen_boards = new List<int[,]>();

                foreach (int[,] _board in boards)
                {
                    int[] coordinates = { 0, 0 };
                    while (_board[coordinates[0], coordinates[1]] != 0)
                    {
                        coordinates = env.NextCoordinates(coordinates[0], coordinates[1]);
                    }

                    List<int> possible_numbers = env.GetPossibleNumbers(_board, coordinates[0], coordinates[1]);

                    foreach (int num in possible_numbers)
                    {
                        int[,] __board = (int[,])_board.Clone();
                        __board[coordinates[0], coordinates[1]] = num;
                        next_gen_boards.Add(__board);
                    }
                }

                boards.Clear(); // clear some ram
                MultiThreadingRecursiveSolution(--deep_level, next_gen_boards, env);
            }
        }
        public void MultiThreadingSolution_Cross(int deep_level, CancellationTokenSource tokenSource, int[,] board_original)
        {
            EnvChecker env = new EnvChecker();
            List<int[,]> boards = new List<int[,]>() { board_original };
            while (deep_level >= 1)
            {
                List<int[,]> next_boards = new List<int[,]>();

                foreach (int[,] _board in boards)
                {
                    int[] coordinates = { 0, 0 };
                    while (_board[coordinates[0], coordinates[1]] != 0)
                    {
                        coordinates = env.NextCoordinates(coordinates[0], coordinates[1]);
                        if (coordinates[0] == 9)
                            return;
                    }

                    List<int> possible_numbers = env.GetPossibleNumbers_Cross(_board, coordinates[0], coordinates[1]);

                    foreach (int num in possible_numbers)
                    {
                        int[,] __board = (int[,])_board.Clone();
                        __board[coordinates[0], coordinates[1]] = num;
                        next_boards.Add(__board);
                    }
                }

                boards = next_boards;
                deep_level--;
            }

            //
            List<Task> workers = new List<Task>();

            foreach (int[,] board in boards)
            {
                CancellationToken __token = tokenSource.Token;
                if (__token.IsCancellationRequested)
                    break;
                Task t = Task.Run(() => Worker_Cross(board, tokenSource, __token), __token);
                workers.Add(t);
            }

            try
            {
                Task.WhenAll(workers.ToArray());
            }
            catch (OperationCanceledException)
            {
                Debug.Print($"\n {nameof(OperationCanceledException)} thrown");
            }
            

        }
        public object[] MultiThreadingSolution_Cross_NE(int deep_level, CancellationTokenSource tokenSource, int[,] board_original)
        {
            EnvChecker env = new EnvChecker();
            List<int[,]> boards = new List<int[,]>() { board_original };
            while (deep_level >= 1)
            {
                List<int[,]> next_boards = new List<int[,]>();

                foreach (int[,] _board in boards)
                {
                    int[] coordinates = { 0, 0 };
                    while (_board[coordinates[0], coordinates[1]] != 0)
                    {
                        coordinates = env.NextCoordinates(coordinates[0], coordinates[1]);
                        if (coordinates[0] == 9)
                            return null;
                    }

                    List<int> possible_numbers = env.GetPossibleNumbers_Cross(_board, coordinates[0], coordinates[1]);

                    foreach (int num in possible_numbers)
                    {
                        int[,] __board = (int[,])_board.Clone();
                        __board[coordinates[0], coordinates[1]] = num;
                        next_boards.Add(__board);
                    }
                }

                boards = next_boards;
                deep_level--;
            }

            //
            List<Task<object[]>> workers = new List<Task<object[]>>();

            foreach (int[,] board in boards)
            {
                CancellationToken __token = tokenSource.Token;
                if (__token.IsCancellationRequested)
                    break;
                Task<object[]> t = Task.Run(() => Worker_Cross_NE(board, tokenSource, __token)); //   remove the token from the task itself
                workers.Add(t);
            }
            object[] returnValue = new object[0];
            try
            {
                returnValue = Task.WhenAll(workers.ToArray()).Result;
                foreach (var t in workers)
                    t.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return returnValue;
        }
        public void MultiThreadingSolution(int deep_level, CancellationTokenSource tokenSource, int[,] board_original)
        {
            EnvChecker env = new EnvChecker();
            List<int[,]> boards = new List<int[,]>() { board_original };
            while (deep_level >= 1)
            {
                List<int[,]> next_boards = new List<int[,]>();

                foreach (int[,] _board in boards)
                {
                    int[] coordinates = { 0, 0 };
                    while (_board[coordinates[0], coordinates[1]] != 0)
                    {
                        coordinates = env.NextCoordinates(coordinates[0], coordinates[1]);
                        if (coordinates[0] == 9)
                            return;
                    }

                    List<int> possible_numbers = env.GetPossibleNumbers(_board, coordinates[0], coordinates[1]);

                    foreach (int num in possible_numbers)
                    {
                        int[,] __board = (int[,])_board.Clone();
                        __board[coordinates[0], coordinates[1]] = num;
                        next_boards.Add(__board);
                    }
                }

                boards = next_boards;
                deep_level--;
            }

            //
            List<Task> workers = new List<Task>();

            foreach (int[,] board in boards)
            {
                CancellationToken __token = tokenSource.Token;
                if (__token.IsCancellationRequested)
                    break;
                Task t = Task.Run(() => Worker(board, tokenSource, __token), __token);
                workers.Add(t);
            }

            try
            {
                Task.WhenAll(workers.ToArray());
            }
            catch (OperationCanceledException)
            {
                Debug.Print($"\n {nameof(OperationCanceledException)} thrown");
            }
        }
        public object[] MultiThreadingSolution_NE(int deep_level, CancellationTokenSource tokenSource, int[,] board_original)
        {
            EnvChecker env = new EnvChecker();
            List<int[,]> boards = new List<int[,]>() { board_original };
            while (deep_level >= 1)
            {
                List<int[,]> next_boards = new List<int[,]>();

                foreach (int[,] _board in boards)
                {
                    int[] coordinates = { 0, 0 };
                    while (_board[coordinates[0], coordinates[1]] != 0)
                    {
                        coordinates = env.NextCoordinates(coordinates[0], coordinates[1]);
                        if (coordinates[0] == 9)
                            return null;
                    }

                    List<int> possible_numbers = env.GetPossibleNumbers(_board, coordinates[0], coordinates[1]);

                    foreach (int num in possible_numbers)
                    {
                        int[,] __board = (int[,])_board.Clone();
                        __board[coordinates[0], coordinates[1]] = num;
                        next_boards.Add(__board);
                    }
                }

                boards = next_boards;
                deep_level--;
            }

            //
            List<Task<object[]>> workers = new List<Task<object[]>>();

            foreach (int[,] board in boards)
            {
                CancellationToken __token = tokenSource.Token;
                if (__token.IsCancellationRequested)
                    break;
                Task<object[]> t = Task.Run(() => Worker_NE(board, tokenSource, __token));
                workers.Add(t);
            }
            object[] returnValue = new object[0];
            try
            {
                returnValue = Task.WhenAll(workers.ToArray()).Result;
                foreach (var t in workers)
                    t.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return returnValue;
        }

        ////////////// WORKERS

        private void Worker(int[,] board, CancellationTokenSource tokenSource, CancellationToken token)
        {
            int[,] workers_board = (int[,])board.Clone();

            if (RecursiveSolution_Async(workers_board, 0, 0, token))
            {
                try
                {
                    tokenSource.Cancel();
                }
                catch (Exception)
                {
                    throw new OperationCanceledException();
                }
                ctr.VisualizeBoard(workers_board);
                pass_Board.Invoke(workers_board);
            }
        }
        private void Worker_Cross(int[,] board, CancellationTokenSource tokenSource, CancellationToken token)
        {
            int[,] workers_board = (int[,])board.Clone();

            if (RecursiveSolution_Cross_Async(workers_board, 0, 0, token))
            {
                try
                {
                    tokenSource.Cancel();
                }
                catch(Exception)
                {
                    throw new OperationCanceledException();
                }
                
                ctr.VisualizeBoard(workers_board);
                pass_Board.Invoke(workers_board);
            }
        }
        private object[] Worker_NE(int[,] board, CancellationTokenSource tokenSource, CancellationToken token)
        {
            int[,] workers_board = (int[,])board.Clone();

            if (RecursiveSolution_Async_NE(workers_board, 0, 0, token))
            {
                tokenSource.Cancel();
                return new object[] { true, board, workers_board };
            }
            return new object[] { false };
        }
        private object[] Worker_Cross_NE(int[,] board, CancellationTokenSource tokenSource, CancellationToken token)
        {
            int[,] workers_board = (int[,])board.Clone();

            if (RecursiveSolution_Cross_Async_NE(workers_board, 0, 0, token))
            {
                tokenSource.Cancel();
                return new object[] { true, board, workers_board };
            }
            return new object[] { false };
        }

    }
}
