using System;
using System.Collections.Generic;

namespace Lab01.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public bool IsDelete { get; set; }
        public List<BoardTransaction> BoardTransactions { get; set; }
    }
}