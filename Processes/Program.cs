using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Processes
{

    public class ProcessesController : ApiController
    {
        public ProcessCollectionRepModel Get(string name)
        {
            if(string.IsNullOrEmpty(name))
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

    public class ProcessRepModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalProcessorTimeInMillis { get; set; }
        // other properties

        public ProcessRepModel(){}
        public ProcessRepModel(Process proc)
        {
            Id = proc.Id;
            Name = proc.ProcessName;
            TotalProcessorTimeInMillis = proc.TotalProcessorTime.TotalMilliseconds;
            // other properties
        }
    }

    public class ProcessCollectionRepModel
    {
        public IEnumerable<ProcessRepModel> Processes { get; set; } 
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");
            config.Routes.MapHttpRoute(
                "ApiDefault",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );
            config.MessageHandlers.Add(new TraceMessageHandler());
            Trace.Listeners.Add(new ConsoleTraceListener());
            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.WriteLine("Server is opened");
            Console.ReadKey();
            server.CloseAsync().Wait();
        }
    }

    public class TraceMessageHandler : MessageProcessingHandler
    {
        protected override HttpRequestMessage ProcessRequest(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(
            HttpResponseMessage response, CancellationToken cancellationToken)
        {
            Trace.TraceInformation("'{0}' to '{1}' completed with a '{2}' status",
                response.RequestMessage.Method,
                response.RequestMessage.RequestUri,
                response.StatusCode);
            return response;
        }
    }
}
