using Lab01.RequestModels;
using Lab01.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessController : ControllerBase
    {
        private readonly IChessService _chessService;

        public ChessController(IChessService chessService)
        {
            _chessService = chessService;
        }

        [HttpPost("test")]
        public IActionResult TestChess([FromBody] ChessRequest request)
        {
            
            var result = _chessService.CheckWin(request);

            return Ok(result);
        }
    }
}