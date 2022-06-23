using System;
using System.Collections.Generic;
using System.Linq;
using Lab01.Models;
using Lab01.Models.ViewModels;
using Lab01.RequestModels;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Lab01.Services.Impl
{
    public class RoomService : IRoomService
    {
        private readonly MasterDbContext _context;

        public RoomService(MasterDbContext context)
        {
            _context = context;
        }

        public List<RoomResponse> GetRoomList()
        {
            var rooms = _context.Rooms
                .Where(room => !room.IsDelete)
                .Select(room => new RoomResponse
                {
                    Id = room.Id,
                    Name = room.Name,
                    Status = room.Status
                }).ToList();
            
            rooms.ForEach(room =>
            {
                var boardTransaction = _context.BoardTransactions
                    .FirstOrDefault(transaction => transaction.RoomId == room.Id && transaction.Status == 0);
                if (boardTransaction != null)
                {
                    room.BoardTransactionId = boardTransaction.Id;
                }
            });

            return rooms;
        }

        public RoomResponse CreateRoom(CreateRoomRequest request)
        {
            if (null == request.Name)
            {
                throw new Exception("Data invalid!");
            }

            // Kiem tra ten phong nay co trong DB chua
            var isValid = _context.Rooms.Count(room => room.Name.Equals(request.Name)) == 0;
            if (!isValid)
            {
                throw new Exception("Room name exist!");
            }

            var newRoom = new Room
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Status = 0,
                IsDelete = false
            };

            _context.Rooms.Add(newRoom);
            _context.SaveChanges();
            return new RoomResponse
            {
                Id = newRoom.Id,
                Name = newRoom.Name,
                Status = newRoom.Status
            };
        }

        public RoomResponse EditRoom(EditRoomRequest request)
        {
            // Validate data
            if (null == request.Name || null == request.Id)
            {
                throw new Exception("Data invalid!");
            }

            // Kiem tra id phong nay co trong DB chua
            var editRoom = _context.Rooms.Where(room => room.Id == request.Id).FirstOrDefault();
            if (editRoom == null)
            {
                throw new Exception("Room not exist!");
            }

            editRoom.Name = request.Name;
            _context.SaveChanges();

            return new RoomResponse
            {
                Id = editRoom.Id,
                Name = editRoom.Name,
                Status = editRoom.Status
            }; 
        }

        public bool DeleteRoom(Guid roomId)
        {
            // Validate data
            if (null == roomId)
            {
                throw new Exception("Data invalid!");
            }

            // Kiem tra id phong nay co trong DB chua
            var deleteRoom = _context.Rooms.Where(room => room.Id == roomId).FirstOrDefault();
            if (deleteRoom == null)
            {
                throw new Exception("Room not exist!");
            }

            // Soft remove
            deleteRoom.IsDelete = true;
            
            // Hard remove
            // _context.Remove(deleteRoom);
            _context.SaveChanges();
            return true;
        }
    }
}