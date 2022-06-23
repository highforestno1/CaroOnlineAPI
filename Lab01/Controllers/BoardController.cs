using Lab01.Models.ViewModels;
using Lab01.RequestModels;
using Lab01.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController : ControllerBase
    {
        private IBoardTransactionService _boardTransactionService;

        public BoardController(IBoardTransactionService boardTransactionService)
        {
            _boardTransactionService = boardTransactionService;
        }

        [HttpGet("get-board-transaction/{id}")]
        public IActionResult GetBoardById(string id)
        {
            var boardTransaction = _boardTransactionService.GetTransactionById(id);
            var result = new BoardTransactionResponse
            {
            };
            return Ok(boardTransaction);
        }

        [HttpPost]
        public IActionResult CreateNewBoard([FromBody] AddBoardTransactionRequest request)
        {
            return Ok(_boardTransactionService.CreateNewBoardTransaction(request));
        }


        [HttpPost("pick")]
        public IActionResult PickChess([FromBody] PickChessReqeuest request)
        {
            var isWin = _boardTransactionService.PickBoardTransaction(request);
            return Ok(isWin);
        }
    }
}