using System;

namespace max.web
{
  public class UpdateModel<Model> : IImplementOneAppFeature
  {
    IProcessDataModels<Model> data_handler;
    ICreateRedirectUrls<Model> redirect_url_factory;
    ICreateResponses response_factory;

    public UpdateModel(ICreateResponses response_factory, IProcessDataModels<Model> data_handler, ICreateRedirectUrls<Model> redirect_url_factory)
    {
      this.response_factory = response_factory;
      this.data_handler = data_handler;
      this.redirect_url_factory = redirect_url_factory;
    }

    public IContainResponseInfo process(IContainRequestInfo the_request)
    {
      var output = data_handler(the_request.map<Model>());
      var redirect_url = redirect_url_factory(output);
      return response_factory(redirect_url);
    }
  }
}