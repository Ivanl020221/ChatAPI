using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ChatAPI.Controllers
{
    public class MainController : ApiController
    {
        ChatEntities context = new ChatEntities();

        // GET: api/Main
        public IEnumerable<SelectLastMessage_Result> Get([FromUri]long UserId)
        {

            return context.SelectLastMessage(UserId);
        }  
    }
}
