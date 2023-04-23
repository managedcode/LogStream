using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedCode.LogStream
{
    public class LogProvider:ILoggerProvider
    {
        
        public ILogger CreateLogger(string nameCategory)
        {
            return new InMemoryLogger(nameCategory);
            
        }

        public void Dispose()
        {
        }

    }
}
