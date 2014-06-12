using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
          {typeof(ViewPersonModel), new RazorFileTemplate("tmpl/view_person.cshtml", provide_app_base_directory)},
          {typeof(EditPersonModel), new RazorFileTemplate("tmpl/edit_person.cshtml", provide_app_base_directory)}
        };

        var display_engine = new DisplayEngine(new ViewTemplateRegistry(templates), new RazorTempalteRenderEngine());

        var commands = new List<IProcessOneWebRequest>
        {
          new WebCommand(request =>
            {
              var pattern = @"/people/view/(\d+)";
              return request.method.Equals("get", StringComparison.OrdinalIgnoreCase) &&
                     Regex.IsMatch(request.path, pattern);
            }, 
            new ViewReport<ViewPersonModel>(request => new ViewPersonModel { first_name= "foo", last_name = "bar" }, 
                                   display_engine, 
                                   content => new OwinResponseWrapper(ctx.Response) {content = content, content_type = "text/html"})),
          new WebCommand(request =>
            {
              var pattern = @"/people/edit/(\d+)";
              return request.method.Equals("get", StringComparison.OrdinalIgnoreCase) &&
                     Regex.IsMatch(request.path, pattern);
            }, 
            new ViewReport<EditPersonModel>(request => new EditPersonModel{ id = 11, first_name= "foo", last_name = "bar" }, 
                                   display_engine, 
                                   content => new OwinResponseWrapper(ctx.Response) {content = content, content_type = "text/html"})),
          new WebCommand(request =>
            {
              var pattern = @"/people/edit/(\d+)";
              return request.method.Equals("post", StringComparison.OrdinalIgnoreCase) &&
                     Regex.IsMatch(request.path, pattern);
            }, 
            new UpdateModel<EditPersonModel>(redirect_url => new OwinResponseWrapper(ctx.Response) {redirect_url = redirect_url},
                                             input => input,
                                             output => "/people/view/" + output.id))

        };

        var handler = new WebHandler(new CommandRegistry(commands, request => new SpecialCaseWebCommand()));

        var response_info = handler.handle(new OwinRequestWrapper(ctx.Request));

        return response_info.write();
      });
    }
  }
}