using System;

namespace Lab01.RequestModels
{
    public class AddBoardTransactionRequest
    {
        public Guid? HostId { get; set; }
        public Guid? PlayerId { get; set; }
        public int BoardSize{ get; set; }
        public Guid? RoomId { get; set; }
    }
}