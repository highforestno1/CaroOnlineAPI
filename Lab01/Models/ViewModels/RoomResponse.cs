using System;

namespace Lab01.Models.ViewModels
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public Guid BoardTransactionId { get; set; }
    }
}