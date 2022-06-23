using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Models;

namespace SignalR.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : Controller
    {
        private readonly IHubContext<BroadcastHub,IHubClient> _signalrHub;

        public TestController(IHubContext<BroadcastHub, IHubClient> signalrHub)
        {
            _signalrHub = signalrHub;
        }

        [HttpPost]
        public async Task<string> Post([FromBody]NotificationMessageModel msg)
        {
            var retMessage = string.Empty;
            try
            {
                msg.Timestamp = DateTime.Now.ToString();
                await _signalrHub.Clients.All.BroadcastNotification(msg);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }         
            return retMessage;
        }
    }
}