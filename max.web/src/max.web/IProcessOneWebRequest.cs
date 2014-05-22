namespace max.web
{
  public interface IProcessOneWebRequest
  {
    IContainResponseInfo process(IContainRequestInfo the_request);
    bool can_process(IContainRequestInfo the_request);
  }
}