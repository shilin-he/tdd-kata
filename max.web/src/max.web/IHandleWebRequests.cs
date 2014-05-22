namespace max.web
{
  public interface IHandleWebRequests
  {
    IContainResponseInfo handle(IContainInfoForOneWebRequest the_request);
  }
}