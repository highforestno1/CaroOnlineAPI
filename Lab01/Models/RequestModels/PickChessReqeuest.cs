using System;

namespace Lab01.RequestModels
{
    public class PickChessReqeuest
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Type { get; set; }
        public Guid BoardId { get; set; }
        public Guid PlayerId { get; set; }
        
    }
}