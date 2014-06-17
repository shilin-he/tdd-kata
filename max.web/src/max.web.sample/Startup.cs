using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.SessionState;
using max.web.owin;
using max.web.razor;
using Owin;

namespace max.web.sample
{
  public class Startup
  {
    static Startup()
    {
      register_views(ViewTable.views);
      register_commands(CommandTable.commands);
    }

    public void Configuration(IAppBuilder app)
    {
      app.Run(ctx =>
      {
        var handler = new WebHandler(new CommandRegistry(CommandTable.commands, request => new SpecialCaseWebCommand()));

        var response_info = handler.handle(new OwinRequestWrapper(ctx.Request));

        return response_info.finish(ctx.Response);
      });
    }

    private static void register_views(ViewCollection views)
    {
      views.add_view_from_file(typeof(ViewPersonModel), "tmpl/view_person.cshtml");
      views.add_view_from_file(typeof(EditPersonModel), "tmpl/edit_person.cshtml");
    }

    private static void register_commands(WebCommandCollection commands)
    {
      commands.add_view_command(@"/people/(\d+)", req => new ViewPersonModel { first_name = "foo", last_name = "bar" });
      commands.add_view_command(@"/people/update/(\d+)", req => new EditPersonModel { first_name = "foo", last_name = "bar" });
      commands.add_edit_command(@"/people/update/(\d+)", req => 11, (req, output) => "/people/11");
    }
  }
}