using System;

namespace max.web.sample
{
  public class SpecialCaseWebCommand : IProcessOneWebRequest
  {
    public IContainResponseInfo process(IContainRequestInfo the_request)
    {
      throw new NotImplementedException();
    }

    public bool can_process(IContainRequestInfo the_request)
    {
      throw new NotImplementedException();
    }
  }
}