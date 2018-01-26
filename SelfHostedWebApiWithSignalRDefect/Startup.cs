using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(SelfHostedWebApiWithSignalRDefect.Startup))]
namespace SelfHostedWebApiWithSignalRDefect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Filters.Clear();
            config.SuppressDefaultHostAuthentication();

            // Start debug pipeline
            DebugMiddlewareOptions options = new DebugMiddlewareOptions
            {
                BeforeHandler = (ctx) => DebugMiddleware.Log("New Request"),
                AfterHandler = (ctx) => DebugMiddleware.Log("End Request\r\n****************************")
            };
            appBuilder.Use<DebugMiddleware>(options);

            appBuilder.Use<DebugMiddleware>(new DebugMiddlewareOptions
            {
                BeforeHandler = (ctx) => DebugMiddleware.Log("Before Auth")
            });

            // Create some dummy Identity
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AccessTokenFormat = new TokenFormatter(),
                AuthenticationType = "DummyAuth",
                Provider = new OAuthBearerAuthenticationProvider
                {
                    OnValidateIdentity = async (ctx) => ctx.Validated(ctx.Ticket),
                    OnRequestToken = async (ctx) => ctx.Token = "111"
                }
            });


            appBuilder.Use<DebugMiddleware>(new DebugMiddlewareOptions
            {
                BeforeHandler = (ctx) => DebugMiddleware.Log("After Auth")
            });


            // Add WebApi
            appBuilder.Use<DebugMiddleware>(new DebugMiddlewareOptions
            {
                BeforeHandler = (ctx) => DebugMiddleware.Log("Before WebApi")
            });
            config.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(config);
            appBuilder.Use<DebugMiddleware>(new DebugMiddlewareOptions
            {
                BeforeHandler = (ctx) => DebugMiddleware.Log("After WebApi")
            });

            // Add a Handler which will always return something
            appBuilder.Use<DebugMiddleware>(new DebugMiddlewareOptions
            {
                BeforeHandler = (ctx) => DebugMiddleware.Log("Before Universal Handler")
            });
            appBuilder.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Universal Handler!");
                await next.Invoke();
            });
            appBuilder.Use<DebugMiddleware>(new DebugMiddlewareOptions
            {
                BeforeHandler = (ctx) => DebugMiddleware.Log("After Universal Handler")
            });

            config.EnsureInitialized();
        }
    }
}
