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

        public int? FirstRoomId { get; set; }
        public int? SecondRoomId { get; set; }
        public int? ThirdRoomId { get; set; }
        public ICollection<Message> Messages { get; } = new List<Message>();

    }

    public class Message
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public string Content { get; set; } // Add this property to represent the content of the message

        public override string ToString()
        {
            return $"{Content}";
        }
    }
}
