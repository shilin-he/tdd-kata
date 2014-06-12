namespace max.web
{
  public class ViewReport<Report> : IImplementOneAppFeature
  {
    IGetAReportUsingARequest<Report> query;
    IDisplayInformation display_engine;
    ICreateResponses response_factory;

    public ViewReport(IGetAReportUsingARequest<Report> query, IDisplayInformation display_engine, ICreateResponses response_factory)
    {
      this.query = query;
      this.display_engine = display_engine;
      this.response_factory = response_factory;
    }

    public IContainResponseInfo process(IContainRequestInfo the_request)
    {
      return response_factory(display_engine.render(query(the_request)));
    }
  }
}