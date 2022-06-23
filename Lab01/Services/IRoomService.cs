using System;
using System.Collections.Generic;
using Lab01.Models.ViewModels;
using Lab01.RequestModels;

namespace Lab01.Services
{
    public interface IRoomService
    {

        List<RoomResponse> GetRoomList();

        RoomResponse CreateRoom(CreateRoomRequest request);

        RoomResponse EditRoom(EditRoomRequest request);

        bool DeleteRoom(Guid roomId);

    }
}