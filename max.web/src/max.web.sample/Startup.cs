using System;
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;
using max.web.owin;
using max.web.razor;
using Owin;

namespace max.web.sample
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.Run(ctx =>
      {
        IProvideAppBaseDirectory provide_app_base_directory = () => AppDomain.CurrentDomain.BaseDirectory;

        var templates = new Dictionary<Type, IContainTemplateInfo>
        {
          {typeof(Person), new RazorFileTemplate("tmpl/person.cshtml", provide_app_base_directory)}
        };

        var display_engine = new DisplayEngine(new ViewTemplateRegistry(templates), new MaxRazorTemplateRenderEngine());

        var commands = new List<IProcessOneWebRequest>
        {
          new WebCommand( request => true, 
            new ViewReport<Person>(request => new Person { first_name= "max", last_name = "he" }, 
                                   display_engine, 
                                   tmpl_render_result => new ResponseInfo(tmpl_render_result)))
        };

        var handler = new WebHandler(new CommandRegistry(commands, request => new SpecialCaseWebCommand()));

        var response_info = handler.handle(new OwinRequest());

        ctx.Response.ContentType = "text/html";

        return ctx.Response.WriteAsync(response_info.content);
      });
    }
  }
}