using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace max.web
{
  public class OwinResponseWrapper : IContainResponseInfo
  {
    readonly IOwinResponse owin_response;
    string _redirect_url;
    string _content_type;

    public OwinResponseWrapper(IOwinResponse owin_response)
    {
      this.owin_response = owin_response;
    }

    public string content { get; set; }

    public string redirect_url
    {
      get { return _redirect_url; }
      set
      {
        owin_response.Redirect(value);
        _redirect_url = value;
      }
    }

    public string content_type
    {
      get { return _content_type; }
      set
      {
        owin_response.ContentType = value;
        _content_type = value;
      }
    }

    public Task write()
    {
      return owin_response.WriteAsync(content ?? string.Empty);
    }
  }
}