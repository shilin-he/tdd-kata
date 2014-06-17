using System.Net.Configuration;

namespace max.web
{
  public delegate Output IProcessDataModels<Output>(IContainRequestInfo request);
}