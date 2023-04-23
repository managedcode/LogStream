using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ManagedCode.LogStream
{
    public class InMemoryLogger : ILogger
    {
        
        private readonly string _nameCategory;        
        private static List<LogModel> logs = new List<LogModel>();
        private IExternalScopeProvider _scopeProvider;
        public static LogLevel minLogLevel { get; set; }


        public InMemoryLogger(string nameCategory)
        {
            _nameCategory = nameCategory;
            _scopeProvider = new LoggerExternalScopeProvider();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return _scopeProvider?.Push(state);
            
        }

        public static int checkEnableLevel(LogLevel level)
        {
            if ((int)minLogLevel > (int)level)
            {
                return -1;
            }
            return (int)level;
        }

        public bool IsEnabled(LogLevel logLevel)
        {           
            if (checkEnableLevel(logLevel) < 0)
            {
                throw new InvalidOperationException("Invalid logging level!");
            }
            return true; 
            
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            
            if (IsEnabled(logLevel))
            {
                LogModel log = new LogModel()
                {
                    level = logLevel,
                    eventId = eventId.Id,                    
                    state = state,
                    dateTime = DateTime.Now,
                    error = exception,
                    message = formatter(state, exception)
                };

                logs.Add(log);
            }
        }

        public static IEnumerable<LogModel> GetLogTrace() 
        {
           return logs.Where(l => l.level.Equals(LogLevel.Trace));  
        }
        public static IEnumerable<LogModel> GetLogDebug()
        {
            return logs.Where(l => l.level.Equals(LogLevel.Debug));
        }
        public static IEnumerable<LogModel> GetLogInformation()
        {
            return logs.Where(l => l.level.Equals(LogLevel.Information));
        }
        public static IEnumerable<LogModel> GetLogWarning()
        {
            return logs.Where(l => l.level.Equals(LogLevel.Warning));
        }
        public static IEnumerable<LogModel> GetLogError()
        {
            return logs.Where(l => l.level.Equals(LogLevel.Error));
        }
        public static IEnumerable<LogModel> GetLogCritical()
        {
            return logs.Where(l => l.level.Equals(LogLevel.Critical));
        }
        public static IEnumerable<LogModel> GetAllLogs()
        {
            return logs.FindAll(l => l.level != LogLevel.None);
        }
    }
}