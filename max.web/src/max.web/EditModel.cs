using System;
using System.Runtime.CompilerServices;

namespace max.web
{
  public class EditModel<Output> : IImplementOneAppFeature
  {
    IProcessDataModels<Output> data_handler;
    ICreateRedirectUrls<Output> redirect_url_factory;
    ICreateResponses response_factory;

    public EditModel(ICreateResponses response_factory, IProcessDataModels<Output> data_handler, ICreateRedirectUrls<Output> redirect_url_factory)
    {
      this.response_factory = response_factory;
      this.data_handler = data_handler;
      this.redirect_url_factory = redirect_url_factory;
    }

    public IContainResponseInfo process(IContainRequestInfo the_request)
    {
      var output = data_handler(the_request);
      var redirect_url = redirect_url_factory(the_request, output);
      return response_factory(redirect_url);
    }
  }
}