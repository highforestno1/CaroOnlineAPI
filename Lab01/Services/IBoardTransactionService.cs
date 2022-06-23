using System.Collections.Generic;
using Lab01.Models;
using Lab01.RequestModels;

namespace Lab01.Services
{
    public interface IBoardTransactionService
    {
        BoardTransaction GetTransactionById(string id);

        BoardTransaction CreateNewBoardTransaction(AddBoardTransactionRequest request);
        bool PickBoardTransaction(PickChessReqeuest request);

    }
}