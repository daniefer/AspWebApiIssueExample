using System;
using System.Threading.Tasks;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(SelfHostedWebApiWithSignalRDefect.Startup))]
namespace SelfHostedWebApiWithSignalRDefect
{
    public class DebugMiddleware : OwinMiddleware
    {
        private readonly DebugMiddlewareOptions _options;

        public DebugMiddleware(OwinMiddleware next, DebugMiddlewareOptions options) : base(next)
        {
            _options = options;
        }

        public override async Task Invoke(IOwinContext context)
        {
            _options.BeforeHandler?.Invoke(context);
            LogUserInfo(context);
            await Next.Invoke(context);
            _options.AfterHandler?.Invoke(context);
        }

        public static void LogUserInfo(IOwinContext context)
        {
            Console.WriteLine($"Authenticated?: {context.Authentication?.User?.Identity?.IsAuthenticated ?? false}");
            Console.WriteLine($"User: {context.Authentication?.User?.Identity?.Name ?? "NULL"}");
        }

        public static void Log(string text, ConsoleColor color = ConsoleColor.Cyan)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}
