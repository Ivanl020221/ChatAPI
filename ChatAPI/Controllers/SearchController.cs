using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatAPI.Controllers
{
    public class SearchController : ApiController
    {
        private readonly ChatEntities context = new ChatEntities();

        // GET: api/Search
        [HttpGet]
        public IEnumerable<DepatrmentsSerial> GetDerartments()
        {
            return this.context.departments.Select(i => new DepatrmentsSerial { IdDepartments = i.IdDepartments, Name = i.Name }).ToList();
        }

        // GET: api/Search/5
        [HttpGet]
        public IEnumerable<SerialEmp> GetEmp([FromUri]string Departmens, [FromUri]string SearchName)
        {
            return this.context.
                    employees.SqlQuery($"SELECT * FROM employees WHERE Department_Id IN({Departmens})").
                        Where(i => i.Name.Contains(SearchName)).Select(j => new SerialEmp
                        {
                            Department_Id = (long)j.Department_Id,
                            ID = j.ID,
                            Name = j.Name,
                            Password = j.Password,
                            Username = j.Username
                        }).ToList();

        }
    }

    public class DepatrmentsSerial
    {
        public long IdDepartments { get; set; }
        public string Name { get; set; }
    }
}
