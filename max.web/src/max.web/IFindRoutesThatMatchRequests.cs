using System;
using Microsoft.Owin;

namespace max.web
{
  public interface IFindRoutesThatMatchRequests
  {
    IContainRouteInfo find_route_match(IContainRequestInfo request);
  }
}