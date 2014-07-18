using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using max.web.razor;

namespace max.web
{
  public class WebCommandCollection : IEnumerable<IProcessOneWebRequest>
  {
    IList<IProcessOneWebRequest> commands = new List<IProcessOneWebRequest>();
    IDisplayInformation display_engine;
    IFindRoutesThatMatchRequests route_registry;

    public WebCommandCollection(IDisplayInformation display_engine, IFindRoutesThatMatchRequests route_registry)
    {
      this.display_engine = display_engine;
      this.route_registry = route_registry;
    }

    public IEnumerator<IProcessOneWebRequest> GetEnumerator()
    {
      return commands.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public void add_command(IMatchOneRequest request_spec, IImplementOneAppFeature feature)
    {
      commands.Add(new WebCommand(request_spec, feature));
    }

    public void add_view_command<ViewModel>(string resource, string operation, IGetAReportUsingARequest<ViewModel> query)
    {
      IMatchOneRequest request_spec =
        req =>
        {
          var route_parameters = route_registry.find_route_match(req).get_route_parameters(req);
          return req.method.Equals("get", StringComparison.OrdinalIgnoreCase) &&
            route_parameters["resource"].Equals(resource, StringComparison.OrdinalIgnoreCase) &&
            route_parameters["operation"].Equals(operation, StringComparison.OrdinalIgnoreCase);
        };

      IImplementOneAppFeature view_report = new ViewReport<ViewModel>(query, display_engine,
        content => new OwinResponseWrapper { content = content, content_type = "text/html" });

      commands.Add(new WebCommand(request_spec, view_report));
    }

    public void add_edit_command<Output>(string url_path_pattern, IProcessDataModels<Output> data_handler, ICreateRedirectUrls<Output> redirect_url_factory)
    {
      IMatchOneRequest request_spec =
        req => req.method.Equals("post", StringComparison.OrdinalIgnoreCase) && Regex.IsMatch(req.path, url_path_pattern);

      ICreateResponses response_factory = data => new OwinResponseWrapper { redirect_url = data };

      IImplementOneAppFeature update_model = new EditModel<Output>(response_factory, data_handler,
        redirect_url_factory);

      commands.Add(new WebCommand(request_spec, update_model));
    }
  }
}