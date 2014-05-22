namespace max.web
{
  public interface IProcessOneWebRequest
  {
    IContainResponseInfo process(IContainInfoForOneWebRequest the_request);
    bool can_process(IContainInfoForOneWebRequest the_request);
  }
}