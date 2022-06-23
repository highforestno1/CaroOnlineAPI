using System;
using System.Collections.Generic;
using System.Linq;
using Lab01.Models;
using Lab01.RequestModels;

namespace Lab01.Services.Impl
{
    public class BoardTransactionService : IBoardTransactionService
    {
        private readonly MasterDbContext _context;
        private readonly IChessService _chessService;

        public BoardTransactionService(MasterDbContext context, IChessService chessService)
        {
            _context = context;
            _chessService = chessService;
        }


        public BoardTransaction GetTransactionById(string id)
        {
            var boardTransaction =
                _context.BoardTransactions.FirstOrDefault(transaction => transaction.Id.ToString() == id && !transaction.IsDelete);
            return boardTransaction;
        }

        public BoardTransaction CreateNewBoardTransaction(AddBoardTransactionRequest request)
        {
            int[,] boardArr = _chessService.GenerateBoard(request.BoardSize);
        
            var newBoardTransaction = new BoardTransaction
            {
                Id = Guid.NewGuid(),
                HostId = request.HostId,
                PlayerId = request.PlayerId,
                RoomId = request.RoomId,
                Board = Convert2DArrToString(boardArr)
            };

            _context.Add(newBoardTransaction);
            _context.SaveChanges();
            return newBoardTransaction;
        }

        public bool PickBoardTransaction(PickChessReqeuest request)
        {
            var boardTransaction =_context.BoardTransactions.FirstOrDefault(transaction => transaction.Id == request.BoardId);
            if (boardTransaction == null)
            {
                throw new Exception("Board exist!");
            }

            var arrBoardTransaction = ConvertStringTo2DArr(boardTransaction.Board);
            var newChessRequest = new ChessRequest
            {
                X = request.X,
                Y = request.Y,
                Type = request.Type
            };
            arrBoardTransaction[request.X, request.Y] = request.Type; 
            var isWin = _chessService.CheckWin(arrBoardTransaction,newChessRequest);
            if (isWin)
            {
                boardTransaction.Status = 1;
                // boardTransaction.WinnerId = request.PlayerId;
            }

            boardTransaction.Board = Convert2DArrToString(arrBoardTransaction);
            
            _context.SaveChanges();
            return isWin;
        }

        private string Convert2DArrToString(int[,] board)
        {
            var boardArr = new List<int>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    boardArr.Add(board[i, j]);
                }
            }

            return String.Join(",", boardArr);
        }


        private int[,] ConvertStringTo2DArr(string boardString)
        {
            var boardArr = boardString.Split(",");
            int size = (int)Math.Sqrt(boardArr.Length);
            var result = new int[size, size];

            int x = 0, y = 0;
            for (int i = 0; i < boardArr.Length; i++)
            {
                result[x, y++] = Convert.ToInt32(boardArr[i]);
                if (y == size)
                {
                    x++;
                    y = 0;
                }
            }

            return result;
        }
        
        
    }
}