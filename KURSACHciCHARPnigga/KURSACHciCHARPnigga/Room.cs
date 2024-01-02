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

        public int? FirstRoomId { get; set; }
        public int? SecondRoomId { get; set; }
        public int? ThirdRoomId { get; set; }

        public Room FirstRoom { get; set; }
        public Room SecondRoom { get; set; }
        public Room ThirdRoom { get; set; }

    }
}
