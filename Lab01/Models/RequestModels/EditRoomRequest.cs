using System;

namespace Lab01.RequestModels
{
    public class EditRoomRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}