using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyAPI.Controllers
{

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
    public class ValuesController : ApiController
    {








        private readonly Configuration _configuration;
        private readonly string _baseURL;
        private readonly string _Specialities;



 

     

        public ValuesController( Configuration configuration)
        {
            _configuration = configuration;
            _Specialities = _configuration.GetSection("Specialities").ToString();
        }





        // GET api/values
  

        // GET api/values/5
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(_Specialities) };
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
