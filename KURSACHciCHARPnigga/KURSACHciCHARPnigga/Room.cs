using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KURSACHciCHARPnigga
{
    public class Room
    {
        public int Id { get; set; }
        public string NameOfRoom { get; set; }

        // Foreign key properties
        public int? FirstRoomId { get; set; }
        public int? SecondRoomId { get; set; }
        public int? ThirdRoomId { get; set; }

        // Navigation properties
        public Room firstRoom { get; set; }
        public Room secondRoom { get; set; }
        public Room thirdRoom { get; set; }

        // ... (other members)

        //public Room(string nameOfRoom)
        //{
        //    NameOfRoom = nameOfRoom;
        //}

        //public Room()
        //{
        //}

        public void InitializeRoom(Room firstRoom, Room secondRoom, Room thirdRoom)
        {
            this.firstRoom = firstRoom;
            this.secondRoom = secondRoom;
            this.thirdRoom = thirdRoom;
        }
    }

}
