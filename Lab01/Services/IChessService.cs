using Lab01.RequestModels;

namespace Lab01.Services
{
    public interface IChessService
    {
        public bool CheckWin(int[,] board, ChessRequest chess);
        public bool CheckWin(ChessRequest chess);

        public int[,] GenerateBoard(int size);

        public void ShowBoardInConsole(int[,] board);
    }
}