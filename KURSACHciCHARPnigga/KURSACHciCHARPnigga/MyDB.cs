using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace KURSACHciCHARPnigga
{
    public class MyDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LONMETH\SQLEXPRESS;
                            Initial Catalog = RoomDb;
                            Integrated Security=True;Connect Timeout=30;
                            Encrypt=False;Trust Server Certificate=False;
                            Application Intent=ReadWrite;
                            Multi Subnet Failover=False");

            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-3A1T100\SQLEXPRESS;
            //                Initial Catalog = RoomDb;
            //                Integrated Security=True;Connect Timeout=30;
            //                Encrypt=False;Trust Server Certificate=False;
            //                Application Intent=ReadWrite;
            //                Multi Subnet Failover=False");
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        public Room getRoom(int roomId)
        {
            var room = this.Rooms.Find(roomId);
            return room;
        }

        public void addMessage(int roomId, string content)
        {
            try
            {
                var room = this.Rooms.Find(roomId);

                if (room != null)
                {
                    var message = new Message { RoomId = roomId, Content = content };
                    room.Messages.Add(message);
                    this.SaveChanges(); // Save changes to the database
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Room>().HasData(

                 new Room { Id = 1, NameOfRoom = "Room 1", FirstRoomId = 2, SecondRoomId = 3, ThirdRoomId = 4 },
                 new Room { Id = 2, NameOfRoom = "Room 2", FirstRoomId = 3, SecondRoomId = 4, ThirdRoomId = 5 },
                 new Room { Id = 3, NameOfRoom = "Room 3", FirstRoomId = 4, SecondRoomId = 1, ThirdRoomId = 4 },
                 new Room { Id = 4, NameOfRoom = "Room 4", FirstRoomId = 6, SecondRoomId = 3, ThirdRoomId = 5 },
                 new Room { Id = 5, NameOfRoom = "Room 5", FirstRoomId = 4, SecondRoomId = 6, ThirdRoomId = 7 },
                 new Room { Id = 6, NameOfRoom = "Room 6", FirstRoomId = 4, SecondRoomId = 5, ThirdRoomId = 7 },
                 new Room { Id = 7, NameOfRoom = "Room 7", FirstRoomId = 9, SecondRoomId = 8, ThirdRoomId = 6 },
                 new Room { Id = 8, NameOfRoom = "Room 8", FirstRoomId = 6, SecondRoomId = 11, ThirdRoomId = 10 },
                 new Room { Id = 9, NameOfRoom = "Room 9", FirstRoomId = 17, SecondRoomId = 11, ThirdRoomId = 7 },
                 new Room { Id = 10, NameOfRoom = "Room 10", FirstRoomId = 8, SecondRoomId = 11, ThirdRoomId = 12 },
                 new Room { Id = 11, NameOfRoom = "Room 11", FirstRoomId = 9, SecondRoomId = 8, ThirdRoomId = 11 },
                 new Room { Id = 12, NameOfRoom = "Room 12", FirstRoomId = 10, SecondRoomId = 13, ThirdRoomId = 14 },
                 new Room { Id = 13, NameOfRoom = "Room 13", FirstRoomId = 16, SecondRoomId = 15, ThirdRoomId = 14 },
                 new Room { Id = 14, NameOfRoom = "Room 14", FirstRoomId = 12, SecondRoomId = 13, ThirdRoomId = 15 },
                 new Room { Id = 15, NameOfRoom = "Room 15", FirstRoomId = 17, SecondRoomId = 16, ThirdRoomId = 14 },
                 new Room { Id = 16, NameOfRoom = "Room 16", FirstRoomId = 17, SecondRoomId = 16, ThirdRoomId = 14 },
                 new Room { Id = 17, NameOfRoom = "Room 17", FirstRoomId = 16, SecondRoomId = 18, ThirdRoomId = 15 },
                 new Room { Id = 18, NameOfRoom = "You Won!!!", FirstRoomId = null, SecondRoomId = null, ThirdRoomId = null }

             );
        }
    }
}


