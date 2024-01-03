using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KURSACHciCHARPnigga
{
    public class Room
    {

        public int Id { get; set; }
        public string NameOfRoom { get; set; }

        //// Foreign key properties
        public int? FirstRoomId { get; set; }
        public int? SecondRoomId { get; set; }
        public int? ThirdRoomId { get; set; }

        // Navigation properties
        //[ForeignKey("FirstRoomId")]
        //public Room FirstRoom { get; set; }

        //[ForeignKey("SecondRoomId")]
        //public Room SecondRoom { get; set; }

        //[ForeignKey("ThirdRoomId")]
        //public Room ThirdRoom { get; set; }




        //public Room(string nameOfRoom, Room firstRoom, Room secondRoom, Room thirdRoom)
        //{
        //    NameOfRoom = nameOfRoom;
        //    this.firstRoom = firstRoom;
        //    this.secondRoom = secondRoom;
        //    this.thirdRoom = thirdRoom;
        //}
        //public Room(string nameOfRoom)
        //{
        //    NameOfRoom = nameOfRoom;
        //}
        //public void InitializeRoom(Room firstRoom, Room secondRoom, Room thirdRoom)
        //{
        //    this.firstRoom = firstRoom;
        //    this.secondRoom = secondRoom;
        //    this.thirdRoom = thirdRoom;
        //}
    }
}
