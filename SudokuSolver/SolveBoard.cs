namespace SudokuSolver
{
    public class SolveBoard
    {
        private List<int[,]> _solutionBoards = new List<int[,]>();

        private static int[,] _startBoard = new int[9, 9] {
            { 7, 8, 0, 0, 0, 5, 0, 0, 1 },
            { 0, 0, 1, 0, 8, 0, 7, 0, 4 },
            { 2, 4, 0, 0, 7, 0, 5, 0, 0 },
            { 0, 3, 8, 9, 0, 0, 4, 0, 0 },
            { 0, 0, 5, 8, 0, 0, 1, 7, 0 },
            { 4, 0, 7, 0, 1, 0, 0, 5, 0 },
            { 9, 0, 0, 5, 0, 4, 0, 1, 0 },
            { 0, 1, 0, 7, 9, 0, 0, 2, 0 },
            { 0, 0, 0, 0, 0, 0, 9, 0, 7 },
        };

        public void Solve()
        {
            IsBoardSolved(_startBoard);
            if (_solutionBoards != null)
            {
                foreach (var board in _solutionBoards)
                {
                    WriteBoard(board);
                    Console.WriteLine("***********");
                }

                Console.WriteLine($"Solved with {_solutionBoards.Count} solutions");
            }
            else
            {
                Console.WriteLine("Can't solve. Many apologies");
            }
        }

        public bool IsBoardSolved(int[,] newBoard)
        {
            for (var row = 0; row < 9; row++)
            {
                for (var column = 0; column < 9; column++)
                {
                    if (newBoard[row, column] == 0)
                    {
                        for (var number = 1; number <= 9; number++)
                        {
                            if (IsValidPlacement(newBoard, number, row, column))
                            {
                                newBoard[row, column] = number;

                                if (IsBoardSolved(newBoard))
                                {
                                    return true;
                                }

                                newBoard[row, column] = 0;
                            }
                        }

                        return false;
                    }
                }
            }

            AddSolution(newBoard);
            return false;
        }

        public void AddSolution(int[,] board)
        {
            int[,] newBoard = new int[9, 9];

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    newBoard[r, c] = board[r, c];
                }
            }

            _solutionBoards.Add(newBoard);
        }

        private bool IsValidPlacement(int[,] board, int number, int row, int column)
        {
            return !IsNumberInRow(board, number, row) &&
                   !IsNumberInColumn(board, number, column) &&
                   !IsNumberInBox(board, number, row, column);
        }

        public bool IsNumberInRow(int[,] board, int number, int row)
        {
            for (int column = 0; column < 9; column++)
            {
                if (board[row, column] == number)
                    return true;
            }

            return false;
        }

        public bool IsNumberInColumn(int[,] board, int number, int column)
        {
            for (int row = 0; row < 9; row++)
            {
                if (board[row, column] == number)
                    return true;
            }

            return false;
        }

        public bool IsNumberInBox(int[,] board, int number, int row, int column)
        {
            int boxRow = row - row % 3;
            int boxColumn = column - column % 3;

            for (int r = boxRow; r < boxRow + 3; r++)
            {
                for (int c = boxColumn; c < boxColumn + 3; c++)
                {
                    if (board[r, c] == number)
                        return true;
                }
            }

            return false;
        }

        public void WriteBoard(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                if (row % 3 == 0 && row > 0)
                    Console.WriteLine("-----------");

                for (int column = 0; column < 9; column++)
                {
                    if (column % 3 == 0 && column > 0)
                        Console.Write("|");
                    Console.Write(board[row, column]);
                }

                Console.WriteLine();
            }
        }
    }
}
