using System;
using Lab01.RequestModels;
using Newtonsoft.Json;

namespace Lab01.Services.Impl
{
    public class ChessService : IChessService
    {
        public bool CheckWin(int[,] board, ChessRequest chess)
        {
            // Check neu ma tran nho hon 5
            if (board.Length < 5)
            {
                return false;
            }
            var result = false;

            // TC1:
            /*board[0, 1] = 1;
            board[1, 1] = 0;
            board[2, 1] = 0;
            board[3, 1] = 0;
            board[4, 1] = 0;
            board[5, 1] = 0;*/
            // Check duong doc
            // result = CheckWinDoc(board, chess);

            // TC2:
            /*board[1, 0] = 1;
            board[1, 1] = 0;
            board[1, 2] = 0;
            board[1, 3] = 0;
            board[1, 4] = 0;
            board[1, 5] = 0;
            result = CheckWinNgang(board, chess);*/

            // TC3:
            /*board[0, 0] = 1;
            board[1, 1] = 0;
            board[2, 2] = 0;
            board[3, 3] = 0;
            board[4, 4] = 0;
            board[5, 5] = 1;
            result = CheckWinCheoChinh(board, chess);*/

            // TC4:
            /*board[6,1] = 1;
            board[5,2] = 0;
            board[4,3] = 0;
            board[3,4] = 0;
            board[2,5] = 0;
            board[1,6] = 1;*/

            result = CheckWinDoc(board, chess) ||
                     CheckWinNgang(board, chess) ||
                     CheckWinCheoChinh(board, chess) ||
                     CheckWinCheoPhu(board, chess);


            ShowBoardInConsole(board);

            return result;
        }

        public bool CheckWin(ChessRequest chess)
        {
            var board = GenerateBoard(10);

            ShowBoardInConsole(board);
            
            return CheckWinDoc(board, chess) ||
                   CheckWinNgang(board, chess) ||
                   CheckWinCheoChinh(board, chess) ||
                   CheckWinCheoPhu(board, chess);
        }

        public int[,] GenerateBoard(int size)
        {
            int[,] board = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = -1;
                }
            }

            return board;
        }

        public void ShowBoardInConsole(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        private bool CheckWinDoc(int[,] board, ChessRequest chessRequest)
        {
            var count = 1;
            // Kiem tra phia trai
            for (int i = 1; i <= 5; i++)
            {
                var x = chessRequest.X - i;
                var y = chessRequest.Y;
                if (!CheckValidPoint(board, x, y) || board[x, y] != chessRequest.Type)
                {
                    break;
                }

                Console.WriteLine("{0},{1}", x, y);
                count++;
            }

            // Kiem tra duoi
            for (int i = 1; i <= 5; i++)
            {
                var x = chessRequest.X + i;
                var y = chessRequest.Y;

                if (!CheckValidPoint(board, x, y) || board[x, y] != chessRequest.Type)
                {
                    break;
                }

                Console.WriteLine("{0},{1}", x, y);
                count++;
            }

            return count >= 5;
        }

        private bool CheckWinNgang(int[,] board, ChessRequest chessRequest)
        {
            var count = 1;
            // Kiem tra cheo duoi
            for (int i = 1; i <= 5; i++)
            {
                var x = chessRequest.X;
                var y = chessRequest.Y - i;
                if (!CheckValidPoint(board, x, y) || board[x, y] != chessRequest.Type)
                {
                    break;
                }

                Console.WriteLine("{0},{1}", x, y);
                count++;
            }

            // Kiem tra duoi
            for (int i = 1; i <= 5; i++)
            {
                var x = chessRequest.X;
                var y = chessRequest.Y + i;

                if (!CheckValidPoint(board, x, y) || board[x, y] != chessRequest.Type)
                {
                    break;
                }

                Console.WriteLine("{0},{1}", x, y);
                count++;
            }

            return count >= 5;
        }

        private bool CheckWinCheoChinh(int[,] board, ChessRequest chessRequest)
        {
            var count = 1;
            // Kiem tra cheo duoi
            for (int i = 1; i <= 5; i++)
            {
                var x = chessRequest.X + i;
                var y = chessRequest.Y + i;
                if (!CheckValidPoint(board, x, y) || board[x, y] != chessRequest.Type)
                {
                    break;
                }

                Console.WriteLine("{0},{1}", x, y);
                count++;
            }

            // Kiem tra duoi
            for (int i = 1; i <= 5; i++)
            {
                var x = chessRequest.X - i;
                var y = chessRequest.Y - i;

                if (!CheckValidPoint(board, x, y) || (CheckValidPoint(board, x, y) && board[x, y] != chessRequest.Type))
                {
                    break;
                }

                Console.WriteLine("{0},{1}", x, y);
                count++;
            }

            return count >= 5;
        }

        private bool CheckWinCheoPhu(int[,] board, ChessRequest chessRequest)
        {
            var count = 1;
            // Kiem tra cheo duoi
            for (int i = 1; i <= 5; i++)
            {
                var x = chessRequest.X + i;
                var y = chessRequest.Y - i;
                if (!CheckValidPoint(board, x, y) || (CheckValidPoint(board, x, y) && board[x, y] != chessRequest.Type))
                {
                    break;
                }

                Console.WriteLine("{0},{1}", x, y);
                count++;
            }

            // Kiem tra duoi
            for (int i = 1; i <= 5; i++)
            {
                var x = chessRequest.X - i;
                var y = chessRequest.Y + i;

                if (!CheckValidPoint(board, x, y) || (CheckValidPoint(board, x, y) && board[x, y] != chessRequest.Type))
                {
                    break;
                }

                Console.WriteLine("{0},{1}", x, y);
                count++;
            }

            return count >= 5;
        }

        private static bool CheckValidPoint(int[,] board, int x, int y)
        {
            return x >= 0 && y >= 0 && x < board.GetLength(0) && y < board.GetLength(1);
        }
        
    }
}