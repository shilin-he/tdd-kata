namespace max.web.sample
{
  public class SpecialCaseWebCommand : IProcessOneWebRequest
  {
    public IContainResponseInfo process(IContainRequestInfo the_request)
    {
      return new OwinResponseWrapper {content = "Oops, can't process your request! Try something more specific..."};
    }

    public bool can_process(IContainRequestInfo the_request)
    {
      return true;
    }
  }
}