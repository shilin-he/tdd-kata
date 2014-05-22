namespace max.web
{
  public delegate Report IGetAReportUsingARequest<out Report>(IContainInfoForOneWebRequest the_request);
}