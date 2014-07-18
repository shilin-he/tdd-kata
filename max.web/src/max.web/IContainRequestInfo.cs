using System.Collections.Generic;
using System.Reflection.Emit;

namespace max.web
{
  public interface IContainRequestInfo
  {
    string path { get; }
    string method { get; }
    InputModel map<InputModel>();
  }
}