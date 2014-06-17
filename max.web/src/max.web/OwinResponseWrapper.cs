using System.Threading.Tasks;
using Microsoft.Owin;

namespace max.web
{
  public class OwinResponseWrapper : IContainResponseInfo
  {
    const string default_content_type = "text/html";

    public string content { get; set; }

    public string redirect_url { get; set; }

    public string content_type { get; set; }

    public Task finish(IOwinResponse owin_response)
    {
      if (!string.IsNullOrEmpty(redirect_url))
        owin_response.Redirect(redirect_url);

      owin_response.ContentType = content_type ?? default_content_type;

      return owin_response.WriteAsync(content ?? string.Empty);
    }
  }
}