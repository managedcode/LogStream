using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedCode.LogStream
{
    public class LogModel
    {
        public LogLevel level;
        public EventId eventId;
        public DateTime dateTime;
        public string name; 
        public string severity;
        public object state;
        public Exception error;
        public string message;
    }
}
