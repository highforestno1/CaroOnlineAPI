using System;
using Lab01.Models.Enum;

namespace Lab01.Models
{
    public class BoardTransaction
    {
        public Guid Id { get; set; }
        
        public Guid? HostId { get; set; }
        public ApplicationUser Host { get; set; }
        
        public Guid? PlayerId { get; set; }
        public ApplicationUser Player { get; set; }

        public ChessType HostType { get; set; }
        public ChessType PlayerType { get; set; }
        
        public int Status { get; set; }
        
        public String Board { get; set; }


        public Guid? WinnerId { get; set; }
        public ApplicationUser Winner { get; set; }
        
        public bool IsDelete { get; set; }

        public Guid? RoomId { get; set; }
        public Room Room { get; set; }
    }
}