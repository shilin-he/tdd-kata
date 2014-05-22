using Owin;

namespace max.web.sample
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.Run(ctx =>
      {
        ctx.Response.ContentType = "text/plain";
        return ctx.Response.WriteAsync("Hello, max.web!");
      });
    }
  }
}