using System.Threading.Tasks;
using Microsoft.Owin;

namespace max.web
{
  public interface IContainResponseInfo
  {
    string content { get; set; }
    string redirect_url { get; set; }
    string content_type { get; set; }
    Task finish(IOwinResponse owin_response);
  }
}