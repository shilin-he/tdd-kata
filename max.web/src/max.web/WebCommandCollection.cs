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

    public WebCommandCollection(IDisplayInformation display_engine)
    {
      this.display_engine = display_engine;
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

    public void add_view_command<ViewModel>(string url_path_pattern, IGetAReportUsingARequest<ViewModel> query)
    {
      IMatchOneRequest request_spec =
        req => req.method.Equals("get", StringComparison.OrdinalIgnoreCase) && Regex.IsMatch(req.path, url_path_pattern);

      IImplementOneAppFeature view_report = new ViewReport<ViewModel>(query, display_engine,
        content => new OwinResponseWrapper { content = content, content_type = "text/html" });

      commands.Add(new WebCommand(request_spec, view_report));
    }

    public void add_edit_command<Output>(string url_path_pattern, IProcessDataModels<Output> data_handler, ICreateRedirectUrls<Output> redirect_url_factory)
    {
      IMatchOneRequest request_spec =
        req => req.method.Equals("post", StringComparison.OrdinalIgnoreCase) && Regex.IsMatch(req.path, url_path_pattern);

      ICreateResponses response_factory = data => new OwinResponseWrapper {redirect_url = data};

      IImplementOneAppFeature update_model = new EditModel<Output>(response_factory, data_handler,
        redirect_url_factory);

      commands.Add(new WebCommand(request_spec, update_model));
    }
  }
}