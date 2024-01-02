using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace KURSACHciCHARPnigga
{
    class MyDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LONMETH\SQLEXPRESS;Integrated Security=True;
            //                                Connect Timeout=30;Initial Catalog=<LabirintRoomSystem>;Encrypt=False;TrustServerCertificate=False;
            //                                ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-3A1T100\SQLEXPRESS;
                            Initial Catalog = LabirintDb;
                            Integrated Security=True;Connect Timeout=30;
                            Encrypt=False;Trust Server Certificate=False;
                            Application Intent=ReadWrite;
                            Multi Subnet Failover=False",
                             options => options.EnableRetryOnFailure());
        }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, NameOfRoom = "Room 1 try \n to escape\n labirint" },
                new Room { Id = 2, NameOfRoom = "Room 2" },
                new Room { Id = 3, NameOfRoom = "Room 3" },
                new Room { Id = 4, NameOfRoom = "Room 4" },
                new Room { Id = 5, NameOfRoom = "Room 5" },
                new Room { Id = 6, NameOfRoom = "Room 6" },
                new Room { Id = 7, NameOfRoom = "Room 7" },
                new Room { Id = 8, NameOfRoom = "Room 8" },
                new Room { Id = 9, NameOfRoom = "Room 9" },
                new Room { Id = 10, NameOfRoom = "Room 10" },
                new Room { Id = 11, NameOfRoom = "Room 11" },
                new Room { Id = 12, NameOfRoom = "Room 12" },
                new Room { Id = 13, NameOfRoom = "Room 13" },
                new Room { Id = 14, NameOfRoom = "Room 14" },
                new Room { Id = 15, NameOfRoom = "Room 15" },
                new Room { Id = 16, NameOfRoom = "Room 16" },
                new Room { Id = 17, NameOfRoom = "Room 17" },
                new Room { Id = 18, NameOfRoom = "You won" }
            );
        //    modelBuilder.Entity<Room>()
        //        .HasOne(r => r.firstRoom)
        //        .WithMany()
        //        .HasForeignKey(r => r.FirstRoomId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Room>()
        //        .HasOne(r => r.secondRoom)
        //        .WithMany()
        //        .HasForeignKey(r => r.SecondRoomId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Room>()
        //        .HasOne(r => r.thirdRoom)
        //        .WithMany()
        //        .HasForeignKey(r => r.ThirdRoomId)
        //        .OnDelete(DeleteBehavior.Restrict);
        }


        //SeedRandomRoomAssignments(modelBuilder);





        public void SeedRandomRoomAssignments()
            {
                try
                {
                    //using (var context = new MyDB())
                    //{
                    var random = new Random();
                    var rooms = this.Rooms.ToList();

                    foreach (var room in rooms)
                    {
                        room.firstRoom = GetRandomRoom(rooms, random);
                        room.secondRoom = GetRandomRoom(rooms, random);
                        room.thirdRoom = GetRandomRoom(rooms, random);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                    //return null;
                }
            }

            private Room GetRandomRoom(List<Room> rooms, Random random)
            {
                try
                {
                    // Exclude the current room from the available choices
                    var availableRooms = rooms.Where(r => r.Id != 18).ToList();

                    // Return a random room from the available choices
                    return availableRooms[random.Next(availableRooms.Count)];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                    return null;
                }

            }
            public void SeedData()
            {
                try
                {
                    SeedRandomRoomAssignments();
                    SaveChanges(); // Save changes to the database
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                    //return null;
                }
            }




        }
    }


