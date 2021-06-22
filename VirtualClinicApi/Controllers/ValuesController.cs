using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace VirtualClinicApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

 

        public HttpResponseMessage GetSpecialities()
        {
            var json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Specialities"));

            return new HttpResponseMessage()
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public ActionResult GetSpecialitiesList([FromUri] List<long> idList)
        {
            ...
        }
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
