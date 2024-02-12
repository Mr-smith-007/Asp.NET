using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO;

namespace Asp.NET.MiddleWares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;                
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
                
        public async Task InvokeAsync(HttpContext context)
        {
            LogConsole(context);
            await LogFile(context);

            await _next.Invoke(context);
        }

        private void LogConsole(HttpContext context)
        {            
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        private async Task LogFile(HttpContext context)
        {            
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
                        
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
                        
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }
    }
}
