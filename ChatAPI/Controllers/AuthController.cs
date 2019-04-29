using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatAPI.Controllers
{
    public class AuthController : ApiController
    {
        ChatEntities context = new ChatEntities();
        public SerialEmp Get([FromUri]string UserName, [FromUri]string Password) => this.context.employees.Where(i => i.Username == UserName && i.Password == Password).Select(j => new SerialEmp {  ID = j.ID, Name = j.Name, Username = j.Username , Department_Id = (long)j.Department_Id, Password = j.Password }).FirstOrDefault();
    }

    public class SerialEmp
    {

        public long ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public long Department_Id { get; set; }
        public string Password { get; set; }

    }
}
