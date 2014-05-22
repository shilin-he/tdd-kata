namespace max.web
{
  public delegate Report IGetAReportUsingARequest<out Report>(IContainRequestInfo the_request);
}