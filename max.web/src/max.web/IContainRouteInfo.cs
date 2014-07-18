using System.Collections.Generic;

namespace max.web
{
  public interface IContainRouteInfo
  {
    IDictionary<string, string> get_route_parameters(IContainRequestInfo request);
  }
}