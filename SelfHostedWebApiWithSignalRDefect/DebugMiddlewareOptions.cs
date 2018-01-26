using System;
using Microsoft.Owin;

namespace SelfHostedWebApiWithSignalRDefect
{
    public class DebugMiddlewareOptions
    {
        public Action<IOwinContext> BeforeHandler { get; set; }
        public Action<IOwinContext> AfterHandler { get; set; }
    }
}
