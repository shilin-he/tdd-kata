namespace max.web
{
  public interface IHandleWebRequests
  {
    IContainResponseInfo handle(IContainRequestInfo the_request);
  }
}