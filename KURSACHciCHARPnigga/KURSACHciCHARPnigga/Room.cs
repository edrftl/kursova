using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KURSACHciCHARPnigga
{
    public class Room
    {
        public string NameOfRoom;
        public Room firstRoom;
        public Room secondRoom;
        public Room thirdRoom;

        public Room(string nameOfRoom, Room firstRoom, Room secondRoom, Room thirdRoom)
        {
            NameOfRoom = nameOfRoom;
            this.firstRoom = firstRoom;
            this.secondRoom = secondRoom;
            this.thirdRoom = thirdRoom;
        }
        public Room(string nameOfRoom)
        {
            NameOfRoom = nameOfRoom;
        }
        public void InitializeRoom(Room firstRoom, Room secondRoom, Room thirdRoom)
        {
            this.firstRoom = firstRoom;
            this.secondRoom = secondRoom;
            this.thirdRoom = thirdRoom;
        }
    }
}
