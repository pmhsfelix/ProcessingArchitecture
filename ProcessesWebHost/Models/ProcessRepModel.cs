using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ProcessesWebHost.Models
{
    public class ProcessRepModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalProcessorTimeInMillis { get; set; }
        // other properties

        public ProcessRepModel() { }
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
}