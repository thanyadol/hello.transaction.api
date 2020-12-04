//using LoggerService;
using hello.transaction.core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

using System.Net;

using System.Threading.Tasks;

namespace hello.transaction.core.Middleware
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;
        private readonly Serilog.ILogger Logg;

        public GlobalErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _loggerFactory = logger;
            _next = next;

            Logg = new LoggerConfiguration()
                .MinimumLevel.Error()
                //.WriteTo.RollingFile($@"{path}/exception.txt", retainedFileCountLimit: 7)
                .WriteTo.Console()
                // .WriteTo.Sink(new EntityLogEventSink())
                .CreateLogger();

        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }

        }


        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            //log to console or db
            this.TraceAsync(context, exception);

            return context.Response.WriteAsync(new ErrorDetails()
            {
                RequestId = Guid.Parse(context.Items["id"].ToString()),
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Type = exception.GetType().ToString(),
                Verb = "HTTP" + context.Request.Method,
                Endpoint = context.Request.Path,
                Source = exception.Source,

                HelpLink = exception.HelpLink,
                StackTrace = exception.StackTrace,

            }.ToString());

        }

        private void TraceAsync(HttpContext context, Exception ex)
        {
            var body = context.Items["body"].ToString();

            //TODO: Save log to chosen datastore
            var trace = ex.StackTrace;
            var cai = context.Items["unique"].ToString();
            //var elapsedMs = 34;
            var requestId = context.Items["id"].ToString();
            var requestPath = context.Request.Path.ToString();
            var requestScheme = $"{context.Request.Scheme} {context.Request.Host}{context.Request.Path} {context.Request.QueryString} {body}";

            //Logg.Error("Executed {@requestId} {@requestPath} {@requestScheme} {@trace} in {Elapsed:000} ms. by {unique} {type}",
            //requestId, requestPath, requestScheme, trace, elapsedMs, cai, EFLogType.EXCEPTION.GetDescription());

        }

    }
}