using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatAPI.Controllers
{
    public class ChatRoomController : ApiController
    {
        private readonly ChatEntities context = new ChatEntities();
        // GET: api/ChatRoom
        [HttpGet]
        public ChatRoomSerial GetChatRoomInfo([FromUri]long ChatRoomID) => this.context.
                ChatRoom.
                    Where(i => i.IdChatRoom == ChatRoomID).
                        Select(i => new ChatRoomSerial
                        {
                            IdChatRoom = i.IdChatRoom,
                            Topic = i.Topic
                        }).FirstOrDefault();

        [HttpGet]
        public IEnumerable<MessageSerial> GetMessage([FromUri]long ChatRoomIDForMessage) => this.context.ChatMassage.
                    Where(i => i.TopicID == ChatRoomIDForMessage).
                        Select(i =>
                        new MessageSerial
                        {
                            IdMessage = i.IdMessage,
                            SenderID = i.SenderID,
                            TopicID = i.TopicID,
                            Time = i.Time,
                            Massage = i.Massage,
                            SenderName = i.employees.Name
                        });

        [HttpGet]
        public IEnumerable<SerialEmp> GetEmployees([FromUri]long ChatRoomIDForUer) => this.context.
                       EmployeChatRoom.
                           Where(i => i.ChatRoomID == ChatRoomIDForUer).
                               Select(i => i.employees).
                                Select(j => new SerialEmp
                                {
                                    ID = j.ID,
                                    Name = j.Name,
                                    Username = j.Username,
                                    Department_Id = (long)j.Department_Id,
                                    Password = j.Password
                                });

        [HttpPut]
        public void Rename([FromUri]string Topic, [FromUri]long IdRoom)
        {
            this.context.ChatRoom.Where(i => i.IdChatRoom == IdRoom).FirstOrDefault().Topic = Topic;
            this.context.SaveChanges();
        }

        [HttpPost]
        public ChatRoomSerial CreateRoom([FromUri]string TopicName, [FromBody]IEnumerable<long> IdEmp)
        {
            ChatRoom Room = new ChatRoom
            {
                Topic = TopicName
            };
            this.context.ChatRoom.Add(Room);
            this.context.SaveChanges();
            foreach (var idus in IdEmp)
            {
                this.context.EmployeChatRoom.Add(new EmployeChatRoom { ChatRoomID = Room.IdChatRoom, EmployeeID = idus });
            }
            this.context.SaveChanges();
            return new ChatRoomSerial { IdChatRoom = Room.IdChatRoom, Topic = Room.Topic };
        }

        [HttpPost]
        public void SendMessage([FromUri]string Message, [FromUri]long IdEmp, [FromUri]long IdRoom) => this.context.ChatMassage.Add(
                new ChatMassage
                {
                    SenderID = IdEmp,
                    TopicID = IdRoom,
                    Massage = Message,
                    Time = DateTime.Now
                });

        [HttpPost]
        public void AddEmp([FromUri]long RoomId, [FromUri]long IdEmp)
        {
            this.context.EmployeChatRoom.Add(new EmployeChatRoom { ChatRoomID = RoomId, EmployeeID = IdEmp });
            this.context.SaveChanges();
        }
    }

    public class ChatRoomSerial
    {
        public long IdChatRoom { get; set; }
        public string Topic { get; set; }
    }

    public class MessageSerial
    {
        public long IdMessage { get; set; }
        public long SenderID { get; set; }
        public long TopicID { get; set; }
        public System.DateTime Time { get; set; }
        public string Massage { get; set; }
        public string SenderName { get; set; }
    }
}
