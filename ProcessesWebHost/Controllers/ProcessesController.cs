using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProcessesWebHost.Models;

namespace ProcessesWebHost.Controllers
{
    public class ProcessesController : ApiController
    {
        public ProcessCollectionRepModel Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new ProcessCollectionRepModel
            {
                Processes = Process
                    .GetProcessesByName(name)
                    .Select(p => new ProcessRepModel(p))
            };
        }
    }
}
