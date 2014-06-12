using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Owin;

namespace max.web.owin
{
  public class OwinRequestWrapper : IContainRequestInfo
  {
    IOwinRequest owin_request;

    public OwinRequestWrapper(IOwinRequest owin_request)
    {
      this.owin_request = owin_request;
    }

    public string path
    {
      get { return owin_request.Path.Value; }
    }

    public string method
    {
      get { return owin_request.Method; }
    }

    public InputModel map<InputModel>()
    {
      var form_collection = owin_request.ReadFormAsync().Result;
      var input = Activator.CreateInstance<InputModel>();
      var properties = typeof(InputModel).GetProperties()
        .Where(p => form_collection.Get(p.Name) != null).ToList();
      properties.ForEach(p => p.SetValue(input, Convert.ChangeType(form_collection.Get(p.Name), p.PropertyType)));
      return input;
    }
  }
}